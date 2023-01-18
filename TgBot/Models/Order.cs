using System;
using System.Collections.Generic;

namespace TgBot.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? PhoneId { get; set; }

    public int? ClientId { get; set; }

    public string? Status { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Phone? Phone { get; set; }
}
