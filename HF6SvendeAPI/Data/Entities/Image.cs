using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HF6SvendeAPI.Data.Entities
{
    public class Image
    {
        public Image()
        {
            ProductImages = new HashSet<ProductImage>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public byte[]? File { get; set; } = null!;
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
