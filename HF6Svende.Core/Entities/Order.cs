using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HF6SvendeAPI.Data.Entities
{
    public class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? CreateDate { get; set; }
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        [ForeignKey(nameof(Payment))]
        public int PaymentId { get; set; }
        [ForeignKey(nameof(Delivery))]
        public int DeliveryId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Delivery Delivery { get; set; } = null!;
        public virtual Payment Payment { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
