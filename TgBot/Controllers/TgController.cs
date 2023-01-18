using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.WebRequestMethods;
using TgBot.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Imaging;
using Telegram.Bot.Types.InputFiles;
using TgBot.Controllers.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace TgBot.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TgController {


        protected PhoneShopContext Context { get; private set; }
        public TgController() {
            this.Context = new PhoneShopContext();
        }


        [HttpPost]
        public async Task<IResult> Post([FromBody] Update update) {

            if (update == null) {
                return Results.BadRequest();
            }

          



            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery) {
                if (update.CallbackQuery.Data.Equals("buy")) {
                    await BotHelper.Manager.SendTextMessageAsync(1094771232, $"@{update.CallbackQuery.From.Username} купил:\n {update.CallbackQuery.Message.Text}");


                    int id = this.Context.Clients.FirstOrDefault(x => x.TelegramId == update.CallbackQuery.Message.Chat.Id.ToString()).UserId;
                    this.Context.Orders.Add(new Order {
                        Status = "ordered",
                        PhoneId = int.Parse(update.CallbackQuery.Message.Text.Split(".")[0]),
                        ClientId = id
                    });
                    this.Context.SaveChangesAsync();
                }
            }




            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message) {

                if (!this.Context.Clients.Any(x => x.TelegramId == update.Message.From.Id.ToString())) {
                    this.Context.Clients.Add(new Client() { TelegramId = update.Message.From.Id.ToString(), Username = update.Message.From.Username });
                    this.Context.SaveChangesAsync();
                } else if (!update.Message.From.Username.IsNullOrEmpty()) {
                    if (this.Context.Clients.FirstOrDefault(y => y.TelegramId == update.Message.From.Id.ToString()).Username != update.Message.From.Username) {
                        this.Context.Clients.FirstOrDefault(y => y.TelegramId == update.Message.From.Id.ToString()).Username = update.Message.From.Username;
                        this.Context.SaveChangesAsync();
                    }
                }

                if (update.Message.From.Username == null) {
                    return Results.BadRequest();
                }

                if (update.Message.Text == null || update.Message.Text.Equals(String.Empty)) {
                    return Results.BadRequest();
                }

                foreach (var command in CommandHelper.ClientCommands) {

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
