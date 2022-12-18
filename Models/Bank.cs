using Microsoft.EntityFrameworkCore;
using Phoenix.Domains;
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
        [DisplayFormat(DataFormatString = "{0:000}")]
        public int Code { get; set; }

        [Required]
        [Column("bnk_name")]
        [Display(Name = "Nome")]
        [MinLength(3, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(75, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string? Name { get; set; }

        [Column("bnk_created")]
        [Display(Name = "Criado em")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime Created { get; set; }

        [Column("bnk_updated")]
        [Display(Name = "Atualizado em")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime Updated { get; set; }

        [Column("sta_id")]
        [Display(Name = "ID Status")]
        [ForeignKey("Status")]
        public int StatusId { get; set; }

        [Display(Name = "Status")]
        public virtual Status? Status { get; set; }
    }

}
