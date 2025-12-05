using System;
using System.Collections.Generic;
using TallerMecanico.Core.Enum;

namespace TallerMecanico.Core.Entities;

public partial class Security
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!; 

    public string Name { get; set; } = null!;

    public RoleType Role { get; set; } 
}
