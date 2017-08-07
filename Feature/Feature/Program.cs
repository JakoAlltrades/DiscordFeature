using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< HEAD
using Discord;
using Discord.Commands;
=======
using Discord.Net;
using Discord;
>>>>>>> origin/master

namespace Feature
{
    public class Program
    {
        public static void Main(string[] args)
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