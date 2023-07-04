using System.ComponentModel;
using System.Globalization;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace UniqueWords
{
    public static class TextProcessor
    {
        /// <summary>
        /// Returns the list of unique words in the <see cref="words"/> array.
        /// </summary>
        public static List<string> GetUniqueWordsFromArray(string[] words)
        {
            var list = new List<string>();

            for (int i = 0; i < words.Length; i++)
            {
                if (!list.Contains(words[i]))
                {
                    list.Add(words[i]);
                }
            }

            return list;
        }

        /// <summary>
        /// Returns the number of unique words in the <see cref="text"/>.
        /// </summary>
        public static int CountUniqueWordsInText(string text)
        {
            string[] words = text.Split(new char[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
                         .Select(word => word.ToLower(CultureInfo.InvariantCulture))
                         .ToArray();

            List<string> uniqueWords = new List<string>();

            foreach (string word in words)
            {
                if (!uniqueWords.Contains(word))
                {
                    uniqueWords.Add(word);
                }
            }

            return uniqueWords.Count;
        }

        /// <summary>
        /// Returns the list of unique words extracted from the <see cref="lines"/>. Words are separated with a space (' ') character.
        /// </summary>
        public static IEnumerable<string> GetUniqueWordsFromEnumerable(IEnumerable<string> lines)
        {
            List<string> uniqueWords = new List<string>();

            foreach (string line in lines)
            {
                string[] words = line.Split(' ');

                foreach (string word in words)
                {
                    if (!uniqueWords.Contains(word))
                    {
                        uniqueWords.Add(word);
                        yield return word;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the list of unique words extracted from the <see cref="text"/>. Words are separated with the <see cref="separator"/> character.
        /// </summary>
        public static string[][] GetUniqueWordsArray(IEnumerable<string> lines, char separator)
        {
            List<string[]> uniqueWordsList = new List<string[]>();

            foreach (string line in lines)
            {
                string[] words = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                List<string> uniqueWords = new List<string>();

                foreach (string word in words)
                {
                    string trimmedWord = word.Trim();
                    bool isDuplicate = false;

                    foreach (string[] prevLineWords in uniqueWordsList)
                    {
                        if (prevLineWords.Contains(trimmedWord))
                        {
                            isDuplicate = true;
                            break;
                        }
                    }

                    if (!isDuplicate)
                    {
                        uniqueWords.Add(trimmedWord);
                    }
                }

                uniqueWordsList.Add(uniqueWords.ToArray());
            }

            return uniqueWordsList.ToArray();
        }

        /// <summary>
        /// Returns the list of unique words extracted from the <see cref="text"/>. Words are separated with the <see cref="separator"/> characters.
        /// </summary>
        public static ICollection<string> GetUniqueWordsCollection(string text, char separator)
        {
            text = text.Replace("\r\n", " ", StringComparison.InvariantCulture).ToString(CultureInfo.InvariantCulture).Trim();
            List<string> uniqueWords = new List<string>();
            int startIndex = 0;

            while (startIndex < text.Length)
            {
                int separatorIndex = text.IndexOf(separator, startIndex);
                if (separatorIndex == -1)
                {
                    string lastWord = text[startIndex..].Trim();
                    uniqueWords.Add(lastWord);
                    break;
                }
                else
                {
                    string word = text[startIndex..separatorIndex].Trim();
                    uniqueWords.Add(word);
                    startIndex = separatorIndex + 1;
                }
            }

            List<string> distinctWords = new List<string>();
            foreach (string word in uniqueWords)
            {
                if (!distinctWords.Contains(word))
                {
                    distinctWords.Add(word);
                }
            }

            return distinctWords;
        }

        /// <summary>
        /// Returns the number of unique words in the <see cref="text"/>.
        /// </summary>
        public static ICollection<string> GetUniqueWordsInCharCollection(ICollection<char> collection, char separator)
        {
            List<string> uniqueWords = new List<string>();
            StringBuilder wordBuilder = new StringBuilder();

            foreach (char character in collection)
            {
                if (character == separator)
                {
                    string word = wordBuilder.ToString().Trim();

                    if (!string.IsNullOrEmpty(word) && !uniqueWords.Contains(word))
                    {
                        uniqueWords.Add(word);
                    }

                    wordBuilder.Clear();
                }
                else
                {
                    wordBuilder.Append(character);
                }
            }

            string lastWord = wordBuilder.ToString().Trim();

            if (!string.IsNullOrEmpty(lastWord) && !uniqueWords.Contains(lastWord))
            {
                uniqueWords.Add(lastWord);
            }

            return uniqueWords;
        }
    }
}
