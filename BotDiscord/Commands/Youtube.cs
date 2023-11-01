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
        public async Task YoutubeAsync(string ytbValue)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = Environment.GetEnvironmentVariable("GOOGLE_API_KEY"),
                ApplicationName = GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = ytbValue;
            searchListRequest.MaxResults = 50;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            int randomIndex = new Random().Next(50);
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
