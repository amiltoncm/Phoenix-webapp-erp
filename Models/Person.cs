using Microsoft.EntityFrameworkCore;
using Phoenix.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Phoenix.Models
{
    [Table("people")]
    [Index(nameof(Name), Name = "idx_peo_name", IsUnique = false)]
    [Index(nameof(Alias), Name = "idx_peo_alias", IsUnique = false)]
    public class Person
    {
        [Key]
        [Column("peo_id")]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("peo_name")]
        [Display(Name = "Nome")]
        [MinLength(3, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(60, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string? Name { get; set; }

        [Required]
        [Column("peo_alias")]
        [Display(Name = "Fantasia")]
        [MinLength(3, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(50, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string? Alias { get; set; }

        [Column("peo_document")]
        [Display(Name = "Documento")]
        [DisplayFormat(DataFormatString = "00.000.000/0000-00")]
        [MinLength(14, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(18, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string? Document { get; set; }

        [Column("peo_resgistration")]
        [Display(Name = "Registro")]
        [DisplayFormat(DataFormatString = "000.000.000")]
        [MinLength(6, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(15, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string? Registration { get; set; }

        [Column("pup_id")]
        [Display(Name = "Tipo de Logradouro")]
        [ForeignKey("PublicPlace")]
        public int PublicPlaceId { get; set; }

        [Column("peo_address")]
        [Display(Name = "Endereço")]
        [MinLength(3, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(60, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string? Address { get; set; }

        [AllowNull]
        [Column("peo_number")]
        [Display(Name = "Número")]
        public int ? Number { get; set; }

        [Column("peo_complement")]
        [Display(Name = "Complemento")]
        [MinLength(5, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(30, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string? Complement { get; set; }

        [Column("peo_district")]
        [Display(Name = "Bairro")]
        [MinLength(2, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(30, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string? District { get; set; }

        [Required]
        [Column("cti_id")]
        [Display(Name = "Cidade")]
        [ForeignKey("City")]
        public int CityId { get; set; }

        [Column("peo_reference")]
        [Display(Name = "Referência")]
        [MinLength(3, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(50, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string? Reference { get; set; }

        [Required]
        [Column("peo_phone")]
        [Display(Name = "Telefone")]
        [MinLength(8, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(15, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string? Phone { get; set; }

        [Column("peo_zip")]
        [Display(Name = "CEP")]
        [DisplayFormat(DataFormatString = "00.000-000")]
        [MinLength(8, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(10, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string? Zip { get; set; }

        [Required]
        [Column("pet_id")]
        [Display(Name = "Tipo de Pessoa")]
        [MinLength(1, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(1, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        [ForeignKey("PersonType")]
        public string ? PersonTypeId { get; set; }

        [Column("peo_email")]
        [Display(Name = "e-mail")]
        [MinLength(7, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(60, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string? Email { get; set; }

        [Required]
        [Column("peo_client")]
        [Display(Name = "Cliente")]
        [Range(0,1)]
        public bool Client { get; set; }

        [Required]
        [Column("peo_provider")]
        [Display(Name = "Fornecedor")]
        [Range(0, 1)]
        public bool Provider { get; set; }

        [Required]
        [Column("peo_shipping")]
        [Display(Name = "Transportadora")]
        [Range(0, 1)]
        public bool Shipping { get; set; }

        [Required]
        [Column("peo_associate")]
        [Display(Name = "Associado")]
        [Range(0, 1)]
        public bool Associate { get; set; }

        [Required]
        [Column("peo_created")]
        [Display(Name = "Criado em")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime Created { get; set; }

        [Required]
        [Column("peo_updated")]
        [Display(Name = "Alterado em")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime Updated { get; set; }

        [AllowNull]
        [Column("peo_deleted")]
        [Display(Name = "Inativado em")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime ? Deleted { get; set; }

        [Required]
        [Column("peo_status")]
        [Display(Name = "Status")]
        [ForeignKey("Status")]
        public int StatusID { get; set; }

        [Display(Name = "Tipo de Logradouro")]
        public virtual PublicPlace ? PublicPlace { get; set; }

        [Display(Name = "Cidade")]
        public virtual City ? City { get; set; }

        [Display(Name = "Tipo de Pessoa")]
        public virtual PersonType ? PersonType { get; set; }

        [Display(Name = "Status")]
        public virtual Status ? Status { get; set; }

    }

}
