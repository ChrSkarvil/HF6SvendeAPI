using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HF6Svende.Application.DTO.Order;
using HF6Svende.Application.DTO.Product;
using HF6Svende.Application.Service_Interfaces;
using HF6Svende.Core.Repository_Interfaces;
using HF6Svende.Infrastructure.Repository;
using HF6SvendeAPI.Data.Entities;

namespace HF6Svende.Application.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;


        public OrderItemService(IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }

        public async Task<OrderItemDTO> CreateOrderItemAsync(OrderItemCreateDTO createOrderItemDto)
        {
            try
            {
                // Mapping dto to entity
                var orderItem = _mapper.Map<OrderItem>(createOrderItemDto);

                // Create orderItem in repository
                var createdOrderItem = await _orderItemRepository.CreateOrderItemAsync(orderItem);

                // Mapping back to dto
                return _mapper.Map<OrderItemDTO>(createdOrderItem);
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
                // Delete order item
                var success = await _orderItemRepository.DeleteOrderItemAsync(id);
                return success;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the order item.", ex);
            }
        }

        public async Task<OrderItemDTO?> GetOrderItemByIdAsync(int id)
        {
            try
            {
                // Get orderItem by id
                var orderItem = await _orderItemRepository.GetOrderItemByIdAsync(id);

                if (orderItem == null) return null;

                // Mapping back to dto
                return _mapper.Map<OrderItemDTO>(orderItem);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the order item.", ex);
            }
        }

        public async Task<List<OrderItemDTO>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            try
            {

                // Get all orderItems
                var orderItems = await _orderItemRepository.GetOrderItemsByOrderIdAsync(orderId);

                // Mapping back to dto
                return _mapper.Map<List<OrderItemDTO>>(orderItems);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the order items.", ex);
            }
        }
    }
}
