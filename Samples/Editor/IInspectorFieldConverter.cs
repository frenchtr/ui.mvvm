using System;
using UnityEngine.UIElements;

namespace TravisRFrench.UI.MVVM.Samples.Editor
{
	public interface IInspectorFieldConverter
	{
		bool CanConvert(Type valueType);

		VisualElement CreateField(
			string label,
			Type valueType,
			Func<object?> getter,
			Action<object?> setter,
			bool isLive);
	}
}