using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLanguage.Grammars
{
    public class Sentence : GrammarRule
    {
        string sentance = null;
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

        public string GenerateSenteance()
        {
            List<GrammarRule> words = GenerateWords();
            Dictionary<string, int> WordTypeCount = GenerateWordCount(words);
            sentance = generateLeadingPhrase();
            int length = 0;
            foreach (GrammarRule rule in words)
            {
                if (rule.GetType().Equals(new Verb().GetType()))
                {
                    sentance += rule.word.Substring(0, rule.word.Length - 1) + " ";
                }
                else
                {
                    sentance += rule.word + " ";
                }
                length = sentance.Length;
            }
            sentance = sentance.Substring(0, length - 1);
            sentance += "?";
            return sentance;
        }

        private string generateLeadingPhrase()
        {
            string[] leadPhrases = { "Who does ", "Where did ", "How did ", "When did " };
            Random r = new Random();
            return leadPhrases[r.Next(leadPhrases.Length)];
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
