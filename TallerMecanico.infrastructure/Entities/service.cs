using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TallerMecanico.infrastructure.Entities;

[Index("IdVehicle", Name = "IdVehicle")]
public partial class service
{
    [Key]
    public int IdService { get; set; }

    public int IdVehicle { get; set; }

    [StringLength(200)]
    public string? Description { get; set; }

    public DateOnly? DateService { get; set; }

    [ForeignKey("IdVehicle")]
    [InverseProperty("services")]
    public virtual vehicle IdVehicleNavigation { get; set; } = null!;
}
