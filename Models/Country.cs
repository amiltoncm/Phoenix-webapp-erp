using Microsoft.EntityFrameworkCore;
using Phoenix.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Models
{
    [Table("countries")]
    [Index(nameof(Name), Name = "idx_cnt_name", IsUnique = true)]
    [Index(nameof(Iso2), Name = "idx_cnt_iso2", IsUnique = true)]
    [Index(nameof(Iso3), Name = "idx_cnt_iso3", IsUnique = true)]
    public class Country
    {
        [Key]
        [Column("cnt_id")]
        [Display(Name = "Código")]
        [DisplayFormat(DataFormatString = "{0:0000}")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("cnt_name")]
        [Display(Name = "Nome")]
        [MinLength(3, ErrorMessage = "É necessaário pelo menos {1} caracteres!")]
        [MaxLength(50, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string? Name { get; set; }

        [Required]
        [Column("cnt_iso2")]
        [Display(Name = "Iso 2")]
        [MinLength(2, ErrorMessage = "É necessaário pelo menos {1} caracteres!")]
        [MaxLength(2, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string? Iso2 { get; set; }

        [Required]
        [Column("cnt_iso3")]
        [Display(Name = "Iso 3")]
        [MinLength(3, ErrorMessage = "É necessaário pelo menos {1} caracteres!")]
        [MaxLength(3, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string? Iso3 { get; set; }

        [Required]
        [Column("sta_id")]
        [Display(Name = "ID Status")]
        [ForeignKey("Status")]
        public int StatusId { get; set; }

        [Required]
        [Column("cnt_created")]
        [Display(Name = "Criado em")]
        public DateTime Created { get; set; }
        
        [Required]
        [Column("cnt_updated")]
        [Display(Name = "Alterado em")]
        public DateTime Updated { get; set; }

        public virtual Status? Status { get; set; }

    }
}
