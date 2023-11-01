using System.Threading.Tasks;
using System.Linq;

using Discord.Commands;



namespace BotDiscord.Commands.Roles
{
    public class UpdateRole : ModuleBase<SocketCommandContext>
    {
        [Command("updaterole")]
        public async Task UpdateRoleAsync(string roleWanted, string newRole)
        {
            var roles = Context.Guild.Roles;
            var role = roles.FirstOrDefault(v => v.Name == roleWanted);
            if (role != null)
            {
                if(roleWanted == newRole)
                {
                    await ReplyAsync("Le nom du rôle est le même !");
                    return;
                }
                await role.ModifyAsync(currentRole => 
                {
                    currentRole.Name = newRole;
                });
                await ReplyAsync($"Le rôle {roleWanted} a été modifié en {newRole} !");
            }
            else
            {
                await ReplyAsync("Le rôle n'existe pas !");
            }
        }
    }
}
