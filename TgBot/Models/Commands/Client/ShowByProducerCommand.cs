using System.Runtime.CompilerServices;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Controllers.Helpers;

namespace TgBot.Models.Commands.Client
{
    public class ShowByProducerCommand : ShowPhonesCommand {
        public override List<string> getAliases() {
            return new List<string>() { "producer", "производитель" };
        }

        public override string getName() {
            return "ShowByProducer";
        }

        public override string getUsage() {
            return "/producer <producer_name> (производитель)";
        }

        public override async void execute(Message message) {

            decimal mincost, maxcost;
            string args = message.Text.ToLower().Replace("/producer", "").Replace("/производитель", "").Trim();
            string name;
            try {
                name = args.Split(' ')[0];
            } catch {
                await BotHelper.Client.SendTextMessageAsync(message.From.Id, $"Некорректное значение");
                return;
            }

            this.Context.Phones.ToList().ForEach(async x => {
                if (this.Context.Producers.First(y => y.Id == x.ProducerId).Name.ToLower().Contains(name)) {
                    showPhonesCommand(message.From.Id, x);
                }
            });
        }

        public override bool isArgumentContains() {
            return true;
        }
    }
}
