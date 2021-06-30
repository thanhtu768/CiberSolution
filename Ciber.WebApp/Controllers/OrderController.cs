using Ciber.ViewModels.Catalog.Orders;
using Ciber.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ciber.WebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderApiClient _orderApiClient;
        public OrderController(IOrderApiClient orderApiClient)
        {
            _orderApiClient = orderApiClient;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var token = HttpContext.Session.GetString("Token");
            var request = new GetOrderPagingRequest() {
                BearerToken = token,
                CategoryName = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _orderApiClient.GetOrderPaging(request);
            return View(data);
        }
    }
}
