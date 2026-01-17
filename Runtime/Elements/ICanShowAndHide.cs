namespace TravisRFrench.UI.MVVM.Elements
{
	public interface ICanShowAndHide : IHasShowState
	{
		void Show();
		void Hide();
		void ToggleShown();
	}
}
