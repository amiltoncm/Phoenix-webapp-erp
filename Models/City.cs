using Microsoft.EntityFrameworkCore;
using Phoenix.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Models
{
    [Table("cities")]
    [Index(nameof(Name), Name = "idx_cti_name", IsUnique = false)]

    public class City
    {
        [Key]
        [Column("cti_id")]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("cti_code")]
        [Display(Name = "Código")]
        public int Code { get; set; }

        [Required]
        [Column("cti_name")]
        [Display(Name = "Nome")]
        [MinLength(3, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(50, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string? Name { get; set; }

        [Column("ste_id")]
        [Display(Name = "ID Estado")]
        [ForeignKey("State")]
        public int StateId { get; set; }

        [Column("sta_id")]
        [Display(Name = "ID Status")]
        [ForeignKey("Status")]
        public int StatusId { get; set; }

        [Column("cti_created")]
        [Display(Name = "Criado em")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime Created { get; set; }

        [Column("cti_updated")]
        [Display(Name = "Atualizado em")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime Updated { get; set; }

        [Display(Name = "Estado")]
        public virtual State? State { get; set; }

        [Display(Name = "Status")]
        public virtual Status? Status { get; set; }
    }
}
