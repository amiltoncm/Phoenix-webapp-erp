using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Models
{
    [Table("payment_term_methods")]
    public class PaymentTermMethod
    {
        [Required]
        [Column("pyt_id")]
        [Display(Name = "Prazo de pagamento")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PaymentTermId { get; set; }

        [Required]
        [Column("pay_id")]
        [Display(Name = "Método de pagamento")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PaymentMethodId { get; set; }

        [Display(Name = "Prazo de pagamento")]
        public PaymentTerm ? PaymentTerm { get; set; }

        [Display(Name = "Método de pagamento")]
        public PaymentMethod ? PaymentMethod { get; set; }

    }

}
