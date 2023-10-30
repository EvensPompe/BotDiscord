using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace BotDiscord.Commands.Roles
{
    public class GetRoles : ModuleBase<SocketCommandContext>
    {
        [Command("roles")]
        public Task GetRolesAsync()
        {
            var roles = Context.Guild.Roles;
            var user = Context.User;
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle("Les rôles du serveur sont :");
            foreach(var role in roles)
            {
                if(role != null)
                {
                    embed.AddField($"{role}", $"{role}", false);
                }
            }
            embed.WithColor(Color.Blue);
            return ReplyAsync("", false, embed.Build());
        }
    }
}
