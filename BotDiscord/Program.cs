using System;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;

using Discord;
using Discord.WebSocket;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace BotDiscord
{
    class Program
    {
        private DiscordSocketClient _client;
        private string[] foodArray;
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

            if (message.Content.ToLower() == "!manger")
            {
                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = Environment.GetEnvironmentVariable("GOOGLE_API_KEY"),
                    ApplicationName = this.GetType().ToString()
                });

                foodArray = new string[] { "Sushi", "Burger", "Ramen", "Tacos", "Jambon Beurre" };

                int randomFood = new Random().Next(foodArray.Length);
                string food = foodArray[randomFood];

                var searchListRequest = youtubeService.Search.List("snippet");
                searchListRequest.Q = food;
                searchListRequest.MaxResults = 50;

                var searchListResponse = await searchListRequest.ExecuteAsync();

                int randomIndex = new Random().Next(50);
                int index = 0;

                foreach (var searchResult in searchListResponse.Items)
                {
                    if (index == randomIndex && searchResult.Id.Kind == "youtube#video")
                    {
                            string resFoodVideo = "https://www.youtube.com/watch?v=" + searchResult.Id.VideoId;
                            await message.Channel.SendMessageAsync(resFoodVideo);
                    }
                    index ++;
                }
            }
        }
    }
}
