using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6Svende.Application.DTO.Product;

namespace HF6Svende.Application.DTO.Listing
{
    public class ListingDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public DateTime? SoldDate { get; set; }
        public bool IsActive { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; } = null!;

        //Product data
        public ProductDTO Product { get; set; } = null!;
    }
}
