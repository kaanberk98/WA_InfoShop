﻿namespace WA_InfoShop.Models;

public class Product : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public int BrandId { get; set; }
    public Brand? Brand { get; set; }
    public IList<ProductProperty>? ProductProperties { get; set; }
}
