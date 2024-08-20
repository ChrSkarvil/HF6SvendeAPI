using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF6Svende.Application.DTO.Customer
{
    public class CustomerUpdateDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public long Phone { get; set; }
        public string Email { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string CountryName { get; set; } = null!;
        public int? PostalCodeId { get; set; }
        public int? CountryId { get; set; }
    }
}
