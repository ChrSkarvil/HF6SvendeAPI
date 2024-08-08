using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HF6SvendeAPI.Data.Entities
{
    public class Color
    {
        public Color()
        {
            ProductColors = new HashSet<ProductColor>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; } = null!;

        public virtual ICollection<ProductColor> ProductColors { get; set; }

    }
}
