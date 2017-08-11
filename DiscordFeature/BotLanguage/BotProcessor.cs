using BotLanguage.Grammars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLanguage
{
    public class BotProcessor
    {
        List<string> enteredPhrase;
        List<string> modPhrase;
        Sentence sentence = null;
        List<GrammarRule> stack = new List<GrammarRule>();
        List<string> genPhrases = new List<string>();
            List<string> previousPhrase = new List<string>();
            string returnPhrase = null;
            bool sentenceFound = false;
            Enum curState;
            int wordCount;
            int curCount;
            bool seekNPAfterPrep = false;
            bool seekNPAfterVerb = false;
            private KeyValuePair<string, GrammarRule>[] GrammarRuleStrings = { new KeyValuePair<string, GrammarRule>("Noun", new Noun()), new KeyValuePair<string, GrammarRule>("Article", new Article()), new KeyValuePair<string, GrammarRule>("Verb", new Verb()), new KeyValuePair<string, GrammarRule>("NounPhrase", new NounPhrase()), new KeyValuePair<string, GrammarRule>("VerbPhrase", new VerbPhrase()), new KeyValuePair<string, GrammarRule>("Sentence", new Sentence()), new KeyValuePair<string, GrammarRule>("Preposition", new Preposition()) };
            enum states
            {
                SeekArticle,
                SeekNounOrAdjective,
                SeekNoun,
                SeekPrepositionOrVerb,
                SeekArticleOrNull,
                SeekVPNoun,
                SeekVPNounOrAdjective,
                SeekVPPrep
            }



            public BotProcessor()
            {
                enteredPhrase = null;
                modPhrase = null;
                curState = states.SeekArticle;
                genPhrases.Add("Does this have signicficance to you?");
                genPhrases.Add("Why do you do this to yourself?");
                genPhrases.Add("Do you enjoy dealing with your problems?");
                genPhrases.Add("Yeah, do you like that?");
                genPhrases.Add("What a weather are we are having?");
            genPhrases.Add("Wow, that sounds interesting. Elobrate.");
            }

            public string GenerateStackString()
            {
                string result = "";
                for (int j = 0; j < stack.Count; j++)
                {
                    result += stack.ElementAt(j).ToString();
                    if (j != stack.Count - 1)
                    {
                        result += " ";
                    }
                }
                return result;
            }

            private List<GrammarRule> GenerateListFromStringStack(string curStack)
            {
                List<GrammarRule> newStack = new List<GrammarRule>();
                List<string> stringStack = curStack.Split(' ').ToList();
                for (int j = 0; j < stringStack.Count; j++)
                {
                    if (GrammarRuleStrings.Select(x => x.Key).Contains(stringStack.ElementAt(j)))
                    {
                        newStack.Add(GrammarRuleStrings.Where(x => x.Key == stringStack[j]).Select(x => x.Value).Single());
                    }
                }
                return newStack;
            }

            public string StartProcess(string phrase)
            {
                phrase = phrase.ToLower();
                enteredPhrase = phrase.Split(' ').ToList();
                modPhrase = enteredPhrase;
                wordCount = enteredPhrase.Count;
                StartMachineProcess();
                ClearPrevious();
                if (returnPhrase != null)
                {
                    //Console.WriteLine(returnPhrase);
                }
                return returnPhrase;
            }

        public void ClearPrevious()
        {
            sentenceFound = false;
            curState = states.SeekArticle;
            stack = new List<GrammarRule>();
            sentence = null;
        }

            private bool CheckIfSucessfulStack(string stackString)
            {
                if (new Sentence().ProcessComponentsIntoGrammar(stackString) && curCount == wordCount - 1)
                {
                    RemoveGrammarCompostionFromStack(stackString, new Sentence());
                    sentenceFound = true;
                    sentence = (Sentence)stack.ElementAt(0);
                }
                return sentenceFound;
            }

            private string UpdateStack(string stringStack)
            {
                bool changedStack = false;
                string newStack = stringStack;
                do
                {
                    if (changedStack)
                    {
                        changedStack = false;
                    }
                    if (!seekNPAfterPrep && new NounPhrase().ProcessComponentsIntoGrammar(newStack))
                    {
                        RemoveGrammarCompostionFromStack(newStack, new NounPhrase());
                        newStack = GenerateStackString();
                        changedStack = true;
                    }
                    else if (new VerbPhrase().ProcessComponentsIntoGrammar(newStack) && !seekNPAfterVerb)
                    {
                        RemoveGrammarCompostionFromStack(newStack, new VerbPhrase());
                        newStack = GenerateStackString();
                        changedStack = true;
                    }
                } while (changedStack);
                return newStack;
            }

            private void RemoveGrammarCompostionFromStack(string stringStack, GrammarRule rule)
            {
                string curStack = stringStack;
                bool foundMatch = false;
                for (int j = 0; j < rule.grammarComposition.Count && !foundMatch; j++)
                {
                    List<GrammarRule> underComponents = new List<GrammarRule>();
                    int insertIndex = -1;
                    if (curStack.Contains(rule.grammarComposition[j]))
                    {
                        List<string> compostionGrammars = rule.grammarComposition[j].Split(' ').ToList();
                        foundMatch = true;
                        for (int k = stack.Count - 1; k >= 0 && compostionGrammars.Count > underComponents.Count; k--)
                        {
                            if (compostionGrammars.Contains(stack[k].ToString()))
                            {
                                underComponents.Insert(0, stack[k]);
                                if (compostionGrammars[0] == stack[k].ToString())
                                {
                                    insertIndex = k;
                                }
                            }
                        }
                        rule.components = underComponents.ToList();
                        stack.Insert(insertIndex, rule);
                        for (int c = stack.Count - 1; c >= 0 && underComponents.Count != 0; c--)
                        {

                            if (underComponents.Contains(stack.ElementAt(c)))
                            {
                                underComponents.Remove(stack.ElementAt(c));
                                stack.RemoveAt(c);
                            }
                        }
                    }

                }
            }

            private void StartMachineProcess()
            {
                bool foundSuccess = false;
                for (int j = 0; j < modPhrase.Count && !foundSuccess; j++)
                {
                    curCount = j;
                    ProcessState(modPhrase[j]);
                    string stackString = GenerateStackString();
                    stackString = UpdateStack(stackString);
                    if (CheckIfSucessfulStack(stackString))
                    {
                        foundSuccess = true;
                    }
                }
                if (sentenceFound)
                {
                    returnPhrase = GenerateSenteance(sentence);
                }
                else
                {
                    returnPhrase = GetGenericPhrase();
                }
                PrintReturnPhrase();
            }

        private string GenerateSenteance(Sentence sentence)
        {
            List<GrammarRule> words = sentence.GenerateWords();
            //Dictionary<string, int> WordTypeCount = sentence.GenerateWordCount(words);
            string returnSentence = generateLeadingPhrase();
            string wordsPassed = "";
            int length = 0;
            foreach (GrammarRule rule in words)
            {
                if (rule.GetType().Equals(new Verb().GetType()))
                {
                    wordsPassed += rule.word.Substring(0, rule.word.Length - 1) + " ";
                }
                else
                {
                    wordsPassed += rule.word + " ";
                }
                length = wordsPassed.Length;
            }
            wordsPassed = wordsPassed.Substring(0, length - 1);
            wordsPassed += "?";
            previousPhrase.Add(wordsPassed);
            returnSentence += wordsPassed;
            return returnSentence;
        }

        private string generateLeadingPhrase()
        {
            string[] leadPhrases = { "Who does ", "Where did ", "How did ", "When did " };
            Random r = new Random();
            return leadPhrases[r.Next(leadPhrases.Length)];
        }

        private void PrintReturnPhrase()
            {
                string phrase = "";
                if (returnPhrase != null)
                {
                    int count = 0;
                    foreach (char letter in returnPhrase)
                    {
                        phrase += letter;
                        count++;
                    }
                    Console.WriteLine(phrase);
                }

            }

            private void ProcessState(string word)
            {
                int runTimes = 1;
                List<GrammarRule> checkList = new List<GrammarRule>();
                GrammarRule state = null;
                if (curState.Equals(states.SeekArticle))
                {
                    state = new Article();
                }
                else if(curState.Equals(states.SeekNounOrAdjective))
                {
                    runTimes = 2;
                    checkList.Add(new Noun());
                    checkList.Add(new Adjective());
                }
                else if (curState.Equals(states.SeekArticleOrNull))
                {
                    state = new Article();
                }
                else if (curState.Equals(states.SeekNoun))
                {
                    state = new Noun();
                }
                else if (curState.Equals(states.SeekPrepositionOrVerb))
                {
                    runTimes = 2;
                    checkList.Add(new Preposition());
                    checkList.Add(new Verb());
                }
                else if (curState.Equals(states.SeekVPNoun))
                {
                    state = new Noun();
                }
                else if(curState.Equals(states.SeekVPNounOrAdjective))
                {
                    runTimes = 2;
                    checkList.Add(new Noun());
                    checkList.Add(new Adjective());
                }
                else if (curState.Equals(states.SeekVPPrep))
                {
                    state = new Preposition();
                }

                if (runTimes == 1)
                {
                    if (state.ProcessWordIntoGrammar(word))
                    {
                        stack.Add(state);
                        if (curState.Equals(states.SeekArticle))
                        {
                            curState = states.SeekNounOrAdjective;
                        }
                        else if (curState.Equals(states.SeekNoun))
                        {
                            curState = states.SeekPrepositionOrVerb;
                            if (curCount != wordCount - 1)
                            {
                                seekNPAfterPrep = true;
                            }
                            else
                            {
                                seekNPAfterPrep = false;
                            }
                            if (seekNPAfterVerb)
                            {
                                seekNPAfterVerb = false;
                            }
                        }
                        else if (curState.Equals(states.SeekArticleOrNull))
                        {
                            curState = states.SeekVPNounOrAdjective;
                        }
                        else if (curState.Equals(states.SeekVPNoun))
                        {
                            curState = states.SeekVPPrep;
                            if (curCount != wordCount - 1)
                            {
                                seekNPAfterPrep = true;
                            }
                            else
                            {
                                seekNPAfterVerb = false;
                            }
                        }
                        else if (curState.Equals(states.SeekVPPrep))
                        {
                            curState = states.SeekArticle;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < checkList.Count; j++)
                    {
                        state = checkList.ElementAt(j);
                        if (state.ProcessWordIntoGrammar(word))
                        {
                            stack.Add(state);
                            if (curState.Equals(states.SeekPrepositionOrVerb))
                            {
                                if (state.GetType().Equals(new Preposition().GetType()))
                                {
                                    curState = states.SeekArticle;

                                }
                                else if (state.GetType().Equals(new Verb().GetType()))
                                {
                                    curState = states.SeekArticleOrNull;
                                    seekNPAfterPrep = false;
                                    if (curCount != wordCount - 1)
                                    {
                                        seekNPAfterVerb = true;
                                    }
                                }
                            }
                            else if(curState.Equals(states.SeekNounOrAdjective))
                            {
                                if(state.GetType().Equals(new Noun().GetType()))
                                {
                                    curState = states.SeekPrepositionOrVerb;
                                    if (curCount != wordCount - 1)
                                    {
                                        seekNPAfterPrep = true;
                                    }
                                    else
                                    {
                                        seekNPAfterPrep = false;
                                    }
                                    if (seekNPAfterVerb)
                                    {
                                        seekNPAfterVerb = false;
                                    }
                                }
                                else if(state.GetType().Equals(new Adjective().GetType()))
                                {
                                    curState = states.SeekNoun;
                                }
                            }
                            else if(curState.Equals(states.SeekVPNounOrAdjective))
                            {
                                if(state.GetType().Equals(new Noun().GetType()))
                                {
                                    curState = states.SeekVPPrep;
                                    if (curCount != wordCount - 1)
                                    {
                                        seekNPAfterPrep = true;
                                    }
                                    else
                                    {
                                        seekNPAfterVerb = false;
                                    }
                                }
                                else if(state.GetType().Equals(new Adjective().GetType()))
                                {
                                    curState = states.SeekVPNoun;
                                }
                            }
                        }
                    }
                }
            }

        private string GetGenericPhrase()
        {
            string phrase = null;
            if (previousPhrase.Count == 0)
            {
                Random r = new Random();
                int rng = r.Next(genPhrases.Count);
                phrase = genPhrases.ElementAt(rng);
            }
            else
            {
                phrase = generateLeadingPhrase() + previousPhrase.Last();
                previousPhrase.RemoveAt(previousPhrase.Count - 1);
            }
            return phrase;
        }
    }
    }
