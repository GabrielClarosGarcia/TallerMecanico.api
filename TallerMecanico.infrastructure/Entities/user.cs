using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TallerMecanico.infrastructure.Entities;

public partial class user
{
    [Key]
    public int IdUser { get; set; }

    [StringLength(50)]
    public string Login { get; set; } = null!;

    [StringLength(100)]
    public string Name { get; set; } = null!;
}
