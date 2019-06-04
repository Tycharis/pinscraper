using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;

namespace PinScraper
{
    internal class ScrapeModule : ModuleBase<SocketCommandContext>
    {
        [Command("purge")]
        [RequireUserPermission(GuildPermission.ManageMessages, Group = "admin")]
        [RequireOwner(Group = "admin")]
        [Summary("Purges x messages from the current channel")]
        public async Task PurgeAsync(int quantity)
        {
            IAsyncEnumerable<IReadOnlyCollection<IMessage>> messages = Context.Channel.GetMessagesAsync(quantity + 1);

            await messages.ForEachAsync(async messageList =>
            {
                foreach (IMessage message in messageList)
                {
                    await message.DeleteAsync();
                }
            });
        }

        [Command("scrape", RunMode = RunMode.Async)]
        [Summary("Scrapes pins from the channel executed in")]
        public async Task ScrapeAsync()
        {
            await ScrapeAsync(Context.Channel);
        }

        [Command("scrape", RunMode = RunMode.Async)]
        [Summary("Scrapes pins from a specified channel")]
        public async Task ScrapeAsync(ISocketMessageChannel channel)
        {
            //TODO Rate limiting is a thing and fuck Discord

            IReadOnlyCollection<RestMessage> pinnedMessages = channel.GetPinnedMessagesAsync().Result;

            await Program.Log(new LogMessage(LogSeverity.Debug, "Scrape", $"Number of pins: {pinnedMessages.Count}"));

            foreach (RestMessage message in pinnedMessages)
            {
                Random random = new Random();

                Embed embed = new EmbedBuilder
                {
                    Author = new EmbedAuthorBuilder
                    {
                        IconUrl = message.Author.GetAvatarUrl(),
                        Name = $"{message.Author.Username}#{message.Author.Discriminator}"
                    },
                    Color = new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)),
                    Fields = new List<EmbedFieldBuilder>
                    {
                        new EmbedFieldBuilder
                        {
                            IsInline = false,
                            Name = "Content",
                            Value = message.Content
                        },
                        new EmbedFieldBuilder
                        {
                            IsInline = false,
                            Name = "Attachments",
                            Value = message.Attachments.Count
                        }
                    },
                    ImageUrl = message.Attachments.Count != 0 ? message.Attachments.ElementAt(0).Url : "" ,
                    Timestamp = message.Timestamp,
                    Title = "Pinned Message",
                    Url = message.GetJumpUrl(),
                    Footer = new EmbedFooterBuilder
                    {
                        Text = message.Channel.Name,
                        IconUrl = Context.Guild.IconUrl
                    }
                }.Build();

                await Context.Message.Channel.SendMessageAsync(embed: embed);
            }
        }
    }
}
