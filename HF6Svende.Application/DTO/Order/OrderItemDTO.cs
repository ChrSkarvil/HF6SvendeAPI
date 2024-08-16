using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6Svende.Application.DTO.Listing;

namespace HF6Svende.Application.DTO.Order
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ListingId { get; set; }

        public ListingDTO Listing { get; set; } = null!;
    }
}
