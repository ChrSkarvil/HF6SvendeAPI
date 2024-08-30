using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF6Svende.Application.DTO.Listing
{
    public class ListingUpdateDTO
    {
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime? ExpireDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DenyDate { get; set; }

        // Product fields to update
        public string Brand { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Size { get; set; } = null!;

        // Images
        public List<int> ImageIdsToRemove { get; set; } = new List<int>();
        public List<IFormFile> NewImages { get; set; } = new List<IFormFile>();

        // Colors
        public List<string> ColorNames { get; set; } = new List<string>();

    }
}
