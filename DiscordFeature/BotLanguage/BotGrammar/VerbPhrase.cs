using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLanguage.BotGrammar
{
    public class VerbPhrase : GrammarRule
    {
        public VerbPhrase()
        {
            possibleWords = null;
            grammarComposition.Add("Verb NounPhrase");
            grammarComposition.Add("Verb");
        }

        public override bool ProcessComponentsIntoGrammar(string stackString)
        {
            bool succsess = false;
            List<string> stackWords = stackString.Split(' ').ToList();
            List<string> ruleWords;
            for (int j = 0; j < grammarComposition.Count && !succsess; j++)
            {
                ruleWords = grammarComposition[j].Split(' ').ToList();
                bool KeepLookingFor = true;
                for (int h = 0; h < ruleWords.Count; h++)
                {
                    for (int k = 0; k < stackWords.Count && KeepLookingFor; k++)
                    {
                        if (stackWords[k] == ruleWords[h])
                        {
                            KeepLookingFor = false;
                            ruleWords.Remove(ruleWords[h]);
                            h--;
                        }
                    }
                    KeepLookingFor = true;
                }
                if (ruleWords.Count == 0)
                {
                    succsess = true;
                }
            }
            return succsess;
        }

        public override string ToString()
        {
            return "VerbPhrase";
        }
    }
}
