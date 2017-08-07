using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Feature
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Start();
        }
        private IDiscordClient client;
        public void Start()
        {
            client = new IDiscordClient(x =>
            {

            });
        }
    }
}
