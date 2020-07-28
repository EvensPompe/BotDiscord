using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace BotDiscord.Commands
{
    public class Chesse : ModuleBase<SocketCommandContext>
    {
        [Command("chesse")]
        public async Task ChesseAsync()
        {
            var emoji = new Emoji("\uD83E\uDDC0");
            await Context.Message.AddReactionAsync(emoji);
        }
    }
}
