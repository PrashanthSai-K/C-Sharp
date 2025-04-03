using task8;
using task8.Class;

namespace Task8
{
    class Task
    {
        static void Main(string[] args)
        {
            Repository<Product> ProductRepo = new Repository<Product>(); // Product Repo Creation
            Repository<User> UserRepo = new Repository<User>(); //User Repo Creation

            Product product1 = new Product("Mobile", 1234.1f, "eletronics");
            Product product2 = new Product("Laptop", 12324.1f, "eletronics");


            ProductRepo.Add(product1); //Create 
            ProductRepo.Add(product2);


            List<Product> products = ProductRepo.GetAll(); //Read
            Console.WriteLine("\n📝List of Products After Creation : ");
            foreach (var p in products)
            {
                Console.WriteLine($"{p.Name}, {p.Price}, {p.Type} ");
            }

            Product updateProduct = new Product("Computer", 22324.1f, "eletronics");

            ProductRepo.Update(1, updateProduct); //Update

            products = ProductRepo.GetAll(); //Read
            Console.WriteLine("\n📝List of Products After Updation: ");
            foreach (var p in products)
            {
                Console.WriteLine($"{p.Name}, {p.Price}, {p.Type} ");
            }

            ProductRepo.Delete(0); //Delete

            products = ProductRepo.GetAll(); //Read
            Console.WriteLine("\n📝List of Products After Deletion: ");
            foreach (var p in products)
            {
                Console.WriteLine($"{p.Name}, {p.Price}, {p.Type} ");
            }

            Product? product = ProductRepo.Get(0); //Read By ID
            Console.WriteLine("\n📝Get Product BY ID: ");

            if (product != null)
                Console.WriteLine($"{product.Name}, {product.Price}, {product.Type} \n");
            else
                Console.WriteLine("OOPS!!Product not found.\n");

        }
    }
}