using System;

namespace Task4
{
    class Student
    {
        public Student(string name, int age, string grade)
        {
            Name = name;
            Age = age;
            Grade = grade;
        }
        public string Name
        {
            get; set;
        }

        public int Age
        {
            get; set;
        }

        public string Grade
        {
            get; set;
        }

    }
    class Task
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();

            students.Add(new Student("Sai", 23, "A"));
            students.Add(new Student("Hari", 22, "B"));
            students.Add(new Student("Guru", 21, "C"));
            students.Add(new Student("Kavin", 21, "D"));
            students.Add(new Student("Keerthi", 22, "E"));

            var filteredList = from student in students
                               where string.Compare(student.Grade, "B") > 0
                               select student;

            var sortedList = students.OrderBy(student => student.Age);

            var groupedList = students.GroupBy(student => student.Age % 2);

            Console.WriteLine($"\nSorted List : ");

            foreach (var student in sortedList)
            {
                Console.WriteLine($"{student.Name},  {student.Age},  {student.Grade}");
            }

            Console.WriteLine($"\nFiltered List : ");

            foreach (var student in filteredList)
            {
                Console.WriteLine($"{student.Name},  {student.Age}, {student.Grade}");

            }

            Console.WriteLine($"\nGrouped List : ");

            foreach (var group in groupedList)
            {
                Console.WriteLine($"\nGroup Key: {group.Key}");
                foreach (var student in group)
                {
                    Console.WriteLine($"{student.Name}, {student.Age}, {student.Grade}");
                }
            }
        }
    }
}