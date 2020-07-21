using System;
using System.Collections.Generic;
using System.Text;

namespace VocabLearning
{
    class LearnedData
    {
        private int index;
        private DateTime date;

        public int Index { get => index; set => index = value; }
        public DateTime Date { get => date; set => date = value; }

        public LearnedData(int index, DateTime date)
        {
            this.index = index;
            this.date = date;
        }
    }
}
