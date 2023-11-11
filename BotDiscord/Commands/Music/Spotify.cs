using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

using Discord.Commands;
using Discord;

using Music;
using System.Linq;

namespace BotDiscord.Commands
{
    public class Artist : ModuleBase<SocketCommandContext>
    {
        private HttpClient client = new HttpClient();
        private HttpResponseMessage response;
        private string spotifyBaseUrl = "https://api.spotify.com/v1";
        private string spotifyAccountUrl = "https://accounts.spotify.com/api";

        private async Task<AuthToken> GetToken()
        {
            string clientId = Environment.GetEnvironmentVariable("SPOTIFY_CLIENT_ID");
            string clientSecretId = Environment.GetEnvironmentVariable("SPOTIFY_CLIENT_SECRET");
            List<KeyValuePair<string, string>> args = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            };
            HttpContent content = new FormUrlEncodedContent(args);
            string basicToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecretId}"));
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {basicToken}");
            response = await client.PostAsync($"{spotifyAccountUrl}/token", content);
            Stream strToken = await response.Content.ReadAsStreamAsync();
            AuthToken token = await JsonSerializer.DeserializeAsync<AuthToken>(strToken);
            return token;
        }

        [Command("getartist")]
        public async Task GetArtistAsync(string artistName)
        {
            AuthToken authToken = await GetToken();
            string bearerToken = authToken.access_token;
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerToken}");
            response = await client.GetAsync($"{spotifyBaseUrl}/search?&q={artistName}&type=artist");
            Stream content = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<ArtistResult>(content);
            foreach (var item in result?.artists.items)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithTitle(item.name);
                List<Embed> embeds = new List<Embed>();
                List<EmbedFieldBuilder> embedFields = new List<EmbedFieldBuilder>();
                if (item.genres.Any())
                {
                    var embedField = new EmbedFieldBuilder();
                    embedField.WithName("Genres");
                    embedField.WithValue(string.Join(", ", item.genres));
                    embedFields.Add(embedField);
                }
                if (item.GetType().GetProperty("followers") != null)
                {
                    var embedField = new EmbedFieldBuilder();
                    embedField.WithName("Followers");
                    embedField.WithValue(item.followers.total.ToString());
                    embedField.WithIsInline(true);
                    embedFields.Add(embedField);
                }
                if (item.GetType().GetProperty("popularity") != null)
                {
                    var embedField = new EmbedFieldBuilder();
                    embedField.WithName("popularity");
                    embedField.WithValue($"{item.popularity}%");
                    embedField.WithIsInline(true);
                    embedFields.Add(embedField);
                }
                embed.WithFields(embedFields);
                embed.WithUrl(item.external_urls.spotify);
                embeds.Add(embed.Build());
                var image = item.images[0];
                EmbedBuilder imageEmbed = new EmbedBuilder();
                imageEmbed.WithUrl(item.external_urls.spotify);
                imageEmbed.WithImageUrl(image.url);
                embeds.Add(imageEmbed.Build());
                await ReplyAsync(embeds: embeds.ToArray());
            }
            return;
        }
    }
}