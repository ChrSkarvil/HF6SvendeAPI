using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HF6SvendeAPI.Data.Entities
{
    public class Customer
    {
        public Customer()
        {
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
            Payments = new HashSet<Payment>();
            Products = new HashSet<Product>();
            Listings = new HashSet<Listing>();
        }

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
        [StringLength(255)]
        public string Email { get; set; } = null!;
        public DateTime? CreateDate { get; set; }
        [ForeignKey(nameof(PostalCode))]
        public int PostalCodeId { get; set; }
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public virtual PostalCode PostalCode { get; set; } = null!;
        public virtual Country Country { get; set; } = null!;
        public virtual Login Login { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Listing> Listings { get; set; }

    }
}
