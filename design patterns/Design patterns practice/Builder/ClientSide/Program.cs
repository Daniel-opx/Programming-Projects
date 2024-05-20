using BuilderPractice;
using System.ComponentModel.DataAnnotations;
using static BuilderLibrary.Dierctor;


namespace ClientSide
{
    internal static class Program
    {
         static void Main(string[] args)
        {
            var builder = new CarBuilder();
           var car1= builder.OfModel("ferari").ThatDrivedThisKm(100).Build();
            var suzukiAlto = ConstructSuzukiAlto(builder,143);
            Console.WriteLine(suzukiAlto);



        }
    }
}
