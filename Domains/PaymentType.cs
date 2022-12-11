using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Domains
{
    // NFe - tPag - Informações de Pagamento (pag)
    [Table("payment_types")]
    [Index(nameof(Name), Name = "idx_pty_name", IsUnique = true)]
    public class PaymentType
    {
        [Key]
        [Column("pty_id")]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [Column("pty_name")]
        [Display(Name = "Nome")]
        [MinLength(3, ErrorMessage = "É necessaário pelo menos {1} caracteres!")]
        [MaxLength(30, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string ? Name { get; set; }

    }

}
