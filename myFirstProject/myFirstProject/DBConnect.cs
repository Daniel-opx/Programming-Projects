using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Org.BouncyCastle.Asn1.X509.SigI;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Data_Models;
using DAL;
using System.Reflection.Metadata.Ecma335;
using Mysqlx.Crud;
using ConditionTree;



namespace myFirstProject
{
    public class DBConnect
    {

        private string server;
        private string database;
        private string uid;
        private string password;
        private string connectionString;

        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "mydb";
            uid = "root";
            password = "password";

            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
        }

        //open connection to database 
        /// <summary>
        /// in charge of openning connection to mysql server
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private MySqlConnection OpenConnection()
        {
            try
            {
                var conn = new MySqlConnection(connectionString);
                conn.Open();
                return conn;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                throw new Exception("couldnt connect to mysql");
            }
        }

       

        //Insert statement
        /// <summary>
        /// 
        /// </summary>
        /// <param name="person">object of type person thst will be inserted to people table</param>
        private void InsertIntoPerson(Person p )
        {
            string query = "insert into person values(null,@first_name,@last_name)";

            using (var connection = OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.Add("@first_name", MySqlDbType.VarChar).Value = p.FirstName;
                cmd.Parameters.Add("@last_name", MySqlDbType.VarChar).Value = p.LastName;

                cmd.ExecuteNonQuery();
            }
        }
        private void InsertIntoPhone(Number n)
        {
            string query = "insert into phone values(null,@Phone_number,@personId)";

            using (var connection = OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.Add("@Phone_number", MySqlDbType.VarChar).Value = n.PhoneNumber;
                cmd.Parameters.Add("@personId", MySqlDbType.Int32).Value = n.PersonId;

                cmd.ExecuteNonQuery();
            }
        }
        public void Insert(Table table,Person p)
        {
            if (table == Table.phone)
                throw new Exception("tavle do not coorespond with objct to insert");
            InsertIntoPerson(p);
        }



        public void Insert(Table table, Number n)
        {
            if (table == Table.person)
                throw new Exception("table do not coorespond with objct to insert");
            InsertIntoPhone(n);
        }


        //Update statement
        private void Update()
        {
            string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

            //Open connection
            using (var connection = OpenConnection())
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();
            }
        }

        //Delete statement
        private void Delete(Table whichTable)
        {
            string query = "";
            switch (whichTable)
            {
                case Table.person:
                    query = "DELETE FROM person";
                    break;
                case Table.phone:
                    query = "DELETE FROM phone_number";
                    break;
                default:
                    break;

            }


            using (var connection = OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.ExecuteNonQuery();
            }

            
        }
        

        /// <summary>
        /// In charge of returning List of objects through select query in mysql db 
        /// </summary>
        /// <typeparam name="T">the type that will be return in list</typeparam>
        /// <param name="table">which table will be queried</param>
        /// <param name="project">the delegate that will be used</param>
        /// <returns>a list of object of one of the types in data models assembly</returns>
        /// 
        //this function uses the same inferance logic as Ienummerable.Select(),
        //it infers the type of t from the delegate return type in the params
        private List<T> Select<T>(Table table,Func<MySqlDataReader, T> project)
        {
            string query = "SELECT * FROM " + table.ToString();

            
            var list = new List<T>();
            
           
            //Open connection
            using (var connection = OpenConnection())
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Create a data reader and Execute the command
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(project(dataReader));
                    }
                }
            }
            return list;
        }
        private List<T> Select<T>(Table table, Func<MySqlDataReader, T> project,WherePredicate wp)
        {
            var visitor = new PredicatePrinter();
            wp.Accept(visitor);
            var whereClause = visitor.ToString();
            Console.WriteLine("the where clause is {0}",whereClause);

            Console.WriteLine(whereClause.Contains("\\"));


            var query1 = String.Format("Select * From {0} Where {1}", table.ToString(), visitor.ToString());
            string query = "SELECT * FROM " + table.ToString() +" WHERE " +visitor.ToString();


            var list = new List<T>();


            //Open connection
            using (var connection = OpenConnection())
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Create a data reader and Execute the command
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(project(dataReader));
                    }
                }
            }
            return list;
        }
        //servers api
        public List<Person> ReadAllPerson() => Select(Table.person, Parsers.GetPersonFromRow);
        public List<Number> ReadAllphones() => Select(Table.phone, Parsers.GetNumberFromRow);
        public List<Person> ReadPersonByFirstName(string value)
        {
            var p = new SinglePerdicate("first_name", "=", value);
            var visitor = new PredicatePrinter();
            return Select(Table.person, Parsers.GetPersonFromRow, p);

        }
        public List<Person> ReadPersonByLastName(string value)
        {
            var p = new SinglePerdicate("last_name", "=", value);
            var visitor = new PredicatePrinter();
            return Select(Table.person, Parsers.GetPersonFromRow, p);

        }


        //Count statement
        public int Count(Table table)
        {
            string query = "SELECT Count(*) FROM " + table.ToString(); 
            int Count = -1;

            //Open Connection
            using (var connection = OpenConnection())
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //cmd.Parameters.Add("@table",MySqlDbType.VarChar).Value = table.ToString();

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                return Count;
            }
        }

        //Backup
        public void Backup()
        {
            try
            {
                DateTime Time = DateTime.Now;
                int year = Time.Year;
                int month = Time.Month;
                int day = Time.Day;
                int hour = Time.Hour;
                int minute = Time.Minute;
                int second = Time.Second;
                int millisecond = Time.Millisecond;

                //Save file to C:\ with the current date as a filename
                string path;
                path = "C:\\MySqlBackup" + year + "-" + month + "-" + day +
            "-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";
                StreamWriter file = new StreamWriter(path);


                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysqldump";
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}",
                    uid, password, server, database);
                psi.UseShellExecute = false;

                Process process = Process.Start(psi);

                string output;
                output = process.StandardOutput.ReadToEnd();
                file.WriteLine(output);
                process.WaitForExit();
                file.Close();
                process.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error , unable to backup!");
            }
        }

        //Restore
        public void Restore()
        {
            try
            {
                //Read file from C:\
                string path;
                path = "C:\\MySqlBackup.sql";
                StreamReader file = new StreamReader(path);
                string input = file.ReadToEnd();
                file.Close();

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysql";
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = false;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}",
                    uid, password, server, database);
                psi.UseShellExecute = false;


                Process process = Process.Start(psi);
                process.StandardInput.WriteLine(input);
                process.StandardInput.Close();
                process.WaitForExit();
                process.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error , unable to Restore!");
            }
        }
        
    }
}