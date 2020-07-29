using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VocabLearning
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VocabTest : ContentPage
    {
        private List<WordPair> testWords;
        private int wordIndex = 0;
        private int correctAnswers = 0;
        public VocabTest()
        {
            InitializeComponent();
        }

        public VocabTest(int startIndex, int count)
        {
            InitializeComponent();
            testWords = new List<WordPair>();
            testWords.AddRange(VocabHandler.Instance.Words.GetRange(startIndex, count));
            Util.ShuffleList<WordPair>(ref testWords);
            cardContent.Text = testWords[wordIndex].Translation;
            popupLayout.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => {
                    popupLayout.IsVisible = false;
                    wordIndex++;
                    if (wordIndex < testWords.Count)
                    {
                        cardContent.Text = testWords[wordIndex].Translation;
                        userEntry.Text = String.Empty;
                    } else
                    {
                        testLayout.IsVisible = false;
                        resultLayout.IsVisible = true;
                        answers.Text = String.Format("Correct answers: {0}/{1}", correctAnswers, testWords.Count);
                        int acc = correctAnswers * 100 / testWords.Count;
                        accuracy.Text = String.Format("Accuracy: {0}%", acc);

                        int index = startIndex / 10;

                        if (acc >= 80)
                        {
                            int maxMastered = -1;

                            if (Application.Current.Properties.ContainsKey("MaxMastered"))
                            {
                                maxMastered = (int)Application.Current.Properties["MaxMastered"];
                            }

                            if (index > maxMastered)
                            {
                                Application.Current.Properties["MaxMastered"] = index;
                            }
                        }
                        
                        Dictionary<int, LearnedData> testData;
                        if (Application.Current.Properties.ContainsKey("TestData"))
                        {
                            string json = (string)Application.Current.Properties["TestData"];

                            testData = JsonConvert.DeserializeObject<Dictionary<int, LearnedData>>(json);
                        }
                        else
                        {
                            testData = new Dictionary<int, LearnedData>();
                        }

                        testData[index] = new LearnedData(index, DateTime.Now, acc);
                        Application.Current.Properties["TestData"] = JsonConvert.SerializeObject(testData);
                        Application.Current.SavePropertiesAsync();
                    }
                    //userEntry.Focus();
                }),
                NumberOfTapsRequired = 1
            });
        }

        void OnEnterPressed(object sender, EventArgs args)
        {
            popupLayout.IsVisible = true;
            String word = testWords[wordIndex].Word;
            bool correct = userEntry.Text == word;
            String answer = String.Format("Wrong answer\n{0}", word);
            
            if (correct)
            {
                correctAnswers++;
                answer = "Right answer";
            
            }
            popupContent.Text = answer;
        }

        void OnRestart(object sender, EventArgs args)
        {
            testLayout.IsVisible = true;
            resultLayout.IsVisible = false;
            correctAnswers = 0;
            wordIndex = 0;
            Util.ShuffleList<WordPair>(ref testWords);
            cardContent.Text = testWords[wordIndex].Translation;
            userEntry.Text = String.Empty;
        }

        async void OnReview(object sender, EventArgs args)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        async void OnMainMenu(object sender, EventArgs args)
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }
    }
}