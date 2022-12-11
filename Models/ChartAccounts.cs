using Microsoft.EntityFrameworkCore;
using Phoenix.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Phoenix.Models
{
    [Table("chart_accounts")]
    [Index(nameof(Name), Name = "idx_cac_name", IsUnique = true)]
    public class ChartAccounts
    {
        [Key]
        [Column("cac_id")]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("cac_name")]
        [Display(Name = "Nome")]
        [MinLength(3, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(50, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string ? Name { get; set; }

        [Column("mvt_id")]
        [Display(Name = "Tipo de Movimento")]
        [MinLength(1, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(1, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        [ForeignKey("MovimentType")]
        public string ? MovimentTypeId { get; set; }

        [Column("cac_created")]
        [Display(Name = "Criado em")]
        public DateTime Created { get; set; }

        [Column("cac_updated")]
        [Display(Name = "Atualizado em")]
        public DateTime Updated { get; set; }

        [Column("sta_id")]
        [Display(Name = "Status")]
        [ForeignKey("Status")]
        public int StatusId { get; set; }

        [Display(Name = "Tipo de Movimento")]
        public virtual MovimentType ? MovimentType { get; set; }

        [Display(Name = "Status")]
        public virtual Status ? Status { get; set; }

    }

}
