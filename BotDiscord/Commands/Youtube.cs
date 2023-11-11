using System;
using System.Threading.Tasks;

using Discord.Commands;

using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace BotDiscord.Commands
{
    public class Youtube : ModuleBase<SocketCommandContext>
    {
        [Command("youtube")]
        public async Task YoutubeAsync(string search, string limit = "10")
        {
            int limitParsed;
            if (!int.TryParse(limit, out limitParsed))
            {
                await ReplyAsync("Utilisez un nombre pour la limite s'il vous plaît");
                return;
            }
            if(limitParsed < 10)
            {
                await ReplyAsync("Mettez une limite supérieure ou égale à 10 s'il vous plaît");
                return;
            }
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = Environment.GetEnvironmentVariable("GOOGLE_API_KEY"),
                ApplicationName = GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = search;
            searchListRequest.MaxResults = limitParsed;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            int randomIndex = new Random().Next(limitParsed);
            int index = 0;

            foreach (var searchResult in searchListResponse.Items)
            {
                if (index == randomIndex && searchResult.Id.Kind == "youtube#video")
                {
                    string resYoutube = "https://www.youtube.com/watch?v=" + searchResult.Id.VideoId;
                    await ReplyAsync(resYoutube);
                }
                index++;
            }
        }
    }
}
