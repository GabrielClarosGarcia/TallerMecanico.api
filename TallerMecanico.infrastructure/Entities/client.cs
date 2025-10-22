using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TallerMecanico.infrastructure.Entities;

public partial class client
{
    [Key]
    public int IdClient { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [InverseProperty("IdClientNavigation")]
    public virtual ICollection<vehicle> vehicles { get; set; } = new List<vehicle>();
}
