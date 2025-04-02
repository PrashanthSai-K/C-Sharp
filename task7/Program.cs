using System.Threading.Tasks;

namespace Task7
{
    class Tasks
    {
        public static async Task<int> APICallOne()
        {
            try
            {
                Console.WriteLine("Call 1 Started...");
                await Task.Delay(5000);
                Console.WriteLine("Call 1 Completed");
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception thrown : {ex.Message}");
                return -1;
            }
        }

        public static async Task<int> APICallTwo()
        {
            try
            {
                Console.WriteLine("Call 2 Started...");
                await Task.Delay(5000);
                throw new Exception("Error in API call 2");
                // Console.WriteLine("Call 2 Completed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception thrown : {ex.Message}");
                return -1;
            }
        }

        public static async Task<int> APICallThree()
        {
            try
            {
                Console.WriteLine("Call 3 Started...");
                await Task.Delay(5000);
                Console.WriteLine("Call 3 Completed");
                return 3;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception thrown : {ex.Message}");
                return -1;
            }
        }



        static async Task Main(string[] args)
        {
            Task<int>[] tasks = {
                APICallOne(),
                APICallTwo(),
                APICallThree()
            };

            int[] results = await Task.WhenAll(tasks);

            Console.WriteLine($"\nAll API calls completed.\nResult 1 : {results[0]}\nResult 2 :{results[1]}\nResult 3 : {results[2]}\n");
        }
    }
}