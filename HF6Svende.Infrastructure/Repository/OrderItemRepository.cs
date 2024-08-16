using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF6Svende.Core.Repository_Interfaces;
using HF6SvendeAPI.Data;
using HF6SvendeAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HF6Svende.Infrastructure.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly DemmacsWatchesDbContext _context;

        public OrderItemRepository(DemmacsWatchesDbContext context)
        {
            _context = context;
        }
        public async Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem)
        {
            try
            {
                _context.OrderItems.Add(orderItem);
                await _context.SaveChangesAsync();
                return orderItem;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the order item.", ex);
            }
        }

        public async Task<bool> DeleteOrderItemAsync(int id)
        {
            try
            {
                var orderItem = await _context.OrderItems.FindAsync(id);
                if (orderItem == null)
                {
                    return false;
                }

                _context.OrderItems.Remove(orderItem);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the order item.", ex);
            }
        }

        public async Task<OrderItem?> GetOrderItemByIdAsync(int id)
        {
            try
            {
                return await _context.OrderItems.Include(l => l.Order).Include(l => l.Listing).FirstOrDefaultAsync(l => l.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the order item.", ex);
            }
        }

        public async Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            try
            {
                return await _context.OrderItems.Where(l => l.OrderId == orderId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the order items.", ex);
            }
        }
    }
}
