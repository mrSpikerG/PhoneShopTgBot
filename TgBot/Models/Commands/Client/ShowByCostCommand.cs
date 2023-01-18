using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using TgBot.Controllers;
using TgBot.Controllers.Helpers;
using Telegram.Bot;

namespace TgBot.Models.Commands.Client {
    public class ShowByCostCommand : ShowPhonesCommand {
        public override List<string> getAliases() {
            return new List<string>() { "cost", "цена" };
        }

        public override string getName() {
            return "ShowByCost";
        }

        public override string getUsage() {
            return "/cost <min_cost>-<max_cost> (цена)";
        }

        public override async void execute(Message message) {

            decimal mincost, maxcost;
            string args = message.Text.ToLower().Replace("/cost","").Replace("/цена","").Trim();
            try {
                mincost = decimal.Parse(args.Split("-")[0]);
            } catch {
                mincost = decimal.MinValue;
            }

            try {
                maxcost = decimal.Parse(args.Split("-")[1]);
            } catch {
                maxcost = decimal.MaxValue;
            }

            this.Context.Phones.ToList().ForEach(async x => {
                if (x.Price <= maxcost && x.Price >= mincost) {
                    showPhonesCommand(message.From.Id, x);
                }
            });
        }

      
        public override bool isArgumentContains() {
            return true;
        }
    }
}
