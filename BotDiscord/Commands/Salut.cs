using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace BotDiscord.Commands
{
    public class Salut : ModuleBase<SocketCommandContext>
    {
        [Command("Salut")]
        public Task SalutAsync()
        {
           return ReplyAsync($"Salut {Context.Message.Author.Mention} !");
        }
    }
}
