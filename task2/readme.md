
# Task 2: Simple Object-Oriented Programming (OOP)


### 🎯Objective :

- Implement a Person class in C# to demonstrate fundamental OOP concepts such as properties, methods, and object instantiation.

###  ✅  Requirements :

- Define a `Person` class with properties such as `Name` and `Age`.
- Implement a method `Introduce()` that prints a personalized greeting.
- Create instances of the Person class in the Main method.
- Call `Introduce()` on each instance to display their information.

###  🛠 Implementation Steps


#### 1️⃣ Define the Person Class

- Add `Name` and `Age` properties.
    - Properties are something that is a combination of private scoped variable and a public scoped function with getter and setter.
- Create `constructor` to initialize the properties.
- Write `Introduce()` function inside the class.

#### 2️⃣ Instantiate and Use the Class
- In the main method, create multiple instances of the `Person` class.
- Call `Introduce()` function on each created object.

### 📝 Code

```
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

```

### 📌 Output

![View 1](./images/image1.png)