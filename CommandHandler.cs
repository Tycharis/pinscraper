using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace PinScraper
{
    internal class CommandHandler
    {
        public const char CommandPrefix = '$';

        private readonly DiscordSocketClient _client;

        private readonly CommandService _commands;

        public CommandHandler(DiscordSocketClient client, CommandService commands)
        {
            _client = client;
            _commands = commands;
        }

        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModuleAsync<ScrapeModule>(null).ConfigureAwait(false);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            if (!(messageParam is SocketUserMessage message))
            {
                return;
            }

            int argPos = 0;

            if (!(message.HasCharPrefix(CommandPrefix, ref argPos) ||
                  message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
            {
                return;
            }

            var context = new SocketCommandContext(_client, message);

            await _commands.ExecuteAsync(context, argPos, null).ConfigureAwait(false);
        }
    }
}
