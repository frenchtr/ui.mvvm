// ViewGenericEditorUITK.cs
// Place in an Editor/ folder.

using System;
using UnityEngine.UIElements;

// Built-in converters

namespace TravisRFrench.UI.MVVM.Samples.Editor
{
    public sealed class StringFieldConverter : IInspectorFieldConverter
    {
        public bool CanConvert(Type valueType) => valueType == typeof(string);

        public VisualElement CreateField(string label, Type valueType, Func<object?> getter, Action<object?> setter, bool isLive)
        {
            var field = new TextField(label)
            {
                value = getter() as string ?? string.Empty
            };

            if (isLive)
            {
                field.RegisterValueChangedCallback(e => setter(e.newValue));
            }

            return field;
        }
    }
}