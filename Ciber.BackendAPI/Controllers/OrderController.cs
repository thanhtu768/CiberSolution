using Ciber.Data.EF;
using Ciber.Services.Catalog;
using Ciber.Services.Catalog.Orders;
using Ciber.Services.System.Logger;
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
   
    public class OrderController : ControllerBase
    {
        //private readonly IManagerOrderService<OrderCreateRequest, OrderUpdateRequest, OrderViewModel, GetOrderPagingRequest> _managerOrderService;
        private readonly ILoggerManager _logger;
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public OrderController(ILoggerManager loggerManager)
        {
            //_managerOrderService = _unitOfWork.OrdeService;
            _logger = loggerManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _unitOfWork.OrdeService.GetAll();
            //_logger.LogError("Test logger service from order controller");
            return Ok(orders);
        }
        [HttpGet("list-source-prod")]
        public async Task<IActionResult> GetListSourceProduct()
        {
            var orders = await _unitOfWork.OrdeService.ListSourceProduct();
            //_logger.LogError("Test logger service from order controller");
            return Ok(orders);
        }
        [HttpGet("list-source-cus")]
        public async Task<IActionResult> GetListSourceCustomer()
        {
            var orders = await _unitOfWork.OrdeService.ListSourceCustomer();
            //_logger.LogError("Test logger service from order controller");
            return Ok(orders);
        }
        [HttpGet("paging-order")]
        public async Task<IActionResult> GetByPaging([FromQuery] GetOrderPagingRequest request)
        {
            var pageResult = await _unitOfWork.OrdeService.GetAllPaging(request);
            return Ok(pageResult);
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetOrderByID([FromQuery] int ID)
        {
            var order = await _unitOfWork.OrdeService.GetByID(ID);
            if (order == null)
                return BadRequest($"Can not found Order{ID}");

            return Ok(order);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] OrderCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             _unitOfWork.OrdeService.Create(request);

             _unitOfWork.SaveChangeAsync();
            return Ok();
        }
    }
}
