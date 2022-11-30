using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Models
{

    [Table("banks")]
    [Index(nameof(Code), Name = "idx_bnk_code", IsUnique = true)]
    [Index(nameof(Name), Name = "idx_bnk_name", IsUnique = true)]
    public class Bank
    {
        [Key]
        [Column("bnk_id")]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("bnk_code")]
        [Display(Name = "Código")]
        public int Code { get; set; }

        [Required]
        [Column("bnk_name")]
        [Display(Name = "Nome")]
        [MinLength(3, ErrorMessage = "É necessaário pelo menos {1} caracteres!")]
        [MaxLength(75, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string ? Name { get; set; }

    }

}
