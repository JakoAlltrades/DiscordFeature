using Discord;
using Discord.Commands;
using Discord.Audio;
using System;
using System.IO;
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
        AudioService audio;
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
            helpList.Add("talk");
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
               // x.CustomPrefixHandler = new Func<Message, int>(discord., 0);
                x.PrefixChar = 'J';
                x.AllowMentionPrefix = true;
            });
            discord.UsingAudio(x =>
            {
                // x.CustomPrefixHandler = new Func<Message, int>(discord., 0);
                x.Bitrate = 64;
                x.Channels = 3;
                x.Build();
            });
            commands = discord.GetService<CommandService>();
            audio = discord.GetService<AudioService>();
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
            ProcessSentence();
            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzQ0MTQzOTEyNDEzNDI5NzYx.DGtyIA.y_wBcXzuLsyMEk7utz5awPyz41Y", TokenType.Bot);
            });
        }

        private void ProcessSentence()
        {

            bp.ClearPrevious();
            commands.CreateCommand("talk").Parameter("phrase", ParameterType.Multiple).Do(async (e) =>
            {
                int maxArgs = e.Args.Count();
                String fullResponse = "";
                for (int i = 0; i < maxArgs; i++)
                {
                    fullResponse = fullResponse + " " + e.GetArg(i);
                }
                string response = bp.StartProcess(fullResponse);
                await e.Channel.SendMessage(response);
            });
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
            commands.CreateCommand("purge").Parameter("phrase", ParameterType.Multiple).Do(async (e) =>
            {
                Message[] messages;
                String  num = e.GetArg(0);
                int numin = Int32.Parse(num);
                Console.WriteLine(num);
                Console.WriteLine(numin);
                messages = await e.Channel.DownloadMessages(numin);
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
                // await e.Server.CurrentUser.VoiceChannel.JoinAudio();
                var server = discord.FindServers(e.Server.ToString()).FirstOrDefault();
                var channel = server.FindChannels(e.User.VoiceChannel.ToString(),ChannelType.Voice).FirstOrDefault();
                Console.WriteLine("server"+server);
                Console.WriteLine("channel"+channel);
                var discordVoice = await discord.GetService<AudioService>().Join(channel);
                Byte[] bytes =File.ReadAllBytes(currentDir);
                await discordVoice.OutputStream.ReadAsync(bytes,0,bytes.Length);
                

            });
        }
        private void Log(object sender,LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
