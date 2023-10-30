using System;
using System.Threading.Tasks;
using System.Net.Http;

using Newtonsoft.Json.Linq;

using Discord.Commands;

namespace BotDiscord.Commands
{
    public class Meme: ModuleBase<SocketCommandContext>
    {
        private HttpClient client;
        private HttpResponseMessage response;
        private HttpContent content;
        private string url = "https://tenor.googleapis.com/v2";
        [Command("Meme")]
        public async Task MemeAsync(string searchMeme)
        {
            try
            {
                client = new HttpClient();
                response = await client.GetAsync($"{url}/search?q={searchMeme}&key={Environment.GetEnvironmentVariable("TENOR_API_KEY")}&limit=50");
                content =  response.Content;
                var data = await content.ReadAsStringAsync();
                if (content != null)
                {
                    var dataJson = JObject.Parse(data);
                    int randomIndex = new Random().Next(50);
                    int index = 0;

                    foreach (var meme in dataJson["results"])
                    {
                        if (index == randomIndex)
                        {
                            await ReplyAsync(meme["url"].ToString());
                        }
                        index++;
                    }
                }
            }
            catch(Exception error)
            {
                Console.WriteLine(error);
            }
        }
    }
}
