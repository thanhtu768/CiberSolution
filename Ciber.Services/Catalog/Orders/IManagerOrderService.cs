using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ciber.Services.Catalog.Orders
{
    interface IManagerOrderService<T>
    {
        Task<int> Create(T obj);
    }
}
