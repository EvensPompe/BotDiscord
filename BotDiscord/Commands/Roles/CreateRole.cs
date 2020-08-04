using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using Discord.Commands;



namespace BotDiscord.Commands.Roles
{
    public class CreateRole : ModuleBase<SocketCommandContext>
    {
        [Command("createrole")]
        public async Task CreateRoleAsync(string roleWanted)
        {
            var roles = Context.Guild.Roles;
            var role = roles.FirstOrDefault(v => v.Name == roleWanted);
            if (role == null)
            {
                await Context.Guild.CreateRoleAsync(roleWanted,null,null,false,null);
                await ReplyAsync($"Le rôle {roleWanted} a été créé !");
            }
            else
            {
                await ReplyAsync("Le rôle existe déjà !");
            }
        }
    }
}
