namespace WA_InfoShop.Models;

public class Category : Entity
{
    public string Name { get; set; }
    public int SupCategoryId { get; set; }

    public IList<Product>? Products { get; set; }
}