using Discord;
using Discord.Commands;
using Discord.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotLanguage;

namespace DiscordFeature
{
    public class MyBot
    {
        DiscordClient discord;
        CommandService commands;
        List<string> helpList;
        BotProcessor bp = new BotProcessor();
        List<string> danks;
        public MyBot()
        {
            danks = new List<string>();
            helpList = new List<string>();
            danks.Add("\\Memes\\f60b0f08fb9fac8319e275e31fc7da55bd021ec2a6e343edf9ffcf7642934020_1.jpg");
            danks.Add("\\Memes\\image1.jpg");
            danks.Add("\\Memes\\image2.jpg");
            danks.Add("\\Memes\\image.jpg");
            danks.Add("\\Memes\\image.png");
            helpList.Add("echo");
           // helpList.Add("talk");
            helpList.Add("Slut");
            helpList.Add("purge");
            helpList.Add("dank");
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });
            discord.UsingCommands(x =>
            {
                x.PrefixChar = 'J';
                x.AllowMentionPrefix = true;
            });
           commands = discord.GetService<CommandService>();
            commands.CreateCommand("Slut")
                .Do(async (e) =>
                {
                   await e.Channel.SendMessage("Suck it fagot");
                });
            RegisterImages();
            RegisterPurgeCommand();
            RegisterEcho();
            RegisterHelp();
            RegisterHey();
            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzQ0MTQzOTEyNDEzNDI5NzYx.DGtyIA.y_wBcXzuLsyMEk7utz5awPyz41Y", TokenType.Bot);
            });
            
        }

        private void ProcessSentence()
        {

        }

        private void RegisterImages()
        {
             
            commands.CreateCommand("dank").Do(async (e) =>
            {
                Random rand = new Random();
                int randNum = rand.Next(0, 5);
                string currentDir = Environment.CurrentDirectory;
                string dankPost = danks.ElementAt(randNum);
                currentDir = currentDir.Replace("\\bin\\Debug",dankPost);
                currentDir = currentDir.Replace("'.","");
                await e.Channel.SendFile(currentDir);
            });
        }
        private  void RegisterPurgeCommand()
        {
            commands.CreateCommand("purge").Do(async (e) =>
            {
                Message[] messages;
                messages = await e.Channel.DownloadMessages(100);
               await e.Channel.DeleteMessages(messages);
            });
        }
        private void RegisterEcho()
        {
            commands.CreateCommand("echo").Parameter("phrase",ParameterType.Multiple).Do(async (e) =>
            {
                int maxArgs =  e.Args.Count();
                String fullResponse = "";
                for(int i = 0; i < maxArgs; i++)
                {
                    fullResponse = fullResponse +" "+ e.GetArg(i);
                }
                Message[] messages;
                messages = await e.Channel.DownloadMessages(1);
                await e.Channel.DeleteMessages(messages);
                await e.Channel.SendMessage(fullResponse);
           
            });
        }
        private void RegisterHelp()
        {
            commands.CreateCommand("help").Parameter("phrase", ParameterType.Multiple).Do(async (e) =>
            {
               
                String fullResponse = "";
                for (int i = 0; i < helpList.Count; i++)
                {
                    fullResponse = fullResponse + " " + helpList.ElementAt(i);
                }
                await e.Channel.SendTTSMessage(fullResponse);
                await e.Channel.SendMessage(fullResponse);

            });
        }
        private void RegisterHey()
        {
            commands.CreateCommand("hey").Parameter("phrase", ParameterType.Multiple).Do(async (e) =>
            {
                string currentDir = Environment.CurrentDirectory;
                string dankPost = "\\SoundMemes\\heyyyy.mp3";
                currentDir = currentDir.Replace("\\bin\\Debug", dankPost);
                Console.WriteLine("In hey");
                await e.Channel.JoinAudio();
                ///await e.Channel.SendMessage(currentDir);
                

            });
        }
        private void Log(object sender,LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
