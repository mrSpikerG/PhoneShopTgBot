using System;
using System.Collections.Generic;

namespace TgBot.Models;

public partial class Producer
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Phone> Phones { get; } = new List<Phone>();
}
