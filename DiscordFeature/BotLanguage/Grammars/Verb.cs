using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLanguage.Grammars
{
    public class Verb : GrammarRule
    {
        public Verb()
        {
            possibleWords.Add("bites");
            possibleWords.Add("chases");
            possibleWords.Add("attacks");
            possibleWords.Add("runs");
            possibleWords.Add("soars");
            possibleWords.Add("jumps");
        }

        public override bool ProcessComponentsIntoGrammar(string stackString)
        {
            bool isComponent = false;
            //if there is a check do here, this case there is not anythign beneath verb
            return isComponent;
        }

        public override string ToString()
        {
            return "Verb";
        }
    }
}
