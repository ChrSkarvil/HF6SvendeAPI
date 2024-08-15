using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HF6Svende.Application.DTO.Image
{
    public class ImageCreateDTO
    {
        public IFormFile File { get; set; } = null!;

        public DateTime? CreateDate { get; set; }

        public bool IsVerified { get; set; }

        public int ProductId { get; set; }
    }
}
