using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF6Svende.Application.DTO.Product
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Brand { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Size { get; set; } = null!;
        public string? CategoryName { get; set; } = null!;
        public int CategoryId { get; set; }

        public List<ImageDto> Images { get; set; } = new List<ImageDto>();
    }
}
