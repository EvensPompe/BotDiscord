using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace BotDiscord.Commands
{
    public class Cheese : ModuleBase<SocketCommandContext>
    {
        [Command("cheese")]
        public async Task CheeseAsync()
        {
            var emoji = new Emoji("\uD83E\uDDC0");
            await Context.Message.AddReactionAsync(emoji);
        }
    }
}
