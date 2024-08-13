using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF6Svende.Application.DTO.Listing
{
    public class UpdateListingDto
    {
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime? ExpireDate { get; set; }
        public DateTime? SoldDate { get; set; }
        public bool IsActive { get; set; }

        // Product fields to update
        public string Brand { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Size { get; set; } = null!;
    }
}
