using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6Svende.Application.DTO.Customer;
using HF6Svende.Application.DTO.Delivery;
using HF6Svende.Application.DTO.Payment;

namespace HF6Svende.Application.DTO.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int CustomerId { get; set; }
        public int PaymentId { get; set; }
        public int DeliveryId { get; set; }

        public virtual CustomerDTO Customer { get; set; } = null!;
        public virtual PaymentDTO Payment { get; set; } = null!;
        public virtual DeliveryDTO Delivery { get; set; } = null!;

        public List<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
    }
}
