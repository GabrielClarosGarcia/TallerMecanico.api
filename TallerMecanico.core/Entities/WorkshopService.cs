using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using TallerMecanico.Core.Entities;

namespace TallerMecanico.Core.Entities
{
    [Table("services")]
    [Index("IdVehicle", Name = "IdVehicle")]
    public partial class WorkshopService
    {
        [Key]
        public int IdService { get; set; }

        public int IdVehicle { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        public DateTime? DateService { get; set; }

        [ForeignKey("IdVehicle")]
        [InverseProperty("Services")]
        public virtual Vehicle Vehicle { get; set; } = null!;
    }
}
