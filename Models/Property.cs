namespace WA_InfoShop.Models;

public class Property : Entity
{
    public string Name { get; set; }

    public IList<ProductProperty>? ProductProperties { get; set; }
}
