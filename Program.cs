using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace PinScraper
{
    internal class Program
    {
        private DiscordSocketClient _client;

        private CommandService _service;

        private CommandHandler _handler;

        private static void Main(string[] args)
        {
            if (!args.Any())
            {
                throw new ArgumentException("No token provided. It should be the first argument");
            }

            new Program().MainAsync(args[0]).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async Task MainAsync(string token)
        {
            _client = new DiscordSocketClient();
            _service = new CommandService();
            _handler = new CommandHandler(_client, _service);

            _client.Log += Log;

            await _handler.InstallCommandsAsync().ConfigureAwait(false);

            await _client.LoginAsync(TokenType.Bot, token).ConfigureAwait(false);
            await _client.StartAsync().ConfigureAwait(false);

            await Task.Delay(-1).ConfigureAwait(false);
        }

        public static Task Log(LogMessage message)
        {
            Console.WriteLine(message);

            return Task.CompletedTask;
        }
    }
}
