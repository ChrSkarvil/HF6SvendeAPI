using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HF6Svende.Application.DTO.Order;
using HF6Svende.Application.DTO.Product;
using HF6Svende.Application.Service_Interfaces;
using HF6Svende.Core.Interfaces;
using HF6Svende.Core.Repository_Interfaces;
using HF6Svende.Infrastructure.Repository;
using HF6SvendeAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HF6Svende.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IListingRepository _listingRepository;
        private readonly IMapper _mapper;


        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IListingRepository listingRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _listingRepository = listingRepository;
            _mapper = mapper;
        }

        public async Task<OrderDTO> CreateOrderAsync(OrderCreateDTO createOrderDto)
        {
            try
            {
                // Mapping dto to entity
                var order = _mapper.Map<Order>(createOrderDto);

                // Create order in repository
                var createdOrder = await _orderRepository.CreateOrderAsync(order);

                if (createdOrder == null)
                {
                    throw new Exception("Failed to create the order.");
                }

                bool anyListingFailed = false;

                // Save the orderitems
                foreach (var itemDto in createOrderDto.OrderItems)
                {
                    // Get the Listing by ListingId
                    var listing = await _listingRepository.GetListingByIdAsync(itemDto.ListingId);

                    if (listing == null)
                    {
                        throw new Exception($"Listing with ID {itemDto.ListingId} not found.");
                    }

                    // Create orderitem if listings solddate is null
                    if (listing.SoldDate == null)
                    {
                        await _listingRepository.SetSoldDateAsync(itemDto.ListingId, DateTime.UtcNow);

                        var orderItem = _mapper.Map<OrderItem>(itemDto);
                        orderItem.OrderId = createdOrder.Id; 
                        await _orderItemRepository.CreateOrderItemAsync(orderItem);
                    }
                    else
                    {
                        anyListingFailed = true;
                        break;
                    }
                }

                if (anyListingFailed)
                {
                    // Rollback changes by deleting the created order
                    await _orderRepository.DeleteOrderAsync(createdOrder.Id);
                    throw new Exception("Failed to create order because one or more listings have already been sold.");
                }

                // Mapping back to dto
                var orderWithItems = await _orderRepository.GetOrderByIdAsync(createdOrder.Id);

                if (orderWithItems == null)
                {
                    throw new Exception("Failed to retrieve the created order.");
                }

                return _mapper.Map<OrderDTO>(orderWithItems);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the order.", ex);
            }
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            try
            {
                // Delete order
                var success = await _orderRepository.DeleteOrderAsync(id);
                return success;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the order.", ex);
            }
        }

        public async Task<List<OrderDTO>> GetAllOrdersAsync()
        {
            try
            {

                // Get all orders
                var orders = await _orderRepository.GetAllOrdersAsync();

                // Mapping back to dto
                return _mapper.Map<List<OrderDTO>>(orders);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the orders.", ex);
            }
        }

        public async Task<OrderDTO?> GetOrderByIdAsync(int id)
        {
            try
            {
                // Get order by id
                var order = await _orderRepository.GetOrderByIdAsync(id);

                if (order == null) return null;

                // Mapping back to dto
                return _mapper.Map<OrderDTO>(order);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the order.", ex);
            }
        }
    }
}
