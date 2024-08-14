using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HF6SvendeAPI.Data.Entities
{
    public class Gender
    {
        public Gender()
        {
            Categories = new HashSet<Category>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Category> Categories { get; set; }
    }
}
