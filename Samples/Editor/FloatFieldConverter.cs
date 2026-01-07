using System;
using UnityEngine.UIElements;

namespace TravisRFrench.UI.MVVM.Samples.Editor
{
	public sealed class FloatFieldConverter : IInspectorFieldConverter
	{
		public bool CanConvert(Type valueType) => valueType == typeof(float);

		public VisualElement CreateField(string label, Type valueType, Func<object?> getter, Action<object?> setter, bool isLive)
		{
			var field = new FloatField(label)
			{
				value = getter() is float f ? f : 0f
			};

			if (isLive)
			{
				field.RegisterValueChangedCallback(e => setter(e.newValue));
			}

			return field;
		}
	}
}