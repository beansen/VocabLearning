using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VocabLearning
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VocabPage : ContentPage
    {
        private short cardSide = 0;
        private int wordIndex = 0;
        private List<WordPair> words = new List<WordPair>();
        private int startIndex;

        public VocabPage(int startIndex, int count)
        {
            this.startIndex = startIndex;
            InitializeComponent();
            words.AddRange(VocabHandler.Instance.Words.GetRange(startIndex, count));
            cardContent.Text = words[wordIndex].Word;
        }

        void OnTurnCard(object sender, EventArgs args)
        {
            if (cardSide == 0)
            {
                cardContent.Text = words[wordIndex].Translation;
                cardSide = 1;
            } else
            {
                cardContent.Text = words[wordIndex].Word;
                cardSide = 0;
            }
        }

        void OnNext(object sender, EventArgs args)
        {
            cardSide = 0;
            wordIndex++;
            cardContent.Text = words[wordIndex].Word;
            if (wordIndex == words.Count - 1)
            {
                startTestButton.IsVisible = true;
                nextButton.IsVisible = false;
            }

            if (wordIndex == 1)
            {
                backButton.IsEnabled = true;
            }

        }

        void OnBack(object sender, EventArgs args)
        {
            cardSide = 0;
            wordIndex--;
            cardContent.Text = words[wordIndex].Word;

            if (wordIndex == words.Count - 2)
            {
                startTestButton.IsVisible = false;
                nextButton.IsVisible = true;
            }

            if (wordIndex == 0)
            {
                backButton.IsEnabled = false;
            }
        }

        async void OnStartTest(object sender, EventArgs args)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new VocabTest(startIndex, words.Count));
        }
    }
}