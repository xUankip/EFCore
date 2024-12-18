using System.ComponentModel.DataAnnotations;

namespace EFCoreLazyLoadingApp.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    [StringLength(50)]
    [MinLength(2)]
    public string Name { get; set; }
    public decimal Price { get; set; }
    
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}