using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace VocabLearning
{
    class VocabHandler
    {
        private static VocabHandler instance;

        private readonly List<WordPair> words;

        public List<WordPair> Words
        {
            get { return words; }
        }

        public static VocabHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VocabHandler();
                }

                return instance;
            }
        }

        public VocabHandler()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(VocabHandler)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("VocabLearning.words.json");
            string text = "";
            using (var reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }

            words = JsonConvert.DeserializeObject<List<WordPair>>(text);
        }
    }
}
