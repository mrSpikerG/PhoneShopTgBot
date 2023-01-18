using System;
using System.Collections.Generic;

namespace TgBot.Models;

public partial class Client
{
    public int UserId { get; set; }

    public string? TelegramId { get; set; }

    public string? Username { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
