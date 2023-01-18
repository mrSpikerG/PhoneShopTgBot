using System.Windows.Input;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.Controllers.Helpers;
using TgBot.Controllers;
using System.Numerics;
using Telegram.Bot;
using System.Threading.Tasks.Dataflow;

namespace TgBot.Models.Commands.Manager {
    public class ShowOrdersCommand : Interface.ICommand {

        protected PhoneShopContext Context { get; private set; }

        //
        //  Keyboard
        //
        private InlineKeyboardButton button1;
        protected InlineKeyboardMarkup keyboard;

        public ShowOrdersCommand() {
            Context = new PhoneShopContext();

            button1 = InlineKeyboardButton.WithCallbackData("Поменять статус", "changeStatus");
            keyboard = new InlineKeyboardMarkup(button1);
        }

        public virtual List<string> getAliases() {
            return new List<string>() { "orders", "заказы" };
        }
        public virtual string getName() {
            return "ShowOrders";
        }

        public virtual string getUsage() {
            return "/orders <status>(optional)  (заказы)";
        }

        public virtual async void execute(Message message) {

            this.Context.Orders.ToList().ForEach(x => {

                Phone temp = this.Context.Phones.FirstOrDefault(y => y.Id == x.PhoneId);
                if (message.Text.Split(" ").Length == 2) {
                    if (x.Status.ToLower().StartsWith(message.Text.Split(" ")[1].ToLower())) {
                        showOrders(message.From.Id, temp, x.Id, x.Status.StartsWith("ordered"));
                    }
                } else {
                    showOrders(message.From.Id, temp, x.Id, x.Status.StartsWith("ordered"));
                }


            });
            
        }
        protected virtual async void showOrders(ChatId id, Phone temp, int orderId, bool isOrdered) {
            string category = Context.Categories.FirstOrDefault(y => y.Id == temp.CategoryId).Name;
            string producer = Context.Producers.FirstOrDefault(y => y.Id == temp.ProducerId).Name;
            if (isOrdered) {
                await BotHelper.Manager.SendTextMessageAsync(id, $"{orderId}. {temp} \nПроизводитель: {producer}\nКатегория: {category}", replyMarkup: keyboard);
            } else {
                await BotHelper.Manager.SendTextMessageAsync(id, $"{orderId}. {temp} \nПроизводитель: {producer}\nКатегория: {category}");
            }
        }

        public virtual bool isArgumentContains() {
            return true;
        }
    }
}
