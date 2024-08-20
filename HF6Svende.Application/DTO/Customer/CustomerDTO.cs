using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6SvendeAPI.Data.Entities;

namespace HF6Svende.Application.DTO.Customer
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public long Phone { get; set; }
        public string Email { get; set; } = null!;
        public DateTime? CreateDate { get; set; }
        public string? PostalCode { get; set; } = null!;
        public string? CountryName { get; set; } = null!;
        public int PostalCodeId { get; set; }
        public int CountryId { get; set; }
    }
}
