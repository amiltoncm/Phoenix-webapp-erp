using Microsoft.EntityFrameworkCore;
using Phoenix.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Models
{
    [Table("payment_terms")]
    [Index(nameof(Name), Name = "idx_pyt_name", IsUnique = true)]
    public class PaymentTerm    {

        [Key]
        [Column("pyt_id")]
        [Display(Name = "Código")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("pyt_name")]
        [Display(Name = "Nome")]
        [MinLength(3, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(50, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string ? Name { get; set; }

        [Required]
        [Column("pyt_installments")]
        [Display(Name = "Parcelas")]
        public int Installments { get; set; }

        [Required]
        [Column("pyt_periods")]
        [Display(Name = "Período (dias)")]
        public int Period { get; set; }

        [Required]
        [Column("pyt_fees")]
        [Display(Name = "Taxas")]
        public float Fees { get; set; }

        [Required]
        [Column("pin_id")]
        [Display(Name = "Indicação de Pagamento")]
        [ForeignKey("PaymentIndication")]
        public int PaymentIndicationId { get; set; }

        [Column("pty_created")]
        [Display(Name = "Criado em")]
        public DateTime Created { get; set; }

        [Column("pty_updated")]
        [Display(Name = "Atualizado em")]
        public DateTime Updated { get; set; }

        [Required]
        [Column("sta_id")]
        [Display(Name = "Status")]
        [ForeignKey("Status")]
        public int StatusId { get; set; }

        [Display(Name = "Indicação de Pagamento")]
        public virtual PaymentIndication? PaymentIndication { get; set; }

        [Display(Name = "Status")]
        public virtual Status ? Status { get; set; }

    }

}
