using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Phoenix.Domains
{
    [Table("moviment_types")]
    [Index(nameof(Name), Name = "idx_mvt_name", IsUnique = true)]
    public class MovimentType
    {

        [Key]
        [Column("mvt_id")]
        [Display(Name = "ID")]
        [MinLength(1, ErrorMessage = "É necessaário pelo menos {1} caracteres!")]
        [MaxLength(1, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ? Id { get; set; }

        [Required]
        [Column("mvt_name")]
        [Display(Name = "Nome")]
        [MinLength(3, ErrorMessage = "É necessaário pelo menos {1} caracteres!")]
        [MaxLength(10, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string ? Name { get; set; }

    }
}
