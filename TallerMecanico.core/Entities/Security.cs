using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TallerMecanico.Core.Enum;

namespace TallerMecanico.Core.Entities
{
    [Table("security")]
    public class Security
{
        [Key]
    public int Id { get; set; }

        [Required]
        [StringLength(50)]
    public string Login { get; set; } = null!;

        [Required]
        [StringLength(200)]
    public string Password { get; set; } = null!; 

        [Required]
        [StringLength(100)]
    public string Name { get; set; } = null!;

        [Required]
    public RoleType Role { get; set; } 
}
}
