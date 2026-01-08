using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TravisRFrench.UI.MVVM.Core;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace TravisRFrench.UI.MVVM.Samples.Editor
{
	[CustomEditor(typeof(View<>), true)]
	[CanEditMultipleObjects]
	public sealed class ViewEditor : UnityEditor.Editor
	{
		private static readonly Type OpenViewType = typeof(View<>);

		private static readonly Dictionary<Type, Type?> ViewModelTypeCache = new();
		private static readonly Dictionary<Type, PropertyInfo?> ViewModelPropCache = new();
		private static readonly Dictionary<Type, PropertyInfo[]> ViewModelEditablePropsCache = new();

		public override VisualElement CreateInspectorGUI()
		{
			var root = new VisualElement();
			
			// Safe default inspector for the View itself.
			InspectorElement.FillDefaultInspector(root, this.serializedObject, this);
			
			// View Model Container
			var container = new VisualElement();

			var viewType = this.target.GetType();
			var vmType = GetViewModelType(viewType);

			if (vmType == null)
			{
				container.Add(new HelpBox("Could not determine ViewModel type from View<> base class.", HelpBoxMessageType.Warning));
				return container;
			}

			var hasInstance = TryGetViewModelInstance(this.target, viewType, out var vmInstance);

			container.Add(BuildViewModelSection(vmType, hasInstance ? vmInstance : null));
			return container;
		}

		private static Type? GetViewModelType(Type concreteViewType)
		{
			if (ViewModelTypeCache.TryGetValue(concreteViewType, out var cached))
			{
				return cached;
			}

			var t = concreteViewType;
			while (t != null)
			{
				if (t.IsGenericType && t.GetGenericTypeDefinition() == OpenViewType)
				{
					var vmType = t.GetGenericArguments()[0];
					ViewModelTypeCache[concreteViewType] = vmType;
					return vmType;
				}

				t = t.BaseType;
			}

			ViewModelTypeCache[concreteViewType] = null;
			return null;
		}

		private static bool TryGetViewModelInstance(object targetObj, Type concreteViewType, out object? instance)
		{
			instance = null;

			if (!ViewModelPropCache.TryGetValue(concreteViewType, out var prop))
			{
				prop = concreteViewType.GetProperty(
					nameof(View<IViewModel>.ViewModel),
					BindingFlags.Instance | BindingFlags.Public);

				ViewModelPropCache[concreteViewType] = prop;
			}

			if (prop == null || prop.GetMethod == null || prop.GetIndexParameters().Length != 0)
			{
				return false;
			}

			instance = prop.GetValue(targetObj);
			return instance != null;
		}

		private static VisualElement BuildViewModelSection(Type viewModelType, object? viewModelInstance)
		{
			var foldout = new Foldout
			{
				text = "ViewModel",
				value = true
			};

			foldout.Add(new Label($"Type: {viewModelType.FullName}"));

			var isLive = viewModelInstance != null;
			foldout.Add(new Label($"Instance: {(isLive ? "(live)" : "(none - showing defaults)")}"));

			if (!isLive)
			{
				foldout.Add(new HelpBox(
					"No injected ViewModel instance is available in the editor. Fields below show default values based on type.",
					HelpBoxMessageType.Info));
			}

			var props = GetEditableProperties(viewModelType);
			if (props.Length == 0)
			{
				foldout.Add(new HelpBox("No public get/set properties found to display.", HelpBoxMessageType.None));
				return foldout;
			}

			foreach (var prop in props)
			{
				var label = ObjectNames.NicifyVariableName(prop.Name);

				Func<object?> getter = () =>
					isLive ? prop.GetValue(viewModelInstance!) : GetDefault(prop.PropertyType);

				Action<object?> setter = v =>
				{
					if (!isLive) return;
					prop.SetValue(viewModelInstance!, v);
				};

				var converter = InspectorFieldConverterRegistry.GetConverter(prop.PropertyType);

				VisualElement field;
				if (converter != null)
				{
					field = converter.CreateField(label, prop.PropertyType, getter, setter, isLive);
				}
				else
				{
					field = new Label($"{label}: ({prop.PropertyType.Name})");
				}

				if (!isLive)
				{
					field.SetEnabled(false);
				}

				foldout.Add(field);
			}

			return foldout;
		}

		private static PropertyInfo[] GetEditableProperties(Type viewModelType)
		{
			if (ViewModelEditablePropsCache.TryGetValue(viewModelType, out var cached))
			{
				return cached;
			}

			var props = viewModelType
				.GetProperties(BindingFlags.Instance | BindingFlags.Public)
				.Where(p =>
					p.GetIndexParameters().Length == 0 &&
					p.GetMethod != null &&
					p.SetMethod != null)
				.ToArray();

			ViewModelEditablePropsCache[viewModelType] = props;
			return props;
		}

		private static object? GetDefault(Type t)
		{
			if (t == typeof(string)) return string.Empty;
			if (t.IsValueType) return Activator.CreateInstance(t);
			return null;
		}
	}
}