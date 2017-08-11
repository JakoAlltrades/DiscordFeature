using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLanguage.Grammars
{
    public class Sentence : GrammarRule
    {
        public Sentence()
        {
            grammarComposition.Add("NounPhrase VerbPhrase");
            possibleWords = null;
        }

        public override bool ProcessComponentsIntoGrammar(string stackString)
        {
            bool succsess = false;
            if (grammarComposition.Contains(stackString))
            {
                succsess = true;
            }
            return succsess;
        }

        private Dictionary<string, int> GenerateWordCount(List<GrammarRule> words)
        {
            Dictionary<string, int> wordCount = new Dictionary<string, int>();
            wordCount.Add("Noun", 0);
            wordCount.Add("Verb", 0);
            wordCount.Add("Article", 0);
            wordCount.Add("Preposition", 0);
            for (int j = 0; j < words.Count; j++)
            {
                int curCount;
                switch (words.ElementAt(j).ToString())
                {
                    case "Noun":
                        if (wordCount.TryGetValue(words.ElementAt(j).ToString(), out curCount))
                        {
                            curCount++;
                            wordCount.Remove(words.ElementAt(j).ToString());
                            wordCount.Add(words.ElementAt(j).ToString(), curCount);
                        }
                        break;
                    case "Verb":
                        if (wordCount.TryGetValue(words.ElementAt(j).ToString(), out curCount))
                        {
                            curCount++;
                            wordCount.Remove(words.ElementAt(j).ToString());
                            wordCount.Add(words.ElementAt(j).ToString(), curCount);
                        }
                        break;
                    case "Article":
                        if (wordCount.TryGetValue(words.ElementAt(j).ToString(), out curCount))
                        {
                            curCount++;
                            wordCount.Remove(words.ElementAt(j).ToString());
                            wordCount.Add(words.ElementAt(j).ToString(), curCount);
                        }
                        break;
                    case "Preposition":
                        if (wordCount.TryGetValue(words.ElementAt(j).ToString(), out curCount))
                        {
                            curCount++;
                            wordCount.Remove(words.ElementAt(j).ToString());
                            wordCount.Add(words.ElementAt(j).ToString(), curCount);
                        }
                        break;

                }
            }

            return wordCount;
        }

        public override string ToString()
        {
            return "Sentence";
        }
    }
}
