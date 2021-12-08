using AutoMapper;
using AutoMapper.QueryableExtensions;
using MealOrdering.Server.Data.Context;
using MealOrdering.Server.Services.Infasture;
using MealOrdering.Shared.Dto;
using MealOrdering.Shared.FilterModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealOrdering.Server.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly MealOrderingDbContext context;
        private readonly IMapper mapper;
        private readonly IValidationService validationService;

        public OrderService(MealOrderingDbContext Context, IMapper Mapper, IValidationService ValidationService)
        {
            context = Context;
            mapper = Mapper;
            validationService = ValidationService;
        }


        #region Order Methods


        #region Get

        public async Task<List<OrderDto>> GetOrdersByFilter(OrderListFilterModel Filter)
        {
            var query = context.Orders.Include(i => i.Supplier).AsQueryable();

            if (Filter.CreatedUserId != Guid.Empty)
                query = query.Where(i => i.CreatedUserId == Filter.CreatedUserId);

            if (Filter.CreateDateFirst.HasValue)
                query = query.Where(i => i.CreateDate >= Filter.CreateDateFirst);

            if (Filter.CreateDateLast > DateTime.MinValue)
                query = query.Where(i => i.CreateDate <= Filter.CreateDateLast);


            var list = await query
                      .ProjectTo<OrderDto>(mapper.ConfigurationProvider)
                      .OrderBy(i => i.CreateDate)
                      .ToListAsync();
            /*
                var list = await context.Orders.Include(i => i.Supplier)
                      .ProjectTo<OrderDto>(mapper.ConfigurationProvider)
                      .OrderBy(i => i.CreateDate)
                      .ToListAsync();*/

            return list;
        }


        public async Task<List<OrderDto>> GetOrders(DateTime OrderDate)
        {
            var list = await context.Orders.Include(i => i.Supplier)
                      .Where(i => i.CreateDate.Date == OrderDate.Date)
                      .ProjectTo<OrderDto>(mapper.ConfigurationProvider)
                      .OrderBy(i => i.CreateDate)
                      .ToListAsync();

            return list;
        }



        public async Task<OrderDto> GetOrderById(Guid Id)
        {
            return await context.Orders.Where(i => i.Id == Id)
                      .ProjectTo<OrderDto>(mapper.ConfigurationProvider)
                      .FirstOrDefaultAsync();
        }

        #endregion

        #region Post

        public async Task<OrderDto> CreateOrder(OrderDto Order)
        {
            var dbOrder = mapper.Map<Data.Models.Orders>(Order);
            await context.AddAsync(dbOrder);
            await context.SaveChangesAsync();

            return mapper.Map<OrderDto>(dbOrder);
        }

        public async Task<OrderDto> UpdateOrder(OrderDto Order)
        {
            var dbOrder = await context.Orders.FirstOrDefaultAsync(i => i.Id == Order.Id);
            if (dbOrder == null)
                throw new Exception("Order not found");


            if (!validationService.HasPermission(dbOrder.CreatedUserId))
                throw new Exception("You cannot change the order unless you created");

            mapper.Map(Order, dbOrder);
            await context.SaveChangesAsync();

            return mapper.Map<OrderDto>(dbOrder);
        }

        public async Task DeleteOrder(Guid OrderId)
        {
            var detailCount = await context.OrderItems.Where(i => i.OrderId == OrderId).CountAsync();


            if (detailCount > 0)
                throw new Exception($"There are {detailCount} sub items for the order you are trying to delete");

            var order = await context.Orders.FirstOrDefaultAsync(i => i.Id == OrderId);
            if (order == null)
                throw new Exception("Order not found");


            if (!validationService.HasPermission(order.CreatedUserId))
                throw new Exception("You cannot change the order unless you created");



            context.Orders.Remove(order);

            await context.SaveChangesAsync();
        }

        #endregion

        #endregion


        #region OrderItem Methods

        #region Get

        public async Task<List<OrderItemsDto>> GetOrderItems(Guid OrderId)
        {
            return await context.OrderItems.Where(i => i.OrderId == OrderId)
                      .ProjectTo<OrderItemsDto>(mapper.ConfigurationProvider)
                      .OrderBy(i => i.CreateDate)
                      .ToListAsync();
        }

        public async Task<OrderItemsDto> GetOrderItemsById(Guid Id)
        {
            return await context.OrderItems.Include(i => i.Orders).Where(i => i.Id == Id)
                      .ProjectTo<OrderItemsDto>(mapper.ConfigurationProvider)
                      .FirstOrDefaultAsync();
        }

        #endregion

        #region Post


        public async Task<OrderItemsDto> CreateOrderItem(OrderItemsDto OrderItem)
        {
            var order = await context.Orders
                .Where(i => i.Id == OrderItem.OrderId)
                .Select(i => i.ExpireDate)
                .FirstOrDefaultAsync();

            if (order == null)
                throw new Exception("The main order not found");

            if (order <= DateTime.Now)
                throw new Exception("You cannot create sub order. It is expired !!!");


            var dbOrder = mapper.Map<Data.Models.OrderItems>(OrderItem);
            await context.AddAsync(dbOrder);
            await context.SaveChangesAsync();

            return mapper.Map<OrderItemsDto>(dbOrder);
        }

        public async Task<OrderItemsDto> UpdateOrderItem(OrderItemsDto OrderItem)
        {
            var dbOrder = await context.OrderItems.FirstOrDefaultAsync(i => i.Id == OrderItem.Id);
            if (dbOrder == null)
                throw new Exception("Order not found");

            mapper.Map(OrderItem, dbOrder);
            await context.SaveChangesAsync();

            return mapper.Map<OrderItemsDto>(dbOrder);
        }

        public async Task DeleteOrderItem(Guid OrderItemId)
        {
            var orderItem = await context.OrderItems.FirstOrDefaultAsync(i => i.Id == OrderItemId);
            if (orderItem == null)
                throw new Exception("Sub order not found");

            context.OrderItems.Remove(orderItem);

            await context.SaveChangesAsync();
        }

        #endregion

        #endregion
    }
}

