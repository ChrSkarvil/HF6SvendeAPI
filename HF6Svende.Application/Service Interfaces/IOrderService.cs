using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6Svende.Application.DTO.Order;

namespace HF6Svende.Application.Service_Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDTO?> GetOrderByIdAsync(int id);
        Task<OrderDTO> CreateOrderAsync(OrderCreateDTO createOrderDto);
        Task<bool> DeleteOrderAsync(int id);
    }
}
