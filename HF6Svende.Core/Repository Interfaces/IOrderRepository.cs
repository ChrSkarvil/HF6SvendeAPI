using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6SvendeAPI.Data.Entities;

namespace HF6Svende.Core.Repository_Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task<Order> CreateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
