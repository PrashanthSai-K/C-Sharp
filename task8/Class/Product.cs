using System;

namespace task8;

public class Product
{
    public Product(string name, float price, string type)
    {
        Name = name;
        Price = price;
        Type = type;
    }
    public string Name
    { get; set; }

    public float Price
    { get; set; }

    public string Type
    { get; set; }

}
