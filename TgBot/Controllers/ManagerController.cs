using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using TgBot.Models;
using TgBot.Controllers.Helpers;
using Microsoft.IdentityModel.Tokens;
using Telegram.Bot.Types;

namespace TgBot.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ManagerController {


        protected PhoneShopContext Context { get; private set; }
        public ManagerController() {
            this.Context = new PhoneShopContext();
        }


        [HttpPost]
        public async Task<IResult> Post([FromBody] Update update) {

            if (update == null) {
                return Results.BadRequest();
            }





            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery) {
                if (update.CallbackQuery.Data.Equals("changeStatus")) {
                    //await BotHelper.Manager.SendTextMessageAsync(1094771232, $"@{update.CallbackQuery.From.Username} купил:\n {update.CallbackQuery.Message.Text}");

                    int orderid = int.Parse(update.CallbackQuery.Message.Text.Split(".")[0]);
                    this.Context.Orders.FirstOrDefault(x => x.Id == orderid).Status = "checked";
                    this.Context.SaveChangesAsync();
                                                                                                                              //
                                                                                                                              //update.CallbackQuery.Message.Chat.Id.ToString()).UserId;
                    //this.Context.Orders.Add(new Order {
                    //    Status = "ordered",
                    //    PhoneId = int.Parse(),
                    //    ClientId = id
                    //});
                    //this.Context.SaveChangesAsync();
                }
            }




            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message) {

                if (update.Message.Text.IsNullOrEmpty()) {
                    return Results.BadRequest();
                }

                foreach (var command in CommandHelper.ManagerCommands) {

                    bool isDone = false;
                    bool isArgument = command.isArgumentContains();

                    foreach (var alias in command.getAliases()) {
                        if (isArgument) {
                            if (update.Message.Text.ToLower().StartsWith($"/{alias}")) {
                                isDone = true;
                            }
                        } else {
                            if (update.Message.Text.ToLower().Equals($"/{alias}")) {
                                isDone = true;
                            }
                        }
                    }

                    if (isDone) {
                        command.execute(update.Message);
                        break;
                    }

                }


                //if (update.Message.Text.StartsWith("Поиск: ")) {
                //    string mess = update.Message.Text.Substring(7);
                //    this.context.Phones.ToList().ForEach(async x => {
                //        if (x.Name.Contains(mess)) {

                //            await BotHelper.Client.SendTextMessageAsync(update.Message.From.Id, $"{x.Name}\n{x.Price} {x.PriceType}", replyMarkup: keyboard);
                //        }
                //    });
                //} else {
                //    if (update.Message.Text.StartsWith("Цена: ")) {

                //        decimal mincost, maxcost;
                //        try {
                //            mincost = decimal.Parse(update.Message.Text.Substring(6).Split("-")[0]);
                //        } catch {
                //            mincost = decimal.MinValue;
                //        }

                //        try {
                //            maxcost = decimal.Parse(update.Message.Text.Substring(6).Split("-")[1]);
                //        } catch {
                //            maxcost = decimal.MaxValue;
                //        }

                //        this.context.Phones.ToList().ForEach(async x => {
                //            if (x.Price <= maxcost && x.Price >= mincost) {
                //                await BotHelper.Client.SendTextMessageAsync(update.Message.From.Id, $"{x.Name}\n{x.Price} {x.PriceType}", replyMarkup: keyboard);
                //            }
                //        });
                //    } else {
                //        this.context.Phones.ToList().ForEach(async x => {
                //            await BotHelper.Client.SendTextMessageAsync(update.Message.From.Id, $"{x.Name}\n{x.Price} {x.PriceType}", replyMarkup: keyboard);
                //        });
                //    }
                //}


            }
            return Results.Ok();
        }


    }
}
