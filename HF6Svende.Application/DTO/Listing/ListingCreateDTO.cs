using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6Svende.Application.DTO.Product;

namespace HF6Svende.Application.DTO.Listing
{
    public class ListingCreateDTO
    {
        public string Title { get; set; } = null!;

        public decimal Price { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public DateTime? SoldDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsListingVerified { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public ProductCreateDTO? Product { get; set; } = null!;



    }
}
