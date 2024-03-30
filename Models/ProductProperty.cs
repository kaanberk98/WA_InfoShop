namespace WA_InfoShop.Models;

public class ProductProperty : Entity
{
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int PropertyId { get; set; }
    public Property? Property { get; set; }
    public string Name { get; set; }
}
