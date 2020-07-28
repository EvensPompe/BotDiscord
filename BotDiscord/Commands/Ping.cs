using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;


namespace BotDiscord.Commands
{
    public class Ping : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public Task PingAsync()
        {
            return ReplyAsync("Pong");
        }
    }
}
