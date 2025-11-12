using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TallerMecanico.Core.Entities;

namespace TallerMecanico.Core.Entities
{
    [Table("clients")]
    public partial class Client
    {
        [Key]
        public int IdClient { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [InverseProperty("Client")]
        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
