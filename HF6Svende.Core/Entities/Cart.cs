using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HF6SvendeAPI.Data.Entities
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        public DateTime? CreateDate { get; set; }
        [ForeignKey(nameof(Listing))]
        public int ListingId { get; set; }
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        public virtual Listing Listing { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
    }
}
