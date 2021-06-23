using Ciber.Services.Catalog.Products.Dtos;
using Ciber.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ciber.Services.Catalog.Products
{
    public interface IPublicProductService
    {
        PagedViewModel<ProductViewModel> GetAllByCategory(int categoryID);
    }
}
