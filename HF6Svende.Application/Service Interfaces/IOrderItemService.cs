using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6Svende.Application.DTO.Order;

namespace HF6Svende.Application.Service_Interfaces
{
    public interface IOrderItemService
    {
        Task<List<OrderItemDTO>> GetOrderItemsByOrderIdAsync(int orderId);
        Task<OrderItemDTO?> GetOrderItemByIdAsync(int id);
        Task<OrderItemDTO> CreateOrderItemAsync(OrderItemCreateDTO createOrderItemDto);
        Task<bool> DeleteOrderItemAsync(int id);

    }
}
