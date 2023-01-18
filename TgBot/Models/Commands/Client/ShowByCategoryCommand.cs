using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Controllers.Helpers;

namespace TgBot.Models.Commands.Client
{
    public class ShowByCategoryCommand : ShowPhonesCommand {
        public override List<string> getAliases() {
            return new List<string>() { "category", "категория" };
        }

        public override string getName() {
            return "ShowByCategory";
        }

        public override string getUsage() {
            return "/category <category_name> (категория)";
        }

        public override async void execute(Message message) {

            decimal mincost, maxcost;
            string args = message.Text.ToLower().Replace("/category", "").Replace("/категория", "").Trim();
            string name;
            try {
                name = args.Split(' ')[0];
            } catch {
                await BotHelper.Client.SendTextMessageAsync(message.From.Id, $"Некорректное значение");
                return;
            }

            this.Context.Phones.ToList().ForEach(async x => {
                if (this.Context.Categories.First(y => y.Id == x.CategoryId).Name.ToLower().Contains(name)) {
                    showPhonesCommand(message.From.Id, x);
                }
            });
        }

        public override bool isArgumentContains() {
            return true;
        }
    }
}

