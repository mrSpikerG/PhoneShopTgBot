using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Controllers.Helpers;

namespace TgBot.Models.Commands.Client {
    public class ShowByNameCommand : ShowPhonesCommand {
        public override List<string> getAliases() {
            return new List<string>() { "name", "название" };
        }

        public override string getName() {
            return "ShowByName";
        }

        public override string getUsage() {
            return "/name <product_name> (название)";
        }

        public override async void execute(Message message) {

            decimal mincost, maxcost;
            
            string args = message.Text.ToLower().Replace("/name", "").Replace("/название", "").Trim();
            string name;
            try {
                name = args.Split(' ')[0];
            }catch {
                await BotHelper.Client.SendTextMessageAsync(message.From.Id, $"Некорректное значение");
                return;
            }

            this.Context.Phones.ToList().ForEach(async x => {
                if (x.Name.ToLower().Contains(name)) {
                    showPhonesCommand(message.From.Id, x);
                }
            });
        }

        

        public override bool isArgumentContains() {
            return true;
        }
    }
}
