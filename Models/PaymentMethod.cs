using Microsoft.EntityFrameworkCore;
using Phoenix.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Models
{
    [Table("payment_methods")]
    [Index(nameof(Name), Name = "idx_pay_name", IsUnique = true)]
    public class PaymentMethod
    {
        [Key]
        [Column("pay_id")]
        [Display(Name = "Código")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("pay_name")]
        [Display(Name = "Nome")]
        [MinLength(3, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(30, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string? Name { get; set; }

        [Required]
        [Column("pty_id")]
        [Display(Name = "Tipo de Pagamento")]
        [ForeignKey("PaymentType")]
        public int PaymentTypeId { get; set; }

        [Column("acc_id")]
        [Display(Name = "Conta")]
        [ForeignKey("Account")]
        public int? AccountId { get; set; }

        [Column("pay_days")]
        [Display(Name = "Dias")]
        public int? Days { get; set; }

        [Required]
        [Column("sta_id")]
        [Display(Name = "Status")]
        [ForeignKey("Status")]
        public int StatusId { get; set; }

        [Required]
        [Column("pay_created")]
        [Display(Name = "Criado em")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime Created { get; set; }

        [Required]
        [Column("pay_updated")]
        [Display(Name = "Alterado em")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime Updated { get; set; }

        [Display(Name = "Conta")]
        public Account? Account { get; set; }

        [Display(Name = "Tipo de Pagamento")]
        public PaymentType? PaymentType { get; set; }

        [Display(Name = "Status")]
        public virtual Status? Status { get; set; }

    }

}
