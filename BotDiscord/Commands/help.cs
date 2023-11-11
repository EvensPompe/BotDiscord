using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace BotDiscord.Commands
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public Task HelpAsync()
        {
            
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle("Commandes du bot:");
            embed.AddField("help", "Afficher toutes les commandes disponibles");
            embed.AddField("salut", "Saluer le bot (on reste poli)");
            embed.AddField("ping", "PONG !!!");
            embed.AddField("cheese", "PONG !!!");
            embed.AddField("dit \"citation\"", "Faire dire la citation au bot (Attention au dérapage)!");
            embed.AddField("meme \"meme\" [\"limite\"]", "Afficher un meme aléatoirement, parmi le nombre limite. Si elle n'est pas défini, elle sera égale à 10 par défaut");
            embed.AddField("youtube \"titre\" [\"limite\"]", "Afficher une vidéo youtube aléatoirement, parmi le nombre limite. Si elle n'est pas défini, elle sera égale à 20 par défaut");
            embed.AddField("addrole \"rôle\"", "Ajouter le rôle à l'utilisateur");
            embed.AddField("createrole \"rôle\"", "Créer un nouveau rôle");
            embed.AddField("roles", "Afficher la liste des rôles disponible");
            embed.AddField("updaterole \"ancien rôle\" \"nouveau rôle\"", "Modifier un rôle existant");
            embed.AddField("deleterole \"rôle\"", "Supprimer un rôle existant");
            embed.WithColor(Color.Blue);
            return ReplyAsync("", false, embed.Build());
        }
    }
}
