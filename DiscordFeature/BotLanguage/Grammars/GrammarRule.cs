using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLanguage.Grammars
{
    public abstract class GrammarRule
    {
        private string ruleName;
        public List<string> grammarComposition = new List<string>();
        protected List<string> possibleWords = new List<string>();
        public List<GrammarRule> components = new List<GrammarRule>();
        public bool isSuccessful { get; set; }
        public string word { get; set; } = null;

        public virtual bool ProcessWordIntoGrammar(string word)
        {
            bool isGrammar = false;
            if (possibleWords.Contains(word))
            {
                this.word = word;
                isGrammar = true;
            }
            return isGrammar;
        }
        public abstract bool ProcessComponentsIntoGrammar(string stackString);//possibly take in the current stack 
        public virtual List<GrammarRule> GenerateWords()
        {
            List<GrammarRule> generateWords = new List<GrammarRule>();
            for (int j = 0; j < components.Count; j++)
            {
                if (components.ElementAt(j).GetType().Equals(new Article().GetType()) || components.ElementAt(j).GetType().Equals(new Noun().GetType()) || components.ElementAt(j).GetType().Equals(new Verb().GetType()) || components.ElementAt(j).GetType().Equals(new Preposition().GetType()) || components.ElementAt(j).GetType().Equals(new Adjective().GetType()))
                {
                    generateWords.Add(components[j]);
                }
                else
                {
                    if (generateWords.Count == 0)
                    {
                        generateWords = components[j].GenerateWords();
                    }
                    else
                    {
                        List<GrammarRule> subList = components.ElementAt(j).GenerateWords();
                        generateWords = generateWords.Concat(subList).ToList();
                    }

                }
            }
            return generateWords;
        }
    }
}
