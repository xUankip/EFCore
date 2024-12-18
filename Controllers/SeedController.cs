using EFCoreLazyLoadingApp.Data;
using EFCoreLazyLoadingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreLazyLoadingApp.Controllers;

public class SeedController : Controller
{
    private readonly AppDbContext _context;

    public SeedController(AppDbContext context)
    {
        _context = context;
    }

    public void GenerateSeed()
    {
        var users = new List<User>()
        {
            new User { Name = "admin", Email = "admin@admin.com" },
            new User { Name = "user", Email = "user@user.com" },
            new User { Name = "user2", Email = "user2@user.com" },
            new User { Name = "user3", Email = "user3@user.com" },
        };
        _context.Users.AddRange(users);
        _context.SaveChanges();
        var products = Enumerable.Range(1, 30).Select(i => new Product
         {
             Name = $"Product {i}",
             Price = new Random().Next(10, 100)
         }).ToList();

         _context.Products.AddRange(products);
         _context.SaveChanges();

         var orders = users.SelectMany(user =>
             Enumerable.Range(1, 3).Select(_ => new Order
             {
                 UserId = user.Id,
                 OrderDate = DateTime.Now.AddDays(-new Random().Next(1, 30))
             })
         ).ToList();

         _context.Orders.AddRange(orders);
         _context.SaveChanges();

         var orderDetails = orders.SelectMany(order =>
             Enumerable.Range(1, new Random().Next(2, 4)).Select(_ =>
             {
                 var product = products[new Random().Next(products.Count)];
                 return new OrderDetail
                 {
                     OrderId = order.Id,
                     ProductId = product.Id,
                     Quantity = new Random().Next(1, 5),
                     UnitPrice = product.Price
                 };
             })
         ).ToList();

         _context.OrderDetails.AddRange(orderDetails);
         _context.SaveChanges();
    }
    public IActionResult Orders()
    {
        var orders = _context.Orders.ToList();
        return View(orders);
    }
    
}
