using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HF6SvendeAPI.Data.Entities
{
    public class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(95)]
        public string Address { get; set; } = null!;
        [Required]
        [StringLength(255)]
        public string Email { get; set; } = null!;
        [ForeignKey(nameof(PostalCode))]
        public int PostalCodeId { get; set; }
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public virtual PostalCode PostalCode { get; set; } = null!;
        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
