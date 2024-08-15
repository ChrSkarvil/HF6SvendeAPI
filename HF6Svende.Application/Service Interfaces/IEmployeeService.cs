using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6Svende.Application.DTO.Employee;

namespace HF6Svende.Application.Service_Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetAllEmployeesAsync();
        Task<EmployeeDTO?> GetEmployeeByIdAsync(int id);
        Task<EmployeeDTO> CreateEmployeeAsync(EmployeeCreateDTO createEmployeeDto);
        Task<EmployeeDTO?> UpdateEmployeeAsync(int id, EmployeeUpdateDTO updateEmployeeDto);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
