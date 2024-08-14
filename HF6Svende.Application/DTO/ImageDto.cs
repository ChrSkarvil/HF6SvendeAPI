using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF6Svende.Application.DTO
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string FileBase64 { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public bool IsVerified { get; set; }
    }
}
