using Discord;
using Discord.Commands;
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
        BotProcessor bp = new BotProcessor();
        public MyBot()
        {
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
            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzQ0MTQzOTEyNDEzNDI5NzYx.DGtyIA.y_wBcXzuLsyMEk7utz5awPyz41Y", TokenType.Bot);
            });
            
        }
        private void RegisterImages()
        {
            String random = RandomImage();
             
            commands.CreateCommand("dank").Do(async (e) =>
            {
                await e.Channel.SendFile(random);
            });
        }
        private String RandomImage()
        {
            String path = "";
            Random rand = new Random();
            string currentDir = Environment.CurrentDirectory;
            int randNum = rand.Next(0,5);
            switch (randNum)
            {
                case 0:
                    path =currentDir+"\\Memes\\f60b0f08fb9fac8319e275e31fc7da55bd021ec2a6e343edf9ffcf7642934020_1.jpg";
                    break; 
                case 1:
                    path = currentDir +"\\Memes\\image(1).jpg";
                    break;
                case 2:
                    path = currentDir + "\\Memes\\image(2).jpg";
                    break;
                case 3:
                    path = currentDir + "\\Memes\\image.jpg";
                    break;
                case 4:
                    path = currentDir + "\\Memes\\image.png";
                    break;
                default:
                    Console.WriteLine("Broke asf");
                    break;
            }

            return path;
        }

        private void Log(object sender,LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
