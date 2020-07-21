using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VocabLearning
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VocabTestListPage : ContentPage
    {
        public VocabTestListPage()
        {
            InitializeComponent();
            sortingPicker.SelectedIndexChanged += OnPickerSelectedIndexChanged;
            Init();
        }

        private void Init()
        {
            int index = 0;

            if (Application.Current.Properties.ContainsKey("LastLearned"))
            {
                string json = (string)Application.Current.Properties["LastLearned"];

                Dictionary<int, LearnedData> lastLearned = JsonConvert.DeserializeObject<Dictionary<int, LearnedData>>(json);

                for (int i = 0; i < lastLearned.Count; i++)
                {
                    int lastWord = (i + 1) * 10;
                    int firstWord = i * 10;
                    if (lastWord > VocabHandler.Instance.Words.Count)
                    {
                        lastWord = VocabHandler.Instance.Words.Count;
                    }

                    string wordsIndexText = String.Format("{0} - {1}", firstWord + 1, lastWord);
                    string lastLearnedText = String.Format("Last learned: {0}", lastLearned[i].Date.ToString());
                    StackLayout stackLayout = new StackLayout() {HeightRequest = 70};
                    stackLayout.Children.Add(new Label() {FontSize = 26, HorizontalTextAlignment =  TextAlignment.Center, Text = wordsIndexText, TextColor = Color.Black });
                    stackLayout.Children.Add(new Label() { FontSize = 17, HorizontalTextAlignment = TextAlignment.Center, Text = lastLearnedText, TextColor = Color.Black });

                    TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += async (s, e) =>
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new VocabTest(firstWord, lastWord - firstWord));
                    };
                    stackLayout.GestureRecognizers.Add(tapGestureRecognizer);

                    listLayout.Children.Add(stackLayout);

                }

                index = lastLearned.Count;
            }

            while (index * 10 < VocabHandler.Instance.Words.Count)
            {
                int lastWord = (index + 1) * 10;
                int firstWord = index * 10;
                if (lastWord > VocabHandler.Instance.Words.Count)
                {
                    lastWord = VocabHandler.Instance.Words.Count;
                }
                string wordsIndexText = String.Format("{0} - {1}", firstWord + 1, lastWord);
                string lastLearnedText = "Last learned: -";
                StackLayout stackLayout = new StackLayout() { HeightRequest = 70 };
                stackLayout.Children.Add(new Label() { FontSize = 26, HorizontalTextAlignment = TextAlignment.Center, Text = wordsIndexText, TextColor = Color.Gray});
                stackLayout.Children.Add(new Label() { FontSize = 17, HorizontalTextAlignment = TextAlignment.Center, Text = lastLearnedText, TextColor = Color.Gray });
                listLayout.Children.Add(stackLayout);

                index++;
            }
        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            List<LearnedData> dataList = new List<LearnedData>();

            if (Application.Current.Properties.ContainsKey("LastLearned"))
            {
                string json = (string)Application.Current.Properties["LastLearned"];

                Dictionary<int, LearnedData> lastLearned = JsonConvert.DeserializeObject<Dictionary<int, LearnedData>>(json);
                dataList.AddRange(lastLearned.Values);
            }

            int direction = 0;
            switch(sortingPicker.SelectedIndex)
            {
                case 0:
                    direction = 1;
                    dataList.Sort(Util.SortByIndexAscending);
                    break;
                case 1:
                    direction = -1;
                    dataList.Sort(Util.SortByIndexDescending);
                    break;
                case 2:
                    direction = 1;
                    dataList.Sort(Util.SortByDateAscending);
                    break;
                case 3:
                    direction = -1;
                    dataList.Sort(Util.SortByDateDescending);
                    break;
            }

            SortList(direction, dataList);
        }

        private void SortList(int direction, List<LearnedData> dataList)
        {
            for (int i = 0; i < listLayout.Children.Count; i++)
            {
                int firstWord = direction == 1 ? i * 10 : (VocabHandler.Instance.Words.Count / 10 - i) * 10;
                int lastWord = firstWord + 10;

                if (lastWord > VocabHandler.Instance.Words.Count)
                {
                    lastWord = VocabHandler.Instance.Words.Count;
                }

                StackLayout stackLayout = (StackLayout)listLayout.Children[i];
                Label wordIndexLabel = (Label)stackLayout.Children[0];
                Label lastLearnedLabel = (Label)stackLayout.Children[1];

                wordIndexLabel.Text = String.Format("{0} - {1}", firstWord + 1, lastWord);

                stackLayout.GestureRecognizers.Clear();

                if (direction == 1)
                {
                    if (i < dataList.Count)
                    {
                        LearnedData learnedData = dataList[i];
                        firstWord = learnedData.Index * 10;
                        lastWord = firstWord + 10;

                        if (lastWord > VocabHandler.Instance.Words.Count)
                        {
                            lastWord = VocabHandler.Instance.Words.Count;
                        }

                        wordIndexLabel.Text = String.Format("{0} - {1}", firstWord + 1, lastWord);
                        lastLearnedLabel.Text = String.Format("Last learned: {0}", learnedData.Date.ToString());
                        wordIndexLabel.TextColor = Color.Black;
                        lastLearnedLabel.TextColor = Color.Black;
                        TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                        tapGestureRecognizer.Tapped += async (s, e) =>
                        {
                            await Application.Current.MainPage.Navigation.PushAsync(new VocabTest(firstWord, lastWord - firstWord));
                        };
                        stackLayout.GestureRecognizers.Add(tapGestureRecognizer);
                    }
                    else
                    {
                        wordIndexLabel.Text = String.Format("{0} - {1}", firstWord + 1, lastWord);
                        lastLearnedLabel.Text = "Last learned: -";
                        wordIndexLabel.TextColor = Color.Gray;
                        lastLearnedLabel.TextColor = Color.Gray;
                    }
                }
                else
                {
                    if (dataList.Count >= listLayout.Children.Count - i)
                    {
                        int index = Math.Abs(listLayout.Children.Count - i - dataList.Count);

                        LearnedData learnedData = dataList[index];
                        firstWord = learnedData.Index * 10;
                        lastWord = firstWord + 10;

                        if (lastWord > VocabHandler.Instance.Words.Count)
                        {
                            lastWord = VocabHandler.Instance.Words.Count;
                        }

                        wordIndexLabel.Text = String.Format("{0} - {1}", firstWord + 1, lastWord);
                        lastLearnedLabel.Text = String.Format("Last learned: {0}", learnedData.Date.ToString());
                        wordIndexLabel.TextColor = Color.Black;
                        lastLearnedLabel.TextColor = Color.Black;
                        TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                        tapGestureRecognizer.Tapped += async (s, e) =>
                        {
                            await Application.Current.MainPage.Navigation.PushAsync(new VocabTest(firstWord, lastWord - firstWord));
                        };
                        stackLayout.GestureRecognizers.Add(tapGestureRecognizer);
                    }
                    else
                    {
                        lastLearnedLabel.Text = "Last learned: -";
                        wordIndexLabel.TextColor = Color.Gray;
                        lastLearnedLabel.TextColor = Color.Gray;
                    }
                }
            }
        }
    }
}