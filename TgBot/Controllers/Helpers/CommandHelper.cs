using System.Runtime.CompilerServices;
using TgBot.Models.Commands.Client;
using TgBot.Models.Interface;

namespace TgBot.Controllers.Helpers {
    public class CommandHelper {

        public static List<ICommand> ClientCommands { get; private set; }


        public static void init() {

            ClientCommands = new List<ICommand>();

            //
            //  Client commands
            //
            registerCommand(new ShowPhonesCommand());
            registerCommand(new ShowByCostCommand());
            registerCommand(new ShowByNameCommand());
            registerCommand(new ShowByProducerCommand());
            registerCommand(new ShowByCategoryCommand());
            registerCommand(new HelpCommand());

        }

        public static void registerCommand(ICommand command) {
            ClientCommands.Add(command);
        }


    }
}
