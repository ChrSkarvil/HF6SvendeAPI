using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HF6SvendeAPI.Models;

namespace HF6SvendeAPI.Data.Entities
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = null!;
        [Required]
        [StringLength(95)]
        public string Address { get; set; } = null!;
        [Required]
        public long Phone { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; } = null!;
        [Required]
        public DateTime HireDate { get; set; }
        public DateTime? EndDate { get; set; }
        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }
        [ForeignKey(nameof(PostalCode))]
        public int PostalCodeId { get; set; }
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }
        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual PostalCode PostalCode { get; set; } = null!;
        public virtual Country Country { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
        public virtual Login Login { get; set; } = null!;

    }
}
