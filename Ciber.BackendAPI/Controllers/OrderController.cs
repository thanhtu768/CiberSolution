using Ciber.Services.Catalog.Orders;
using Ciber.ViewModels.Catalog.Orders;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
        [HttpGet("paging-order")]
        public async Task<IActionResult> GetByPaging([FromQuery] GetOrderPagingRequest request)
        {
            var orders = await _managerOrderService.GetAllPaging(request);
            return Ok(orders);
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetOrderByID([FromQuery] int ID)
        {
            var order = await _managerOrderService.GetByID(ID);
            if (order == null)
                return BadRequest($"Can not found Order{ID}");

            return Ok(order);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] OrderCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orderID = await _managerOrderService.Create(request);

            switch (orderID)
            {
                case 0:
                    return BadRequest();
                case -1:
                    return BadRequest("Product quantity is not enough!");
                default:
                    var newOrder = await _managerOrderService.GetByID(orderID);
                    return CreatedAtAction(nameof(GetOrderByID), new { ID = newOrder.ID }, newOrder);
            }


        }
    }
}
