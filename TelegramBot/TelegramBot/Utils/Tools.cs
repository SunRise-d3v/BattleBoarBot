using Telegram.Bot.Types;

namespace TelegramBot.Utils
{
    public class Tools
    {
        public static long GetChatId(Update update)
            => update.Message?.Chat.Id ?? 1510162893;

        public static string DrawBorder()
            => "═══════════════════════════\n";
    }
}