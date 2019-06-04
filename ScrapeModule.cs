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
                IEnumerable<Task> deleteTask = messageList.Select(message => message.DeleteAsync());

                await Task.WhenAll(deleteTask);
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

            IReadOnlyCollection<RestMessage> pinnedMessages = await channel.GetPinnedMessagesAsync();

            await Program.Log(
                new LogMessage(
                    LogSeverity.Debug,
                    "Scrape",
                    $"Number of pins: {pinnedMessages.Count}"));

            IEnumerable<Task<RestUserMessage>> sendMessageTasks = pinnedMessages
                .Select(message =>
                    Context.Message.Channel.SendMessageAsync(embed: GetEmbedFromMessage(message)));

            await Task.WhenAll(sendMessageTasks);
        }

        private Embed GetEmbedFromMessage(RestMessage message)
        {
            return new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder
                {
                    IconUrl = message.Author.GetAvatarUrl(),
                    Name = $"{message.Author.Username}#{message.Author.Discriminator}"
                },
                Color = GetRandomColor(),
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
                ImageUrl = message.Attachments.Count != 0
                        ? message.Attachments.ElementAt(0).Url
                        : string.Empty,
                Timestamp = message.Timestamp,
                Title = "Pinned Message",
                Url = message.GetJumpUrl(),
                Footer = new EmbedFooterBuilder
                {
                    Text = message.Channel.Name,
                    IconUrl = Context.Guild.IconUrl
                }
            }.Build();
        }

        private static Color GetRandomColor()
        {
            var random = new Random();

            const int INCLUSIVE_MIN_HEX = 0;
            const int EXCLUSIVE_MAX_HEX = 256;

            return new Color(
                random.Next(INCLUSIVE_MIN_HEX, EXCLUSIVE_MAX_HEX),
                random.Next(INCLUSIVE_MIN_HEX, EXCLUSIVE_MAX_HEX),
                random.Next(INCLUSIVE_MIN_HEX, EXCLUSIVE_MAX_HEX));
        }
    }
}
