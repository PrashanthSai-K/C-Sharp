using System;
using System.IO;

namespace Task5
{
    class Task
    {
        static void Main(string[] args)
        {
            try
            {
                if (!File.Exists(@"./data.csv"))
                {
                    List<string> users = new List<string>()
                {
                    "sai, 1, 21",
                    "hai, 2, 31",
                    "kavin, 3, 41"
                };

                    File.WriteAllLines(@"./data.csv", users);
                }

                var lines = File.ReadAllLines(@"./data.csv");

                int lineCount = lines.Length;

                int WordCount = lines.Sum(line => line.Length);

                Console.WriteLine($"\nLine Count : {lineCount}\nWord Count : {WordCount}\n");

                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                }
                Console.WriteLine();

                var list = lines
                .Select(line => line.Split(","))
                .Select(data => $"Name = {data[0]}, Year = {data[1]}, Age = {data[2]}");

                File.WriteAllLines(@"./data.txt", list);

                var newlines = File.ReadAllLines(@"./data.txt");

                foreach (var line in newlines)
                {
                    Console.WriteLine(line);
                }

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            finally
            {
                Console.WriteLine($"\nFile read/write completed\n");
            }

        }
    }
}