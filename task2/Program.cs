using System;

class Person
{
    public Person(string name, int age)
    {
        this.name = name;
        Age = age;
    }
    
    private string name; //Field
    public string Name //Property
    {
        get { return name; }
        set { name = value; }

    }

    public int Age //Short hand
    {
        get; set;
    }

    public void Introduce()
    {
        Console.WriteLine("Hi, My name is {0}. And my age is {1}", Name, Age);
    }

}

class Program
{
    static void Main(string[] args)
    {
        Person person1 = new Person("Sai", 21);
        Person person2 = new Person("Hari", 20);
        Person person3 = new Person("Kavin", 20);
        Person person4 = new Person("Guru", 19);

        person1.Introduce();
        person2.Introduce();
        person3.Introduce();
        person4.Introduce();
    }
}