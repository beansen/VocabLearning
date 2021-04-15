using Xamarin.Forms;

namespace VocabLearning
{
    public abstract class Screen : Layout
    {

        public View ScreenContent
        {
            set { this.Content = value; }
            get { return this.Content; }
        }
        public virtual void OnAppearance()
        {
        }
    }
}
