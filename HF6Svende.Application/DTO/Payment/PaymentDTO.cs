using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF6Svende.Application.DTO.Payment
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Method { get; set; } = null!;
        public decimal Amount { get; set; }
        public int CustomerId { get; set; }

    }
}
