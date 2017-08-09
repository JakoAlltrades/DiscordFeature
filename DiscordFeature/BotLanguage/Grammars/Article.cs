using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLanguage.Grammars
{
    public class Article : GrammarRule
    {
        public Article()
        {
            possibleWords.Add("a");
            possibleWords.Add("the");
            possibleWords.Add("an");
        }

        public override bool ProcessComponentsIntoGrammar(string stackString)
        {
            bool isComponent = false;
            //if there is a check do here, this case there is not anythign beneath article
            return isComponent;
        }

        public override string ToString()
        {
            return "Article";
        }
    }
}
