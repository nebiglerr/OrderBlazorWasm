﻿using MealOrdering.Server.Services.Infasture;
using MealOrdering.Shared.Dto;
using MealOrdering.Shared.FilterModels;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealOrdering.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService OrderService)
        {
            orderService = OrderService;
        }



        #region Order Methods

        [HttpGet("OrderById/{Id}")]
        public async Task<ServiceResponse<OrderDto>> GetOrderById(Guid Id)
        {
            return new ServiceResponse<OrderDto>()
            {
                Value = await orderService.GetOrderById(Id)
            };
        }

        [HttpGet("OrdersByDate")]
        public async Task<ServiceResponse<List<OrderDto>>> GetOrder(DateTime OrderDate)
        {
            return new ServiceResponse<List<OrderDto>>()
            {
                Value = await orderService.GetOrders(OrderDate)
            };
        }

        [HttpPost("OrdersByFilter")]
        public async Task<ServiceResponse<List<OrderDto>>> GetOrdersByFilter([FromBody] OrderListFilterModel Filter)
        {
            return new ServiceResponse<List<OrderDto>>()
            {
                Value = await orderService.GetOrdersByFilter(Filter)
            };
        }

        [HttpGet("TodaysOrder")]
        public async Task<ServiceResponse<List<OrderDto>>> GetTodaysOrder()
        {
            return new ServiceResponse<List<OrderDto>>()
            {
                Value = await orderService.GetOrders(DateTime.Now)
            };
        }

        [HttpPost("CreateOrder")]
        public async Task<ServiceResponse<OrderDto>> CreateOrder(OrderDto Order)
        {
            return new ServiceResponse<OrderDto>()
            {
                Value = await orderService.CreateOrder(Order)
            };
        }

        [HttpPost("UpdateOrder")]
        public async Task<ServiceResponse<OrderDto>> UpdateOrder(OrderDto Order)
        {
            return new ServiceResponse<OrderDto>()
            {
                Value = await orderService.UpdateOrder(Order)
            };
        }

        [HttpPost("DeleteOrder")]
        public async Task<BaseResponse> DeleteOrder([FromBody] Guid OrderId)
        {
            await orderService.DeleteOrder(OrderId);
            return new BaseResponse();
        }

        [HttpGet("DeleteOrder/{OrderId}")]
        public async Task<BaseResponse> DeleteOrderFromQueryString(Guid OrderId)
        {
            await orderService.DeleteOrder(OrderId);
            return new BaseResponse();
        }

        #endregion

        #region OrderItem Methods

        #region Get

        [HttpGet("OrderItemsById/{Id}")]
        public async Task<ServiceResponse<OrderItemsDto>> GetOrderItemsById(Guid Id)
        {
            return new ServiceResponse<OrderItemsDto>()
            {
                Value = await orderService.GetOrderItemsById(Id)
            };
        }

        #endregion


        [HttpPost("CreateOrderItem")]
        public async Task<ServiceResponse<OrderItemsDto>> CreateOrderItem(OrderItemsDto OrderItem)
        {
            return new ServiceResponse<OrderItemsDto>()
            {
                Value = await orderService.CreateOrderItem(OrderItem)
            };
        }

        [HttpPost("UpdateOrderItem")]
        public async Task<ServiceResponse<OrderItemsDto>> UpdateOrderItem(OrderItemsDto OrderItem)
        {
            return new ServiceResponse<OrderItemsDto>()
            {
                Value = await orderService.UpdateOrderItem(OrderItem)
            };
        }


        [HttpPost("DeleteOrderItem")]
        public async Task<BaseResponse> DeleteOrderItem([FromBody] Guid OrderItemId)
        {
            await orderService.DeleteOrderItem(OrderItemId);
            return new BaseResponse();
        }

        [HttpGet("OrderItems")]
        public async Task<ServiceResponse<List<OrderItemsDto>>> GetOrderItems(Guid OrderId)
        {
            return new ServiceResponse<List<OrderItemsDto>>()
            {
                Value = await orderService.GetOrderItems(OrderId)
            };
        }

        #endregion

    }
}
