using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HF6SvendeAPI.Data.Entities
{
    public class Product
    {
        public Product()
        {
            ProductColors = new HashSet<ProductColor>();
            Images = new HashSet<Image>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(45)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(45)]
        public string Brand { get; set; } = null!;
        [Required]
        [StringLength(200)]
        public string Description { get; set; } = null!;
        [Required]
        [StringLength(30)]
        public string Size { get; set; } = null!;
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
        public virtual Listing Listing { get; set; } = null!;
        public virtual ICollection<ProductColor> ProductColors { get; set; }
        public virtual ICollection<Image> Images { get; set; }

    }
}
