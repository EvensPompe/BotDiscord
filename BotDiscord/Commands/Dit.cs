using System.Threading.Tasks;

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
