using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLanguage.Grammars
{
    public class NounPhrase : GrammarRule
    {
        public NounPhrase()
        {
            possibleWords = null;
            grammarComposition.Add("Article Noun Preposition NounPhrase");
            grammarComposition.Add("Article Noun");
            grammarComposition.Add("Article Adjective Noun Preposition NounPhrase");
            grammarComposition.Add("Article Adjective Noun");
        }

        public override bool ProcessComponentsIntoGrammar(string stackString)
        {
            bool isComponent = false;
            for (int j = 0; j < grammarComposition.Count && !isComponent; j++)
            {
                if (stackString.Contains(grammarComposition.ElementAt(j)))
                {
                    isComponent = true;
                }
            }

            return isComponent;
        }

        public override string ToString()
        {
            return "NounPhrase";
        }
    }
}
