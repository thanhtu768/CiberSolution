using Ciber.ViewModels.Catalog.Orders;
using Ciber.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ciber.WebApp.Services
{
    public interface IOrderApiClient
    {
        Task<PagedResult<OrderViewModel>> GetOrderPaging(GetOrderPagingRequest request);
    }
}
