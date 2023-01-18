using System.Linq.Expressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Controllers;
using TgBot.Controllers.Helpers;

namespace TgBot.Models.Commands.Manager {
    public class SendMessageCommand : Interface.ICommand {

        protected PhoneShopContext Context { get; private set; }

        public SendMessageCommand() {
            Context = new PhoneShopContext();
        }

        public virtual List<string> getAliases() {
            return new List<string>() { "send", "отправить" };
        }
        public virtual string getName() {
            return "SendMessage";
        }

        public virtual string getUsage() {
            return "/send <userId>(optional `all`) <text> (отправить)";
        }

        public virtual async void execute(Message message) {
            try {
                string id = message.Text.Split(" ")[1];
                string text = message.Text.Remove(0, message.Text.IndexOf(id) + id.Length);

                if (id.ToLower().Equals("all")) {
                    foreach (var user in this.Context.Clients) {
                        await BotHelper.Client.SendTextMessageAsync(user.TelegramId, text);
                    }
                } else {
                    await BotHelper.Client.SendTextMessageAsync(this.Context.Clients.FirstOrDefault(x => x.UserId == int.Parse(id)).TelegramId, text);
                }

            } catch {
                return;
            }
        }

        public virtual bool isArgumentContains() {
            return true;
        }

    }
}
