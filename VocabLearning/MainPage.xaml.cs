using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace VocabLearning
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnVocabButton(object sender, EventArgs args)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new VocabListPage());
        }

        async void OnTestButton(object sender, EventArgs args)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new VocabTestListPage());
        }
    }
}
