using Ciber.Services.Catalog.Products.Dtos;
using Ciber.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ciber.Services.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    {
        public PagedViewModel<ProductViewModel> GetAllByCategory(int categoryID)
        {
            throw new NotImplementedException();
        }
    }
}
