using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TallerMecanico.infrastructure.Entities;

[Index("IdClient", Name = "IdClient")]
public partial class vehicle
{
    [Key]
    public int IdVehicle { get; set; }

    public int IdClient { get; set; }

    [StringLength(50)]
    public string? Brand { get; set; }

    [StringLength(50)]
    public string? Model { get; set; }

    [StringLength(20)]
    public string? Plate { get; set; }

    [ForeignKey("IdClient")]
    [InverseProperty("vehicles")]
    public virtual client IdClientNavigation { get; set; } = null!;

    [InverseProperty("IdVehicleNavigation")]
    public virtual ICollection<service> services { get; set; } = new List<service>();
}
