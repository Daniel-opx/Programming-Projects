using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqWithXML
{
    class Program
    {
       
        static void Main(string[] args)
        {

            //We simply apply our Student-Structure to XML. 
            string studentsXML =
                        @"<Students>
                            <Student>
                                <Name>Toni</Name>
                                <Age>21</Age>
                                <University>Yale</University>
                                <Semester>6</Semester>
                            </Student>
                            <Student>
                                <Name>Carla</Name>
                                <Age>17</Age>
                                <University>Yale</University>
                                <Semester>5</Semester>
                            </Student>
                            <Student>
                                <Name>Leyla</Name>
                                <Age>19</Age>
                                <University>Beijing Tech</University>
                                <Semester>7</Semester>
                            </Student>
                            <Student>
                                <Name>Frank</Name>
                                <Age>25</Age>
                                <University>Beijing Tech</University>
                                <Semester>1</Semester>
                            </Student>
                            <Student>
                                <Name>Yossi</Name>
                                <Age>18</Age>
                                <University>Ben-Gurion</University>
                                <Semester>2</Semester>
                           </Student>

                        </Students>";

            XDocument studntsXdoc = new XDocument();
            studntsXdoc = XDocument.Parse(studentsXML);


            //parsing info from xml , using Descendants method , and string as parameter,
            var students = from student in studntsXdoc.Descendants("Student")
                           select new // we use the select new to create anonimous data type
                           {
                               Name = student.Element("Name").Value /* after getting each block from
                                                                     * within this block we use
                                                                     * .Element method() with string as parameter
                                                                     * an then .Value to get the value in the block*/
                                                                     
                               ,
                               Age = student.Element("Age").Value,
                               University = student.Element("University").Value

                           };

        foreach ( var student in students )
            {
                Console.WriteLine("Student {0} with age {1} from uni {2}",
                    student.Name,student.Age,student.University);

            }
            Console.ReadKey();

            
            var students2 = studntsXdoc.Descendants("Students").
                Select(student => new
                {
                    Name = student.Element("Name").Value
                               ,
                    Age = student.Element("Age").Value,
                    University = student.Element("University").Value

                });
            Console.WriteLine("now printing the " + "students2 list" +"\n==============================================");
            foreach (var student in students)
            {
                Console.WriteLine("Student {0} with age {1} from uni {2}",
                    student.Name, student.Age, student.University);

            }
            Console.ReadKey();

            var sortedStudents = from stud in students
                                orderby stud.Age
                                select stud;

            Console.WriteLine("now printing sorted by age\n" +
                "====================================");
            foreach ( var student in sortedStudents )
            {
                Console.WriteLine("Student {0} with age {1} from uni {2}",
                    student.Name, student.Age, student.University);
            } 
            Console.ReadKey();

            var sordtedStudents2 = students.OrderBy(student => student.Age);
            Console.WriteLine("now printing sorted by age2\n" +
                "====================================");
            foreach (var student in sordtedStudents2)
            {
                Console.WriteLine("Student {0} with age {1} from uni {2}",
                    student.Name, student.Age, student.University);
            }
            Console.ReadKey();

            var studentsWWithSemester = from student in studntsXdoc.Descendants("Student")
                           select new
                           {
                               Name = student.Element("Name").Value
                               ,
                               Age = student.Element("Age").Value,
                               University = student.Element("University").Value
                               ,Semester = student.Element("Semester").Value

                           };
            Console.WriteLine("=====================================\nnow printing studentswithsemester\n" +
           "====================================");
            foreach (var student in studentsWWithSemester)
            {
                Console.WriteLine("Student {0} with age {1} from uni {2}, at semster {3}",
                    student.Name, student.Age, student.University,student.Semester);
            }
            Console.ReadKey();
        }
    }
}
