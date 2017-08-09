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
        BotProcessor bp = new BotProcessor();
        public MyBot()
        {
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });
            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzQ0MTQzOTEyNDEzNDI5NzYx.DGtyIA.y_wBcXzuLsyMEk7utz5awPyz41Y", TokenType.Bot);
            });
            
           
        }


        private void Log(object sender,LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
