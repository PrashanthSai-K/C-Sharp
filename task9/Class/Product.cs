using System;
using System.Net.NetworkInformation;
using task9.Attribute;

namespace task9.Class;

[Runnable]
public class Product
{
    public Product(string name, float price, string type)
    {
        Name = name;
        Price = price;
        Type = type;
    }
    public string Name
    { get; }

    public float Price
    { get; }

    public string Type
    { get; }

    [Runnable]
    public void ShowProduct()
    {
        Console.WriteLine($"Runnable Attribute Method to show entire Product: {Name} - {Price} - {Type}");
    }

    [Runnable]
    public void ShowProductName()
    {
        Console.WriteLine($"Runnable Attribute Method to show Product Name : {Name}");
    }

    public void ShowProductPrice()
    {
        Console.WriteLine($"Non-Runnable Method to show Product Price : {Price}");
    }

}

