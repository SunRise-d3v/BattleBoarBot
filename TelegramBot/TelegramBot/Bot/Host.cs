using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Bot
{
    public class Host
    {
        private TelegramBotClient _bot;
        public Action<ITelegramBotClient, Update>? onMessage;

        public Host(string token)
        {
            _bot = new TelegramBotClient(token);
        }

        public void Start()
        {
            _bot.StartReceiving(Update, Error);
            Console.WriteLine("Bot is started");
        }

        public async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            Console.WriteLine($"I received a message: {update.Message?.Text ?? "[Other]"}");
            onMessage?.Invoke(botClient, update);
            await Task.CompletedTask;
        }

        public async Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken token)
        {
            Console.WriteLine($"Error: {exception.Message}");
            await Task.CompletedTask;
        }
    }
}