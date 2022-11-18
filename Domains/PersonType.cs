using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phoenix.Domains
{
    [Table("person_types")]
    [Index(nameof(Name), Name = "idx_pet_name", IsUnique = true)]
    public class PersonType
    {
        [Key]
        [Column("pet_id")]
        [Display(Name = "ID")]
        [MinLength(1, ErrorMessage = "É necessaário pelo menos {1} caracteres!")]
        [MaxLength(1, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public String? Id { get; set; }

        [Required]
        [Column("pet_name")]
        [Display(Name = "Nome")]
        [MinLength(3, ErrorMessage = "É necessaário pelo menos {1} caracteres!")]
        [MaxLength(20, ErrorMessage = "O campo nome suporta apenas {1} caracteres!")]
        public String? Name { get; set; }

    }
}
