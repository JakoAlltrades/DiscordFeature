using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLanguage.BotGrammar
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
            possibleWords.Add("walks");
            possibleWords.Add("licks");
            possibleWords.Add("poops");
            possibleWords.Add("sits");
            possibleWords.Add("slides");
            possibleWords.Add("falls");
            possibleWords.Add("speaks");
            possibleWords.Add("enchants");
            possibleWords.Add("dives");
            possibleWords.Add("floods");
            possibleWords.Add("cleake");
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
