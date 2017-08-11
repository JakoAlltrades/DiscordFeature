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
            possibleWords.Add("tiny");
            possibleWords.Add("angry");
            possibleWords.Add("worried");
            possibleWords.Add("annoyed");
            possibleWords.Add("ferociously");
            possibleWords.Add("happy");
            possibleWords.Add("gentle");
            possibleWords.Add("brave");
            possibleWords.Add("high");
            possibleWords.Add("low");
            possibleWords.Add("colossal");
            possibleWords.Add("easy-going");
            possibleWords.Add("fearful");
            possibleWords.Add("scared");
            possibleWords.Add("honest");
        }

        public override bool ProcessComponentsIntoGrammar(string stackString)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "Adjective";
        }
    }
}
