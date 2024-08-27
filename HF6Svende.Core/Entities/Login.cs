using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using HF6Svende.Core.Entities;

namespace HF6SvendeAPI.Data.Entities
{
    public enum UserType
    {
        Customer = 1,
        Employee = 2,
    }

    public class Login
    {
        public Login()
        {
            RefreshTokens = new HashSet<RefreshToken>();;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string Password { get; set; } = null!;
        [Required]
        public UserType UserType { get; set; }
        [ForeignKey(nameof(Customer))]
        public int? CustomerId { get; set; }
        [ForeignKey(nameof(Employee))]
        public int? EmployeeId { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

    }
}
