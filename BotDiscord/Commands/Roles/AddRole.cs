using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using Discord;
using Discord.Commands; 

namespace BotDiscord.Commands.Roles
{
    public class AddRole: ModuleBase<SocketCommandContext>
    {
        [Command("addrole")]
        public async Task AddRoleAsync(string roleAsked)
        {
            var user = Context.User;
            var role = Context.Guild.Roles.FirstOrDefault(r => r.Name == roleAsked);
            if (user != null && !user.IsBot)
            {
                if (role == null)
                {
                    await ReplyAsync("Le rôle n'existe pas ou a été supprimé !");
                }
                else
                {
                    if(role.Name != "Black Bot" || role.Name != "@everyone")
                    {
                        if (!(user as IGuildUser).Guild.Roles.Contains(role))
                        {
                            await (user as IGuildUser).AddRoleAsync(role);
                            await ReplyAsync($"Vous avez ajouté le rôle: {roleAsked} !");
                        }
                        else
                        {
                            await ReplyAsync($"Vous avez déjà ce rôle !");
                        }
                    }
                    else
                    {
                        await ReplyAsync("Vous ne pouvez pas ajouter ce rôle !");
                    }
                }
            }
            else
            {
                await ReplyAsync("L'utilisateur n'existe pas ou vous êtes un bot !");
            }
        }
    }
}
