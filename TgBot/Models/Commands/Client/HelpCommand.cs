using System.Numerics;
using System.Windows.Input;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.Controllers.Helpers;
using TgBot.Models.Interface;

namespace TgBot.Models.Commands.Client {
    public class HelpCommand : Interface.ICommand {
      

        public List<string> getAliases() {
            return new List<string>{ "help","помощь"};
        }

        public string getName() {
            return "Help";
        }

        public string getUsage() {
            return "/help (помощь)";
        }

        public async void execute(Message message) {
            string messageToSend = "Список команд:\n";
            foreach(var command in CommandHelper.ClientCommands) {
                messageToSend += $"{command.getName()} - {command.getUsage()}\n";
            }
            await BotHelper.Client.SendTextMessageAsync(message.From.Id, messageToSend);
        }

        public bool isArgumentContains() {
            return false;
        }
    }
}
