using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Models
{
    [Table("profiles")]
    [Index(nameof(Name), Name = "idx_pro_name", IsUnique = true)]
    public class Profile
    {
        [Key]
        [Column("pro_id")]
        [Display(Name = "Código")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("pro_name")]
        [Display(Name = "Nome")]
        [MinLength(3, ErrorMessage ="É necessário pelo menos {1} caracteres!")]
        [MaxLength(20, ErrorMessage ="O campo nome suporta apenas {1} caracteres!")]
        public string? Name { get; set; }

    }
}
