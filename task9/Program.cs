
using System.Reflection;
using task9.Attribute;
using task9.Class;

namespace Task9
{
    class Tasks
    {
        static void Main(string[] args)
        {
            Type type = typeof(Product); //Reflection class to get Type of the Class given

            Console.WriteLine("\nClass Name : " + type.FullName);

            Product? obj = (Product?)Activator.CreateInstance(type, "Earphone", 123.0f, "electronics"); //Activator class to instantiate object dynamically
           
            Console.WriteLine();
            foreach (var prop in type.GetProperties())
            {
                Console.WriteLine($"Property in Product Class : {prop.Name} - {prop.PropertyType.Name}");
            }
            Console.WriteLine();

            foreach (var method in type.GetMethods())
            {
                if (method.GetCustomAttribute<RunnableAttribute>() != null)
                {
                    method.Invoke(obj, null);
                }
                else
                {
                    Console.WriteLine($"{method.Name} method does not have Runnable attribute.");
                }
            }
            Console.WriteLine();
        }
    }
}