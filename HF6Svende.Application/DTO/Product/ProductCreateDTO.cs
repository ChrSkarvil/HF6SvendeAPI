using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF6Svende.Application.DTO.Product
{
    public class ProductCreateDTO
    {
        public string Brand { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Size { get; set; } = null!;
        public int CustomerId { get; set; }
        public int CategoryId { get; set; }
    }
}
