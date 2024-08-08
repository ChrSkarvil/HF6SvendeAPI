using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HF6SvendeAPI.Data.Entities
{
    public class Country
    {
        public Country()
        {
            Customers = new HashSet<Customer>();
            Deliveries = new HashSet<Delivery>();
            Departments = new HashSet<Department>();
            Employees = new HashSet<Employee>();
            PostalCodes = new HashSet<PostalCode>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(10)]
        public string CountryCode { get; set; } = null!;

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<PostalCode> PostalCodes { get; set; }
    }
}
