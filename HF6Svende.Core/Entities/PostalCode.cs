using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HF6SvendeAPI.Data.Entities
{
    public class PostalCode
    {
        public PostalCode()
        {
            Customers = new HashSet<Customer>();
            Deliveries = new HashSet<Delivery>();
            Departments = new HashSet<Department>();
            Employees = new HashSet<Employee>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string PostCode { get; set; } = null!;
        [Required]
        [StringLength(65)]
        public string City { get; set; } = null!;
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
