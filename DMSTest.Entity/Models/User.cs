using System;
using System.Collections.Generic;

namespace DMSTest.Entity.Models;

public partial class User
{
    public int IdUsers { get; set; }

    public string? Nombre { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
}
