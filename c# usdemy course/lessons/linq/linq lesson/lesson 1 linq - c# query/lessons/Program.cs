using System.Security.Cryptography.X509Certificates;

namespace lessons
{
    class Foo
    {
        public string msg;

        public void print()
        {
            Console.WriteLine(msg);
        }

    }

    internal class Program
    {



        static void Main(string[] args)
        {
            var manager = new UniversityManager();
            //manager.SortStudentsByAge();
            Console.WriteLine(" ");
            manager.StudentAndUniversityNameCollection();




        }



        class UniversityManager
        {
            public List<University> universities;
            public List<Student> students;
            public List<int> universitysIdList;
            /// <summary>
            /// example of join function with linq.
            /// </summary>
            /// <param name="id"></param>
            public void AllStudentsFromThatUniversity(int id)
            {
                if (!universitysIdList.Contains(id))//check if the id exist
                {
                    Console.WriteLine("university id does not exist in data base");
                    return;
                }

                IEnumerable<Student> bjStudents = from student in students // This part introduces a range variable student that represents each element in the students collection. 
                                                  join university in universities //This is a join clause that combines elements from the students and uniList collections based on the specified conditio
                                                  on student.UniversityId equals university.Id//specifies the condition for joining, indicating that the UniversityId property of the student should be equal to the Id property of the university
                                                  where university.Id == id//filtering condition to just one university id
                                                  select student; //This part selects the student variable from the joined result, effectively projecting the entire student object.
                Console.WriteLine("this is the studdents that go to university with id of {0}",id);
                foreach (var stud in bjStudents)
                {
                    stud.PrintStudentData();
                }
            }

            public void AllStudentsFromThatUniversity2(int id)
            {
                // Indicates that it is done on the students list
                IEnumerable<Student> bjStudents = students
                    // First parameter - the second collection that you join the first collection to
                    .Join(
                        universities,
                        // Key selector for the first collection
                        student => student.UniversityId,
                        // Key selector for second collection, it's like the "on student.UniversityId equals university.Id", they must be equal
                        university => university.Id,
                        // Lambda expression that says these are two parameters, return student
                        (student, university) => student
                    )
                    // Additional filtering to include only students associated with a university whose Id matches the specified id
                    .Where(student => universities.Any(university => university.Id == id)); // Additional comment explaining the purpose of the Where clause

            }
            /// <summary>
            /// simple constructor for hard coding some students and universities class type object
            /// </summary>
            public UniversityManager()
            {
                universities = new List<University>();
                students = new List<Student>();
                universitysIdList = new List<int>();

                universities.Add(new University { Id = 1, Name = "Yale" });
                universities.Add(new University { Id = 2, Name = "Beijing Tech" });

                foreach (var item in universities)
                {
                    universitysIdList.Add(item.Id);
                }

                students.Add(new Student { Id = 1, Name = "Carla", Gender = "female", Age = 17, UniversityId = 1 });
                students.Add(new Student { Id = 2, Name = "Toni", Gender = "male", Age = 21, UniversityId = 2 });
                students.Add(new Student { Id = 3, Name = "Leyola", Gender = "female", Age = 19, UniversityId = 1 });
                students.Add(new Student { Id = 4, Name = "James", Gender = "male", Age = 25, UniversityId = 2 });
                students.Add(new Student { Id = 5, Name = "Linda", Gender = "Transgender", Age = 22, UniversityId = 1 });

            }

            /// <summary>
            /// reverse method, we can reverse a list with this simple method
            /// via the extension method reverse. 
            /// reverse method works with every IEnumerable.
            /// </summary>
            /// <param name="list"></param>
            /// <returns></returns>
            public IEnumerable<T> ReversedList<T>(IEnumerable<T> list)//generic method
            {
                return list.Reverse(); 
            }
            /// <summary>
            /// we can reverse IEnumerables by the orderby accending,decending keywords
            /// </summary>
            /// <param name="list"></param>
            /// <returns></returns>
            public IEnumerable<int> reverseWithLinq(IEnumerable<int> list)
            {
                var reversedList = from num in list
                                   orderby num descending // order in decending order i.e reverse
                                   select num;
                return reversedList;
            }

            /// <summary>
            /// simple filter 
            /// </summary>
            public void MaleStudent()
            {
                IEnumerable<Student> sortedStudents = from student in students//indicates the collection
                                                      where student.Gender == "male"//indicate the filter condition
                                                      select student; // select keyword-return a collection with filtered students

                Console.WriteLine("Male Students:");
                foreach (Student student in sortedStudents)
                {
                    student.PrintStudentData();
                }

            }


            /// <summary>
            /// this is a linq with method chaining syntax for certen filter, in this casw females
            /// </summary>
            public void FemaleGetter2()
            {
                var females = students.//indicates the collection
                    Where((Student s) => s.Gender == "female").//indicates filter condition with lambda
                    Select(s => s);//indicates return with lambda

                foreach (Student student in females)
                {
                    Console.WriteLine(student.Name);
                }
            }

            /// <summary>
            /// this is also female filter but with query synatx
            /// </summary>
            public void FemaleStudents()
            {
                IEnumerable<Student> females = from female in students where female.Gender == "female" select female;
                foreach (var female in females)
                {
                    female.PrintStudentData();
                }
            }
            public void WeirdoStudents()
            {
                IEnumerable<Student> weirdos = from female in students where female.Gender != "female" && female.Gender != "male" select female;
                foreach (var weirdo in weirdos)
                {
                    weirdo.PrintStudentData();
                }
            }


            /// <summary>
            /// linq sorting , orderby keyword, in linq query syntax
            /// </summary>
            public void SortStudentsByAge()
            {
                IEnumerable<Student> sortStudents = from student in students // list indication
                                                    orderby student.Age // sort according to the age property
                                                    select student;//return the list
                Console.WriteLine("Sorted student list:");
                foreach (var item in sortStudents)

                {
                    item.PrintStudentData();
                }
            }
            /// <summary>
            /// sorting with method chaining
            /// </summary>
            public void SortStudentsByAge2()
            {
                // Sorting the students list by Age property in ascending order
                var sortedStudents = students.OrderBy(student => student.Age).
                    Select(student =>student);

                // Displaying a message indicating that the student list is sorted
                Console.WriteLine("Sorted student list:");

                // Iterating through each student in the sorted list and printing the data
                foreach (var item in sortedStudents)
                {
                    item.PrintStudentData();
                }
            }

            /// <summary>
            /// method that uses the join key word and return anonymous data type with new fields of type Student (class) 
            /// </summary>
            public void StudentAndUniversityNameCollection()
            {
                var newCollection = from student in students // indicates which collection to act on
                                    join university in universities // which collection to join
                                    on student.UniversityId equals university.Id // on what casis to join them
                                    orderby student.Name // order alphabetically
                                    select new // return new anonymous object student with new fields
                                    {
                                        StudentName = student.Name,
                                        UniversityName = university.Name
                                    };
                Console.WriteLine("new collection:");
                foreach (var student in newCollection)
                {
                    Console.WriteLine("Student :" + student.StudentName + 
                        " from university: " + student.UniversityName);

                }
            }
            public void StudentAndUniversityNameCollection2()
            {
                // Joining students and universities based on matching UniversityId and Id
                var newCollection = students.Join(
                    universities,                         // (1) Second collection to join with the first one
                    student => student.UniversityId,     // (2) Key selector for the first collection (students)
                    university => university.Id,         // (3) Key selector for the second collection (universities)
                                                         // (4) Result selector - creating a new anonymous type with StudentName and UniversityName properties
                    (student, university) => new
                    {
                        StudentName = student.Name,        // (5) Property of the new anonymous type
                        UniversityName = university.Name   // (6) Property of the new anonymous type
                    }
                ).OrderBy(studObject => studObject.StudentName); // after the join method we order by the name.

                // Note: The result is not used or stored in this method, consider returning or using it as needed.
                // If you want to do something with the result, you might want to assign it to a variable or return it.
                // For example: return result; or process the result further.

                foreach (var student in newCollection)
                {
                    Console.WriteLine("Student :" + student.StudentName +
                        " from university: " + student.UniversityName);

                }
            }


        }





        class University
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public void Print()
            {
                Console.WriteLine($"University {this.Name} with Id {this.Id}");
            }
        }
        class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Gender { get; set; }
            public int Age { get; set; }

            public int UniversityId { get; set; }
            public void PrintStudentData()
            {
                Console.WriteLine($"Student {Name} with Id {Id}" +
                    $",gender {Gender} and Age {Age} from university " +
                    $"with the Id {UniversityId}");
            }


        }




    }
}
