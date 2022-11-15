using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Domains
{
    [Table("status")]
    [Index(nameof(Name), Name = "idx_sta_name", IsUnique = true)]
    public class Status
    {
        [Key]
        [Column("sta_id")]
        [Display(Name = "Código")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("sta_name")]
        [Display(Name = "Nome")]
        [MinLength(3, ErrorMessage ="É necessaário pelo menos {1} caracteres!")]
        [MaxLength(20, ErrorMessage ="O campo nome suporta apenas {1} caracteres!")]
        public string? Name { get; set; }

    }
}
