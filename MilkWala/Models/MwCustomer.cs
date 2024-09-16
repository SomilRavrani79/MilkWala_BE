using System;
using System.Collections.Generic;

namespace MilkWala.Models;

public partial class MwCustomer
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public bool IsActive { get; set; }
}
