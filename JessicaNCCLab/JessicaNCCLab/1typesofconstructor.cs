using System;

namespace JessicaNCCLab
{
    class Student
    {
        public string name;
        public int age;
        public static string college;

        // 1. Static Constructor - Called once before any object is created
        static Student()
        {
            college = "Nagarjuna College of IT";
            Console.WriteLine("Static Constructor called");
        }

        // 2. Default Constructor
        public Student()
        {
            name = "Jessica";
            age = 21;
            Console.WriteLine("Default Constructor called");
        }

        // 3. Parameterized Constructor
        public Student(string name, int age)
        {
            this.name = name;
            this.age = age;
            Console.WriteLine("Parameterized Constructor called");
        }

        // 4. Copy Constructor
        public Student(Student S2)
        {
            name = S2.name;
            age = S2.age;
            Console.WriteLine("Copy Constructor called");
        }

        // 5. Private Constructor - Restricts direct instantiation
        private Student(string secret)
        {
            name = "Hidden";
            age = 0;
            Console.WriteLine("Private Constructor called with secret: " + secret);
        }

        // Static method to access the private constructor
        public static Student GetSecretStudent()
        {
            return new Student("secret123");
        }

        // Display method
        public void Display()
        {
            Console.WriteLine("Name : " + name + "\tAge : " + age + "\tCollege : " + college + "\n");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Using Default Constructor
            Student S1 = new Student();
            S1.Display();

            // Using Parameterized Constructor
            Student S2 = new Student("Ram", 20);
            S2.Display();

            // Using Copy Constructor
            Student S3 = new Student(S2);
            S3.Display();

            // Using Private Constructor via Static Method
            Student S4 = Student.GetSecretStudent();
            S4.Display();

            Console.WriteLine("\nLab No.    : 1");
            Console.WriteLine("Name       : Jessica Maharjan");
            Console.WriteLine("Roll No.   : 9");
        }
    }
}
