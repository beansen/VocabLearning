using System;
using System.Collections.Generic;
using System.Text;

namespace VocabLearning
{
    class Util
    {
        public static void ShuffleList<T>(ref List<T> list)
        {
            Random random = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static int SortByIndexAscending(LearnedData a, LearnedData b)
        {
            if (a.Index > b.Index)
            {
                return 1;
            }

            return -1;
        }

        public static int SortByIndexDescending(LearnedData a, LearnedData b)
        {
            if (a.Index > b.Index)
            {
                return -1;
            }

            return 1;
        }

        public static int SortByDateAscending(LearnedData a, LearnedData b)
        {
            if (a.Date > b.Date)
            {
                return 1;
            }

            return -1;
        }

        public static int SortByDateDescending(LearnedData a, LearnedData b)
        {
            if (a.Date > b.Date)
            {
                return -1;
            }

            return 1;
        }
    }
}
