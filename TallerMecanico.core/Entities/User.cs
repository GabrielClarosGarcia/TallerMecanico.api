using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerMecanico.Core.Entities
{
    [Table("users")]
    public partial class User
    {
        [Key]
        public int IdUser { get; set; }

        [StringLength(50)]
        public string Login { get; set; } = null!;

        [StringLength(100)]
        public string Name { get; set; } = null!;
    }
}
