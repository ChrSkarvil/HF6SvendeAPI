using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6SvendeAPI.Data.Entities;

namespace HF6Svende.Core.Repository_Interfaces
{
    public interface IOrderItemRepository
    {
        Task<OrderItem?> GetOrderItemByIdAsync(int id);
        Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem);
        Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
        Task<bool> DeleteOrderItemAsync(int id);
    }
}
