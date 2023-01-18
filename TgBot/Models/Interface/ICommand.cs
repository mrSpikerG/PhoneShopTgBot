using Telegram.Bot.Types;

namespace TgBot.Models.Interface {
    public interface ICommand {

        public List<string> getAliases();
        public string getName();
        public string getUsage();
        public bool isArgumentContains();
        public void execute(Message message);
        

    }
}
