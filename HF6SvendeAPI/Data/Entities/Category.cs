using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HF6SvendeAPI.Data.Entities
{
    public class Category
    {
        public Category() 
        {
            Products = new HashSet<Product>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [ForeignKey(nameof(Gender))]
        public int GenderId { get; set; }

        public virtual Gender Gender { get; set; } = null!;
        public virtual ICollection<Product> Products { get; set; }

    }
}
