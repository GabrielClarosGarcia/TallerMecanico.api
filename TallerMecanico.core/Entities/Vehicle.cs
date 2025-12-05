using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TallerMecanico.Core.Entities
{
    [Table("vehicles")]
    [Index("IdClient", Name = "IdClient")]
    public partial class Vehicle
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

        // La relación con Client
        [ForeignKey("IdClient")]
        [InverseProperty("Vehicles")]
        public virtual Client Client { get; set; } = null!;

        [InverseProperty("Vehicle")]
        public virtual ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
