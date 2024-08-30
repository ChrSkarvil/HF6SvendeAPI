using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HF6SvendeAPI.Data.Entities
{
    public class Listing
    {
        public Listing() 
        {
            OrderItems = new HashSet<OrderItem>();
            Carts = new HashSet<Cart>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public DateTime? SoldDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public DateTime? DenyDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;

        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }



        public bool IsListingVerified { get; set; }


    }
}
