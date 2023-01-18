using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.Controllers;
using TgBot.Controllers.Helpers;
using TgBot.Models.Interface;

namespace TgBot.Models.Commands.Client {
    public class ShowPhonesCommand : ICommand {

        protected PhoneShopContext Context { get; private set; }
        
        //
        //  Keyboard
        //
        private InlineKeyboardButton button1;
        protected InlineKeyboardMarkup keyboard;
        
        public ShowPhonesCommand() {
            this.Context = new PhoneShopContext();

            this.button1 = InlineKeyboardButton.WithCallbackData("Купить", "buy");
            this.keyboard = new InlineKeyboardMarkup(button1);
        }

        public virtual List<string> getAliases() {
            return new List<string>() { "show", "list", "показать", "список" };
        }
        public virtual string getName() {
            return "ShowPhones";
        }

        public virtual string getUsage() {
            return "/show (list, показать, список)";
        }

        public virtual async void execute(Message message) {

            this.Context.Phones.ToList().ForEach(async x => {
                showPhonesCommand(message.From.Id, x);
            });
        }

        protected virtual async void showPhonesCommand(ChatId id,Phone phone) {
            string category = this.Context.Categories.FirstOrDefault(x => x.Id == phone.CategoryId).Name;
            string producer = this.Context.Producers.FirstOrDefault(x => x.Id == phone.ProducerId).Name;

            await BotHelper.Client.SendTextMessageAsync(id, $"{phone.Id}. {phone.Name}\n{phone.Price} {phone.PriceType} \nПроизводитель: {producer}\nКатегория: {category}", replyMarkup: keyboard);
        }

        public virtual bool isArgumentContains() {
            return false;
        }
    }
}
