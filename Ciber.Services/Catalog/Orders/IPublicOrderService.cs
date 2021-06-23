
using Ciber.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ciber.Services.Catalog.Orders
{
    interface IPublicOrderService<ObjView,ReqPage>
    {
        Task<PagedResult<ObjView>> GetOrderByCusID(ReqPage request);

    }
}
