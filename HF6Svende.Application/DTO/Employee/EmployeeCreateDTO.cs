using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF6Svende.Application.DTO.Employee
{
    public class EmployeeCreateDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public long Phone { get; set; }
        public decimal Salary { get; set; }
        public string Email { get; set; } = null!;
        public DateTime HireDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string PostalCode { get; set; } = null!;
        public string CountryName { get; set; } = null!;
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
        public int? PostalCodeId { get; set; }
        public int? CountryId { get; set; }
    }
}
