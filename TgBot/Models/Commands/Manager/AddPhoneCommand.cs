using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Controllers;
using TgBot.Controllers.Helpers;
using TgBot.Models.Interface;
using static System.Net.Mime.MediaTypeNames;

namespace TgBot.Models.Commands.Manager {
    public class AddPhoneCommand : ICommand {

        protected PhoneShopContext Context { get; private set; }

        public AddPhoneCommand() {
            Context = new PhoneShopContext();
        }

        public List<string> getAliases() {
            return new List<string>() {"add","добавить" };
        }

        public string getName() {
            return "AddProduct";
        }

        public string getUsage() {
            return "/add <price> <priceType> <categoryId> <producerId> <name> (добавить)";
        }
        
        public async void execute(Message message) {
            try {
                string[] args = message.Text.Split(" ");
                decimal price = decimal.Parse(args[1]);
                string priceType = args[2];
                int categoryId = int.Parse(args[3]);
                int producerId = int.Parse(args[4]);

                int forDelete = 0;
                for (int i = 0; i < 5; i++) {
                    forDelete += args[i].Length + 1;
                }
                string name = message.Text.Remove(0, forDelete);

                this.Context.Phones.Add(new Phone() { Price = price, PriceType = priceType, ProducerId = producerId, CategoryId = categoryId, Name = name });
                this.Context.SaveChangesAsync();

            await BotHelper.Manager.SendTextMessageAsync(message.From.Id, "Товар успешно добавлен");
            }catch {
            await BotHelper.Manager.SendTextMessageAsync(message.From.Id, "Некорректные значения");

            }

        }

        public bool isArgumentContains() {
            return true;
        }
    }
}
