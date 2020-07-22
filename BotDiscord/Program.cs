using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using System.Configuration;

namespace BotDiscord
{
    class Program
    {
        private DiscordSocketClient _client;
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();
        public async Task MainAsync()
        {

            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info
            });

            _client.Log += Log;

            await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("SECRET_TOKEN"));
            await _client.StartAsync();
            _client.MessageUpdated += MessageUpdated;
            _client.MessageReceived += MessageReceived;
            await Task.Delay(-1);
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            // If the message was not in the cache, downloading it will result in getting a copy of `after`.
            var message = await before.GetOrDownloadAsync();
            Console.WriteLine($"{message} -> {after}");
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Content.ToLower() == "!salut")
            {
                await message.Channel.SendMessageAsync("Salut " + message.Author.Mention + " !");
            }

            if (message.Content.ToLower() == "!ping")
            {
                await message.Channel.SendMessageAsync("pong");
            }

            if (message.Content.ToLower() == "!help")
            {
                string helpMessage = "Voici la liste des commandes disponible pour le moment :\n" +
                    "- '!salut':Saluer le bot !\n" +
                    "- '!ping':Le bot répondra 'pong'\n" +
                    "- '!chesse': Chesse !!!!";

                await message.Channel.SendMessageAsync(helpMessage);
            }

            if (message.Content.ToLower() == "!chesse")
            {
                var emoji = new Emoji("\uD83E\uDDC0");
                await message.AddReactionAsync(emoji);
            }
        }
    }
}
