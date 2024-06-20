//---------------------------------------------------------------
//                 P175B123 for English students
//                 Exam sample task. Question1
//---------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Question1
{
    /// <summary>
    /// Class to store data about single person
    /// </summary>
    public class Person
    {
        // Auto-properties
        public string nameSurname { get; set; } // pavardė, vardas
        public int Age { get; set; }            // Age
        public TimeSpan Arrival { get; set; }   // Arrival time (hour:minute:second)

        // Constructor with parameters
        public Person(string nameSurname, int Age, TimeSpan Arrival)
        {
            this.nameSurname = nameSurname;
            this.Age = Age;
            this.Arrival = Arrival;
        }

        // Method for creating a string output from class properties 
        public override string ToString()
        {
            string eilute;
            eilute = string.Format("{0, -17} {1}   {2}",
                                    nameSurname, Age, Arrival);
            return eilute;
        }

        // Overriden method Equals()
        public override bool Equals(object myObject)
        {
            Person person = myObject as Person;
            return person.nameSurname == nameSurname &&
                   person.Age == Age;
        }

        // Overriden method GetHashCode()
        public override int GetHashCode()
        {
            return nameSurname.GetHashCode() ^
                   Age.GetHashCode();
        }
    }

    class Program
    {
        const string CFd = "Persons.txt";

        static void Main(string[] args)
        {
            // Declaration of objects
            List<Person> PersonList = ReadPersons(CFd);     // Dynamic array (List) of persons
            PrintPersons(PersonList, "Initial data");       // Print list of persons to console
            Queue<Person> Q = new Queue<Person>();          // Queue Q
            TimeSpan timeStart = new TimeSpan(7, 0, 0);     // Start time
            TimeSpan timeEnd = new TimeSpan(10, 0, 0);      // End time
            TimeSpan timeStep = new TimeSpan(0, 10, 0);     // Time step
            int[] ArrayOfKeys = { 7, 9, 4, 1, 6, 2, 3 };    // Keys

            // TODO: execute required calculations and solve task
            // LOOP operators (for, foreach, while) are NOT USED IN MAIN!!!
            CreateQueue(PersonList, Q, timeStart, timeEnd, timeStep);
            PrintQueue("Created Queue. Task 1", Q);
            int qCount = Q.Count;
            Console.WriteLine("Number of elements in Q: {0}", qCount);
            object item = Q.Peek();
            Console.WriteLine("First element of object Q: {0}", item);

            SortedDictionary<int, Person> SD = new SortedDictionary<int, Person>();
            int fAge = ((Person)item).Age;
            CreateSortedDictionary(PersonList, SD, fAge, ArrayOfKeys);
            if(SD.Count > 0)
            {
                Console.WriteLine("Sorted Dictionary");
                Print(SD);
                Console.WriteLine("Enter the key");
                int SKey = int.Parse(Console.ReadLine());
                if (SD.ContainsKey(SKey))
                {
                    Person person;
                    SD.TryGetValue(SKey, out person);
                    Console.WriteLine("Value was found {0}", person.ToString());
                }
                else
                {
                    Console.WriteLine("No people found");
                }
                int youngest = SD.Values.Min(c => c.Age);
                int average = (int)SD.Values.Average(c=>c.Age);
                Console.WriteLine("Average age: {0}", average);
            }
            else
            {
                Console.WriteLine("Dictionary is empty");
            }

        }

        /// <summary>
        /// Reads initial data
        /// </summary>
        /// <param name="fv">Name of the file</param>
        /// <returns>Dynamic array (List) of persons </returns>
        static List<Person> ReadPersons(string fv)
        {
            List<Person> PersonList = new List<Person>();
            using (StreamReader reader = new StreamReader(fv, Encoding.UTF8))
            {
                string mLine;
                while ((mLine = reader.ReadLine()) != null)
                {
                    string[] parts = mLine.Split(';');
                    string nameSurname = parts[0];
                    int Age = int.Parse(parts[1]);
                    TimeSpan Arrival = TimeSpan.Parse(parts[2]);
                    Person person = new Person(nameSurname, Age, Arrival);
                    PersonList.Add(person);
                }
            }
            return PersonList;
        }

        /// <summary>
        /// Prints contents of dynamic array using table format
        /// </summary>
        /// <param name="PersonList">Container of persons</param>
        /// <param name="header">Label (note) above table</param>
        static void PrintPersons(List<Person> PersonList, string header)
        {
            const string top =
                "--------------------------------------- \r\n" +
                " No. Surname, name  Age  Arrival Time \r\n" +
                "---------------------------------------";
            Console.WriteLine("\n " + header);
            Console.WriteLine(top);
            for (int i = 0; i < PersonList.Count; i++)
            {
                Person person = PersonList[i];
                Console.WriteLine("{0, 3} {1}", i + 1, person);
            }
            Console.WriteLine("---------------------------------------\n");
        }

        /// <summary>
        /// Prints contents of sorted dictionary using enumerators
        /// </summary>
        /// <param name="SD">Object of sorted dictionary</param>
        public static void Print(SortedDictionary<int, Person> SD)
        {
            var enumerator = SD.GetEnumerator();
            while (enumerator.MoveNext())
            {
                object item = enumerator.Current;
                Console.WriteLine(" {0} ", item);
            }
            Console.WriteLine();
        }
        public static void PrintQueue(string note, Queue<Person> Q)
        {
            Console.WriteLine(note);
            foreach(Person person in Q)
            {
                Console.WriteLine("{0, -10}", person.ToString());
            }
        }

        // TODO: implement method by given header
        /// <summary>
        ///  Create a Queue Q of persons from dynamic array (List)
        /// </summary>
        /// <param name="PersonList">Dynamic array (List) of persons</param>
        /// <param name="Q">Queue object</param>
        /// <param name="timeStart">Start time(hour:minute:second)</param>
        /// <param name="timeEnd">End time (hour:minute:second)</param>
        /// <param name="timeStep">Time step (hour:minute:second)</param>
        static void CreateQueue(List<Person> PersonList, Queue<Person> Q,
                           TimeSpan timeStart, TimeSpan timeEnd, TimeSpan timeStep)
        {
            for (int i = 0; i < PersonList.Count; i++)
            {
                Person p = PersonList[i];
                if (p.Arrival > timeStart && p.Arrival < timeEnd && (int)(p.Arrival.Minutes % timeStep.Minutes) == 0)
                {
                    Person newP = new Person(p.nameSurname, p.Age, p.Arrival);
                    Q.Enqueue(newP);
                }
            }

        }

        // TODO: implement method by given header
        /// <summary>
        ///  Create a sorted dictionary SD of persons from dynamic array (List) whose age is older than age of first person from Q
        /// </summary>
        /// <param name="PersonList">Dynamic array (List) of person</param>
        /// <param name="SD">Object of sorted dictionary</param>
        /// <param name="fAge">Filter criteria for age</param>
        /// <param name="ArrayOfKeys">Array of keys: added sequentially to sorted dictionary </param>
        static void CreateSortedDictionary(List<Person> PersonList, SortedDictionary<int, Person> SD,
                                            int fAge, int[] ArrayOfKeys)
        {
            int numberOfKeys = 0;
            for(int i = 0; i< PersonList.Count;i++)
            {
                if (PersonList[i].Age>fAge)
                {
                    if (numberOfKeys < ArrayOfKeys.Length)
                    {
                        Person temp = new Person(PersonList[i].nameSurname, PersonList[i].Age,
                        PersonList[i].Arrival);
                        SD.Add(ArrayOfKeys[numberOfKeys], temp);
                        numberOfKeys++;
                    }
                }
            }
        }

    }
}
