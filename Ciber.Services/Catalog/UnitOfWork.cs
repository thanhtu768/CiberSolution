using Ciber.Data.EF;
using Ciber.Services.Catalog.Orders;
using Ciber.Services.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ciber.Services.Catalog
{
    public class UnitOfWork
    {
        private ManagerOrderService _orderService;
        private ManageProductService _productService;
        private CiberDbContext _dbContext ;
        public UnitOfWork()
        {
            _dbContext = new CiberDbContextFactory().CreateDbContext(null);
        }
        public ManagerOrderService OrdeService { get
            {
                if(_orderService == null)
                {
                    _orderService = new ManagerOrderService(_dbContext);
                }
                return _orderService;
            } 
        }
        public ManageProductService ProductService { get { 
            if(_productService == null)
                {
                    _productService = new ManageProductService(_dbContext);
                }
                return _productService;
            }
        }
        public void SaveChange()
        {
            _dbContext.SaveChanges();
        }
        public void SaveChangeAsync()
        {
            _dbContext.SaveChangesAsync();
        }
    }
}
