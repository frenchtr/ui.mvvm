using System;
using UnityEngine.UIElements;

namespace TravisRFrench.UI.MVVM.Samples.Editor
{
	public sealed class EnumFieldConverter : IInspectorFieldConverter
	{
		public bool CanConvert(Type valueType) => valueType.IsEnum;

		public VisualElement CreateField(string label, Type valueType, Func<object?> getter, Action<object?> setter, bool isLive)
		{
			var current = getter() as Enum ?? (Enum)Enum.GetValues(valueType).GetValue(0)!;
			var field = new EnumField(label, current);

			if (isLive)
			{
				field.RegisterValueChangedCallback(e => setter(e.newValue));
			}

			return field;
		}
	}
}