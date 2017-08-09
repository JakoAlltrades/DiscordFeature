using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLanguage.Grammars
{
    public class Preposition : GrammarRule
    {
        public Preposition()
        {
            possibleWords.Add("with");
            possibleWords.Add("after");
            possibleWords.Add("beside");
            possibleWords.Add("over");
        }

        public override bool ProcessComponentsIntoGrammar(string stackString)
        {
            bool isComponent = false;
            //if there is a check do here, this case there is not anythign beneath preposition
            return isComponent;
        }

        public override string ToString()
        {
            return "Preposition";
        }
    }
}
