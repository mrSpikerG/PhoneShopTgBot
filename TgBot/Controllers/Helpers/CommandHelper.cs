using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Runtime.CompilerServices;
using TgBot.Models.Commands.Both;
using TgBot.Models.Commands.Client;
using TgBot.Models.Commands.Manager;
using TgBot.Models.Interface;

namespace TgBot.Controllers.Helpers {
    public class CommandHelper {

        public static List<ICommand> ClientCommands { get; private set; }
        public static List<ICommand> ManagerCommands { get; private set; }


        public static void init() {

            ClientCommands = new List<ICommand>();
            ManagerCommands = new List<ICommand>();

            //
            //  Client commands
            //
            registerClientCommand(new ShowPhonesCommand());
            registerClientCommand(new ShowByCostCommand());
            registerClientCommand(new ShowByNameCommand());
            registerClientCommand(new ShowByProducerCommand());
            registerClientCommand(new ShowByCategoryCommand());
            registerClientCommand(new HelpCommand(CommandSide.Client));


            //
            //  Manager commands
            //
            registerManagerCommand(new HelpCommand(CommandSide.Manager));
            registerManagerCommand(new ShowOrdersCommand());
            registerManagerCommand(new ShowUsers());
            registerManagerCommand(new SendMessageCommand());
            registerManagerCommand(new AddPhoneCommand());
            registerManagerCommand(new RemovePhoneCommand());


        }


        public static void registerClientCommand(ICommand command){
            registerCommand(command, CommandSide.Client);
        }

        public static void registerManagerCommand(ICommand command) {
            registerCommand(command, CommandSide.Manager);
        }

        public static void registerCommand(ICommand command, CommandSide side) {
            if (side == CommandSide.Client) {
                ClientCommands.Add(command);
            }

            if (side == CommandSide.Manager) {
                ManagerCommands.Add(command);
            }

        }

        public enum CommandSide {
            Client,
            Manager
        }

    }
}
