using Phoenix.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Models
{
    [Table("person_addresses")]
    public class PersonAddress
    {
        [Key]
        [Column("pad_id")]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("peo_id")]
        [Display(Name = "ID Pessoa")]
        [ForeignKey("People")]
        public int PersonId { get; set; }

        [Required]
        [Column("adt_id")]
        [Display(Name = "Tipo de Endereço")]
        [ForeignKey("AddressType")]
        public int AddressTypeId { get; set; }

        [Column("pup_id")]
        [Display(Name = "ID Tipo de Logradouro")]
        [ForeignKey("PublicPlace")]
        public int PublicPlaceId { get; set; }

        [Column("pad_address")]
        [Display(Name = "Endereço")]
        [MinLength(3, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(60, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string ? Address { get; set; }

        [Column("pad_number")]
        [Display(Name = "Número")]
        public int Number { get; set; }

        [Column("pad_complement")]
        [Display(Name = "Complemento")]
        [MinLength(5, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(30, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string ? Complement { get; set; }

        [Column("pad_reference")]
        [Display(Name = "Referência")]
        [MinLength(3, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(50, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string ? Reference { get; set; }

        [Column("pad_district")]
        [Display(Name = "Bairro")]
        [MinLength(2, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(30, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string ? District { get; set; }

        [Required]
        [Column("cti_id")]
        [Display(Name = "ID Cidade")]
        [ForeignKey("City")]
        public int CityId { get; set; }

        [Column("pad_zip")]
        [Display(Name = "CEP")]
        [DisplayFormat(DataFormatString = "00.000-000")]
        [MinLength(8, ErrorMessage = "É necessário pelo menos {1} caracteres!")]
        [MaxLength(10, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public string ? Zip { get; set; }

        [Display(Name = "Pessoa")]
        public virtual Person ? Person { get; set; }

        [Display(Name = "Tipo de Endereço")]
        public virtual AddressType ? AddressType { get; set; }

        [Display(Name = "Tipo de Logradouro")]
        public virtual PublicPlace ? PublicPlace { get; set; }

        [Display(Name = "Cidade")]
        public virtual City ? City { get; set; }

    }

}
