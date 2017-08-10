using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLanguage.Grammars
{
    public class Adjective : GrammarRule
    {

        public Adjective()
        {
            possibleWords.Add("large");
            possibleWords.Add("small");
        }

        public override bool ProcessComponentsIntoGrammar(string stackString)
        {
            throw new NotImplementedException();
        }
    }
}
