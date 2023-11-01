using System.Threading.Tasks;
using System.Linq;

using Discord.Commands;



namespace BotDiscord.Commands.Roles
{
    public class DeleteRole : ModuleBase<SocketCommandContext>
    {
        [Command("deleterole")]
        public async Task DeleteRoleAsync(string roleWanted)
        {
            var roles = Context.Guild.Roles;
            var role = roles.FirstOrDefault(v => v.Name == roleWanted);
            if (role != null)
            {
                await role.DeleteAsync();
                await ReplyAsync($"Le rôle {roleWanted} a été supprimé !");
            }
            else
            {
                await ReplyAsync("Le rôle n'existe pas !");
            }
        }
    }
}
