﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VocabLearning
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VocabListPage : ContentPage
    {
        public VocabListPage()
        {
            InitializeComponent();
        }

        private void Init()
        {
            listLayout.Children.Clear();

            int maxMastered = -1;

            if (Application.Current.Properties.ContainsKey("MaxMastered"))
            {
                maxMastered = (int)Application.Current.Properties["MaxMastered"];
            }

            maxMastered++;

            int index = 0;

            while (index * 10 < VocabHandler.Instance.Words.Count)
            {
                int lastWord = (index + 1) * 10;
                int firstWord = index * 10;
                if (lastWord > VocabHandler.Instance.Words.Count)
                {
                    lastWord = VocabHandler.Instance.Words.Count;
                }

                Label label = new Label();
                label.HorizontalTextAlignment = TextAlignment.Center;
                label.VerticalTextAlignment = TextAlignment.Center;
                label.HeightRequest = 40;
                label.TextColor = index <= maxMastered ? Color.Black : Color.Gray;
                label.FontSize = 26;
                label.IsEnabled = index <= maxMastered;
                label.Text = String.Format("{0} - {1}", firstWord + 1, lastWord);
                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += async (s, e) =>
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new VocabPage(firstWord, lastWord - firstWord));
                };
                label.GestureRecognizers.Add(tapGestureRecognizer);
                listLayout.Children.Add(label);

                index++;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Init();
        }
    }
}