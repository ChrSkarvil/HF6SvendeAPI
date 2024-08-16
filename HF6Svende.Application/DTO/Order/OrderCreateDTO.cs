using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF6Svende.Application.DTO.Order
{
    public class OrderCreateDTO
    {
        public DateTime CreateDate { get; set; }
        public int CustomerId { get; set; }
        public int PaymentId { get; set; }
        public int DeliveryId { get; set; }
        public List<OrderItemCreateDTO> OrderItems { get; set; } = new List<OrderItemCreateDTO>();
    }
}
