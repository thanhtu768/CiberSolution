using Ciber.ViewModels.Catalog.Products;
using Ciber.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ciber.Services.Catalog.Products
{
    public interface IPublicProductService
    {
        PagedResult<ProductViewModel> GetAllByCategory(int categoryID);
    }
}
