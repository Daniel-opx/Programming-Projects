using ConditionTree;
using Microsoft.AspNetCore.Mvc;
using myFirstProject;

namespace Server_Layer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var db = new DBConnect();

            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();
            app.MapGet("", () => "db.ReadAllPerson()");
            app.MapGet("/phones", () => db.ReadAllphones());

            app.MapGet("/people", (string FirstName) =>
            {
                if (FirstName == null || FirstName == "") return db.ReadAllPerson();
                else
                {
                    var p = new SinglePerdicate("first_name", "=", FirstName);
                    return db.ReadPersonByFirstName(FirstName);
                }
            });
            
               


            //complete later -  use parameter binding and http request parameters
            //app.MapGet("/people", (string firstName) => db.ReadPersonByFirstName(firstName));


            app.Run();
        }
    }
}
