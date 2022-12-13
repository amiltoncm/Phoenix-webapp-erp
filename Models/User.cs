using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Phoenix.Domains;
using System.Diagnostics.CodeAnalysis;

namespace Phoenix.Models
{
    [Table("users")]
    [Index(nameof(Name), Name = "idx_usr_name", IsUnique = true)]
    public class User
    {
        [Key]
        [Column("usr_id")]
        [Display(Name = "Código")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("usr_name")]
        [Display(Name = "Nome")]
        [MinLength(3, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(20, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string? Name { get; set; }

        [Column("usr_email")]
        [Display(Name = "e-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        [MinLength(7, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(75, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string? Email { get; set; }

        [Required]
        [Column("usr_password")]
        [Display(Name = "Senha")]
        [MinLength(6, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(128, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string? Password { get; set; }

        [Required]
        [Column("pro_id")]
        [Display(Name = "ID Perfil")]
        [ForeignKey("Profile")]
        public int ProfileId { get; set; }

        [Required]
        [Column("sta_id")]
        [Display(Name = "ID Status")]
        [ForeignKey("Status")]
        public int StatusId { get; set; }

        [Required]
        [Column("usr_created")]
        [Display(Name = "Criado em")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime Created { get; set; }

        [Required]
        [Column("usr_updated")]
        [Display(Name = "Alterado em")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime Updated { get; set; }

        [AllowNull]
        [Column("usr_deleted")]
        [Display(Name = "Inativado em")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime ? Deleted { get; set; }

        [Display(Name = "Status")]
        public virtual Status? Status { get; set; }

        [Display(Name = "Perfil")]
        public virtual Profile? Profile { get; set; }

    }

}
