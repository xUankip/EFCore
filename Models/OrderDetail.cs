using System.ComponentModel.DataAnnotations.Schema;
namespace EFCoreLazyLoadingApp.Models;

public class OrderDetail
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public virtual Order Orders { get; set; }
    public virtual Product Products { get; set; }
}