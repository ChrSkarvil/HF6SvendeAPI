using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HF6SvendeAPI.Data.Entities
{
    public class Listing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime ExpireDate { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

    }
}
