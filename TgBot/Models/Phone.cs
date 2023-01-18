using System;
using System.Collections.Generic;

namespace TgBot.Models;

public partial class Phone
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? CategoryId { get; set; }

    public int? ProducerId { get; set; }

    public decimal? Price { get; set; }

    public string? PriceType { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual Producer? Producer { get; set; }
}
