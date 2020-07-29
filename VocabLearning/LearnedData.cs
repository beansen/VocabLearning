using System;
using System.Collections.Generic;
using System.Text;

namespace VocabLearning
{
    class LearnedData
    {
        private int index;
        private DateTime date;
        private int accuracy;

        public int Index { get => index; set => index = value; }
        public DateTime Date { get => date; set => date = value; }
        public int Accuracy { get => accuracy; set => accuracy = value; }

        public LearnedData(int index, DateTime date, int accuracy)
        {
            this.index = index;
            this.date = date;
            this.accuracy = accuracy;
        }
    }
}
