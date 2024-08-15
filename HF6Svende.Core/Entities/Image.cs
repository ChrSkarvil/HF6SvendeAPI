using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HF6SvendeAPI.Data.Entities
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "VARBINARY(MAX)")]
        public byte[] File { get; set; } = null!;
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public bool IsVerified { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;

    }
}
