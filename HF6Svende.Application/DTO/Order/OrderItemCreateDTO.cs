using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF6Svende.Application.DTO.Order
{
    public class OrderItemCreateDTO
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ListingId { get; set; }
    }
}
