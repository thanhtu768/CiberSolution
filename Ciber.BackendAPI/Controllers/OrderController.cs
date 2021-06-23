using Ciber.Services.Catalog.Orders;
using Ciber.ViewModels.Catalog.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ciber.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IManagerOrderService<OrderCreateRequest, OrderUpdateRequest, OrderViewModel, GetOrderPagingRequest> _managerOrderService;
        public OrderController(IManagerOrderService<OrderCreateRequest, OrderUpdateRequest, OrderViewModel, GetOrderPagingRequest> managerOrderService)
        {
            _managerOrderService = managerOrderService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _managerOrderService.GetAll();

            return Ok(orders);
        } 
    }
}
