using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLanguage.Grammars
{
    public class Noun : GrammarRule
    {
        public Noun()
        {
            possibleWords.Add("dog");
            possibleWords.Add("cat");
            possibleWords.Add("fish");
            possibleWords.Add("person");
            possibleWords.Add("man");
            possibleWords.Add("woman");
            possibleWords.Add("puppy");
            possibleWords.Add("octopus");
            possibleWords.Add("bird");
            possibleWords.Add("sky");
            possibleWords.Add("sea");
            possibleWords.Add("John");
            possibleWords.Add("Matt");
            possibleWords.Add("Brandon");
            possibleWords.Add("Biscut");
            possibleWords.Add("baby");
            possibleWords.Add("oil");
            possibleWords.Add("baby-oil");
            possibleWords.Add("daddy");
            possibleWords.Add("tortilla");
        }

        public override bool ProcessComponentsIntoGrammar(string stackString)
        {
            bool isComponent = false;
            //if there is a check do here, this case there is not anythign beneath noun
            return isComponent;
        }

        public override string ToString()
        {
            return "Noun";
        }
    }
}
