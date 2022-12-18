using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Domains
{
    // NFe - indPag - Indicador da forma de pagamento da NF-e
    [Table("payment_indications")]
    [Index(nameof(Name), Name = "idx_pin_name", IsUnique = true)]
    public class PaymentIndication
    {
        [Key]
        [Column("pin_id")]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [Column("pin_name")]
        [Display(Name = "Nome")]
        [MinLength(5, ErrorMessage = "É necessaário pelo menos {1} caracteres!")]
        [MaxLength(20, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string? Name { get; set; }

    }

}
