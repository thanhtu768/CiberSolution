using Ciber.Data.EF;
using Ciber.Data.Enititys;
using Ciber.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Ciber.ViewModels;
using Ciber.ViewModels.Catalog.Orders;
using Ciber.ViewModels.Common;

namespace Ciber.Services.Catalog.Orders
{
    public class ManagerOrderService : IManagerOrderService<OrderCreateRequest, OrderUpdateRequest, OrderViewModel, GetOrderPagingRequest>
    {
        private readonly CiberDbContext _context;
        public ManagerOrderService(CiberDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(OrderCreateRequest obj)
        {
            var order = new Order()
            {
                CustomerID = obj.CustomerID,
                ProductID = obj.ProductID,
                Amount = obj.Amount,
                OrderDate = DateTime.Now
            };
            _context.Orders.Add(order);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int Id)
        {
            var order = await _context.Orders.FindAsync(Id);
            if (order == null) 
                throw new CiberManagerException($"Cannot find Order: {Id}");

            _context.Orders.Remove(order);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<OrderViewModel>> GetAll()
        {
            var query = from a in _context.Orders
                        join b in _context.Products on a.ProductID equals b.ID
                        join c in _context.Categories on b.CategoryID equals c.ID
                        join d in _context.Customers on a.CustomerID equals d.ID
                        select new { a, b, c, d };

            int totalRow = await query.CountAsync();
            var data = await query
                .Select(x => new OrderViewModel()
                {
                    ID = x.a.ID,
                    ProductID = x.b.ID,
                    ProductName = x.b.Name,
                    CustomerID = x.d.ID,
                    CustomerName = x.d.Name,
                    TotalPrice = x.b.Price * x.a.Amount,
                    Amount = x.a.Amount,
                    OrderDate = x.a.OrderDate,
                    CategoryName = x.c.Name
                }).ToListAsync();

            

            return data;
        }

        public async Task<PagedResult<OrderViewModel>> GetAllPaging(GetOrderPagingRequest page)
        {
            var query = from a in _context.Orders
                        join b in _context.Products on a.ProductID equals b.ID
                        join c in _context.Categories on b.CategoryID equals c.ID
                        join d in _context.Customers on a.CustomerID equals d.ID
                        select new { a, b, c, d };

            if (!string.IsNullOrEmpty(page.CustomerName))
            {
                query = query.Where(x => x.d.Name.Contains(page.CustomerName));
            }
                
            if(page.OrderID != 0)
            {
                query = query.Where(x => x.a.ID == page.OrderID);
            }
            int totalRow = await query.CountAsync();
            var data = await query.Skip((page.PageIndex - 1)*page.PageSize).Take(page.PageSize)
                .Select(x => new OrderViewModel() {
                    ID = x.a.ID,
                    ProductID = x.b.ID,
                    ProductName = x.b.Name,
                    CustomerID = x.d.ID,
                    CustomerName = x.d.Name,
                    TotalPrice = x.b.Price * x.a.Amount,
                    Amount = x.a.Amount,
                    OrderDate = x.a.OrderDate,
                    CategoryName = x.c.Name
                }).ToListAsync();

            var pageResult = new PagedResult<OrderViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };

            return pageResult;
        }

        public async Task<int> Update(OrderUpdateRequest obj)
        {
            throw new NotImplementedException();
        }
    }
}
