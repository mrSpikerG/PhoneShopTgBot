using Telegram.Bot.Types;
using TgBot.Controllers.Helpers;
using TgBot.Controllers;
using Telegram.Bot;
using TgBot.Models.Interface;

namespace TgBot.Models.Commands.Manager {
    public class RemovePhoneCommand : ICommand {

        protected PhoneShopContext Context { get; private set; }

        public RemovePhoneCommand() {
            Context = new PhoneShopContext();
        }

        public List<string> getAliases() {
            return new List<string>() { "remove", "удалить" };
        }

        public string getName() {
            return "RemoveProduct";
        }

        public string getUsage() {
            return "/remove <phoneId> (удалить)";
        }

        public async void execute(Message message) {
            try {
                string[] args = message.Text.Split(" ");
                int phoneId = int.Parse(args[1]);

                if(this.Context.Phones.Any(x => x.Id == phoneId)) {
                    this.Context.Phones.Remove(this.Context.Phones.FirstOrDefault(x => x.Id == phoneId));
                this.Context.SaveChangesAsync();
                }


                await BotHelper.Manager.SendTextMessageAsync(message.From.Id, "Товар успешно удален");
            } catch {
                await BotHelper.Manager.SendTextMessageAsync(message.From.Id, "Некорректные значения");

            }

        }

        public bool isArgumentContains() {
            return true;
        }
    }
}
