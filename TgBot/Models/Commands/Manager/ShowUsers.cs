using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using TgBot.Controllers.Helpers;
using TgBot.Controllers;
using Telegram.Bot;

namespace TgBot.Models.Commands.Manager {
    public class ShowUsers : Interface.ICommand {

        protected PhoneShopContext Context { get; private set; }

        public ShowUsers() {
            Context = new PhoneShopContext();
        }

        public virtual List<string> getAliases() {
            return new List<string>() { "users", "пользователи" };
        }
        public virtual string getName() {
            return "ShowUsers";
        }

        public virtual string getUsage() {
            return "/users (пользователи)";
        }

        public virtual async void execute(Message message) {
            Context.Clients.ToList().ForEach(async x => {
                await BotHelper.Manager.SendTextMessageAsync(message.From.Id, $"{x.UserId}. @{x.Username}");
            });
        }

        public virtual bool isArgumentContains() {
            return false;
        }

    }
}
