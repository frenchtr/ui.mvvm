using TravisRFrench.UI.MVVM.Core;

namespace TravisRFrench.UI.MVVM.Elements
{
	public class Element : ViewModel, IElement
	{
		public bool IsActive { get; private set; }
		public bool IsEnabled { get; private set; }
		public bool IsVisible { get; private set; }

		public void SetActive(bool active)
		{
			this.IsActive = active;
		}
		
		public void SetEnabled(bool enabled)
		{
			this.IsEnabled = enabled;
		}
		
		public void SetVisible(bool visible)
		{
			this.IsVisible = visible;
		}
	}
}
