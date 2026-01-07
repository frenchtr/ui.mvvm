using System;
using UnityEngine.UIElements;

namespace TravisRFrench.UI.MVVM.Samples.Editor
{
	public sealed class IntFieldConverter : IInspectorFieldConverter
	{
		public bool CanConvert(Type valueType) => valueType == typeof(int);

		public VisualElement CreateField(string label, Type valueType, Func<object?> getter, Action<object?> setter, bool isLive)
		{
			var field = new IntegerField(label)
			{
				value = getter() is int i ? i : 0
			};

			if (isLive)
			{
				field.RegisterValueChangedCallback(e => setter(e.newValue));
			}

			return field;
		}
	}
}