using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VocabLearning
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : Screen
    {
        public MainMenu()
        {
            string text = File.ReadAllText("MainMenu.xaml");
            ScreenContent = new ContentView().LoadFromXaml(text);
        }

        async void OnVocabButton(object sender, EventArgs args)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new VocabListPage());
        }

        async void OnTestButton(object sender, EventArgs args)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new VocabTestListPage());
        }

        public override void OnAppearance()
        {

        }


    }
}