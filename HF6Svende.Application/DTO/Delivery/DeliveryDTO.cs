using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF6Svende.Application.DTO.Delivery
{
    public class DeliveryDTO
    {
        public int Id { get; set; }
        public string Address { get; set; } = null!;
        public DateTime? DispatchedDate { get; set; }
        public DateTime? EstDeliveryDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public decimal DeliveryFee { get; set; }
        public string PostalCode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public int PostalCodeId { get; set; }
        public int CountryId { get; set; }

    }
}
