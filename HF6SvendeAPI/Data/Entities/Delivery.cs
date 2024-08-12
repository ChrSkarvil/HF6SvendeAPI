using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HF6SvendeAPI.Data.Entities
{
    public class Delivery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(95)]
        public string Address { get; set; } = null!;
        public DateTime? DispatchedDate { get; set; }
        public DateTime? EstDeliveryDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        [Required]
        public decimal DeliveryFee { get; set; }
        [ForeignKey(nameof(PostalCode))]
        public int PostalCodeId { get; set; }
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public virtual PostalCode PostalCode { get; set; } = null!;
        public virtual Country Country { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;

    }
}
