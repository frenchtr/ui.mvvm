using TravisRFrench.UI.MVVM.Samples.Common;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace TravisRFrench.UI.MVVM.Samples.Editor
{
    [CustomPropertyDrawer(typeof(FlattenAttribute))]
    public sealed class FlattenPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var a = (FlattenAttribute)this.attribute;

            var child = property.FindPropertyRelative(a.RelativePath);
            if (child == null)
            {
                var root = new VisualElement();
                root.Add(new HelpBox(
                    $"Flatten: Could not find relative property '{a.RelativePath}' on '{property.propertyPath}'.",
                    HelpBoxMessageType.Warning));

                var fallback = new PropertyField(property);
                fallback.Bind(property.serializedObject);
                root.Add(fallback);
                return root;
            }

            var label = string.IsNullOrEmpty(a.LabelOverride)
                ? child.displayName
                : a.LabelOverride;

            var field = new PropertyField(child, label);
            field.Bind(property.serializedObject);
            return field;
        }

        // IMGUI fallback (older inspector paths)
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var a = (FlattenAttribute)this.attribute;
            var child = property.FindPropertyRelative(a.RelativePath);

            if (child == null)
            {
                EditorGUI.PropertyField(position, property, label, true);
                return;
            }

            var childLabel = string.IsNullOrEmpty(a.LabelOverride)
                ? new GUIContent(child.displayName)
                : new GUIContent(a.LabelOverride);

            EditorGUI.PropertyField(position, child, childLabel, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var a = (FlattenAttribute)this.attribute;
            var child = property.FindPropertyRelative(a.RelativePath);

            return child != null
                ? EditorGUI.GetPropertyHeight(child, true)
                : EditorGUI.GetPropertyHeight(property, label, true);
        }
    }
}
