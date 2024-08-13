using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF6Svende.Application.DTO
{
    public class ListingDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public DateTime? SoldDate { get; set; }
        public bool IsActive { get; set; }
        public string? CustomerName { get; set; } = null!;

        //Product data
        public ProductDto Product { get; set; } = null!;
    }
}
