using System;
using UnityEngine.UIElements;

namespace TravisRFrench.UI.MVVM.Samples.Editor
{
	public sealed class BoolFieldConverter : IInspectorFieldConverter
	{
		public bool CanConvert(Type valueType) => valueType == typeof(bool);

		public VisualElement CreateField(string label, Type valueType, Func<object?> getter, Action<object?> setter, bool isLive)
		{
			var field = new Toggle(label)
			{
				value = getter() is bool b && b
			};

			if (isLive)
			{
				field.RegisterValueChangedCallback(e => setter(e.newValue));
			}

			return field;
		}
	}
}