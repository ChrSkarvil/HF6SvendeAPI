using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6Svende.Application.DTO.Customer;
using HF6Svende.Application.DTO.Product;

namespace HF6Svende.Application.Service_Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerDTO>> GetAllCustomersAsync();
        Task<CustomerDTO?> GetCustomerByIdAsync(int id);
        Task<CustomerDTO> CreateCustomerAsync(CustomerCreateDTO createCustomerDto);
        Task<CustomerDTO?> UpdateCustomerAsync(int id, CustomerUpdateDTO updateCustomerDto);
        Task<bool> DeleteCustomerAsync(int id);
    }
}
