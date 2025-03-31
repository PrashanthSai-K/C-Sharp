namespace Task1
{
    class Task
    {
        static int FactotialFunc(int num)
        {
            if(num == 0){
                return 1;
            }else{
                return num * FactotialFunc(num-1);
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter number to find factorial : ");
                int number = Convert.ToInt32(Console.ReadLine());
                if(number <= 0){
                    Console.WriteLine("Enter a valid positive number");
                    continue;
                }
                Console.WriteLine("Factorial of the given number : {0}", FactotialFunc(number));
            }
        }

    }
}