using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace PinScraper
{
    internal class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;

        public CommandHandler(DiscordSocketClient client, CommandService commands)
        {
            _commands = commands;
            _client = client;
        }

        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModuleAsync(typeof(ScrapeModule), null);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            if (!(messageParam is SocketUserMessage message)) return;
            
            int argPos = 0;
            
            if (!(message.HasCharPrefix('$', ref argPos) ||
                  message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;

            SocketCommandContext context = new SocketCommandContext(_client, message);
            
            await _commands.ExecuteAsync(context, argPos, null);
        }
    }
}