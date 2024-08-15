using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HF6Svende.Application.DTO.Product
{
    public class ProductCreateDTO
    {
        public string Brand { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Size { get; set; } = null!;
        public int CategoryId { get; set; }

        public List<IFormFile> Images { get; set; } = new List<IFormFile>();

    }
}
