using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF6Svende.Application.DTO.Product
{
    public class ProductCreateImageDTO
    {
        public string FileBase64 { get; set; } = null!;
        public bool IsVerified { get; set; }
    }
}
