namespace WA_InfoShop.Models;

public class Brand : Entity
{
    public string Name { get; set; }

    public IList<Product> Products { get; set; }
}
