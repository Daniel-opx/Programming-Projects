using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Http;

namespace ASP.NET_learning
{
    public class Todo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsComplete { get; set; }

    }
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = builder.Build();
            //middleware
            app.UseRewriter(new RewriteOptions().AddRedirect("tasks/(.*)", "todos/$1"));
            //midleware
            app.Use(async (context, next) =>
            {
                Console.WriteLine($"[{context.Request.Method} {context.Request.Path} {DateTime.UtcNow}] Started");
                await next(context);
                Console.WriteLine($"[{context.Request.Method} {context.Request.Path} {DateTime.UtcNow} {context.Response.StatusCode}] Finished");
            });

            var toDoList = new List<Todo>();

            app.MapGet("/todos", () => toDoList);

            app.MapGet("/", () => " World!");

            app.MapGet("/todos/{id}", Results<Ok<Todo>, NotFound> (int id) => //Crud - read
            {
                var targetTodo = toDoList.SingleOrDefault(t => id == t.ID);
                return targetTodo == null ? TypedResults.NotFound() : TypedResults.Ok(targetTodo);
            });

            app.MapPost("/todos", (Todo task) => // crud - Create
            {
                toDoList.Add(task);
                return TypedResults.Created($"/todos/{task.ID}", task);
            })
            .AddEndpointFilter(async (context, next) =>
            {
                var taskArgument = context.GetArgument<Todo>(0);
                var errors = new Dictionary<string, string[]>();
                if (taskArgument.DueDate < DateTime.UtcNow)
                    errors.Add(nameof(Todo.DueDate), ["Cannot have due date in the past"]);
                if (taskArgument.IsComplete)
                    errors.Add(nameof(Todo.IsComplete), ["Cannot add complete Todo object"]);

                if (errors.Count > 0) return Results.ValidationProblem(errors);
                return await next(context);
            });
                

            app.MapDelete("/todos/{ID}", (int id) => // CRUD - delete
            {
                toDoList.RemoveAll(t => t.ID == id);
                return TypedResults.NoContent();
            });


            app.MapDelete("/todos", () =>
            {
                toDoList.Clear();
                return TypedResults.NoContent();
            });






            /*the next line include the three elemnts that required to define how a web request 
             should be handled
            1.http method- in our case MapGet. this method indicates that a client is requesting or 
            wanting to get info from a server
            2.url route -  the first argument - "/". whenver a client send info request i execute 
            the method thats described by the handler
            3.handler- deal with incoming reauest that matches the http method and the route
            */
            //app.MapGet("/", () => "Hello World!");



            app.Run();


        }
    }
}
