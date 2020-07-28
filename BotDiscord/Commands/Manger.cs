using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord.Commands;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.Extensions.DependencyInjection;

namespace BotDiscord.Commands
{
    public class Manger : ModuleBase<SocketCommandContext>
    {
        private string[] foodArray;
        [Command("manger")]
        public async Task MangerAsync()
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
                    await ReplyAsync(resFoodVideo);
                }
                index++;
            }
        }
    }
}
