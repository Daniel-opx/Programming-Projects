using Data_Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal static class  Parsers
    {
        internal static Person GetPersonFromRow(MySqlDataReader reader)
           => new Person(reader.GetString("first_name"), reader.GetString("last_name"), reader.GetInt32("id"));
        internal static Number GetNumberFromRow(MySqlDataReader reader)
          => new Number( reader.GetString("phone_number"), reader.GetInt32("person_id"), reader.GetInt32("id"));
    }
}
