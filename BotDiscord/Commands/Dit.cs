using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace BotDiscord.Commands
{
    public class Dit : ModuleBase<SocketCommandContext>
    {
        [Command("dit")]
        public Task DitAsync(string citation)
        {
            return ReplyAsync(citation);
        }
    }
}
