using System.Reflection;
using System.Runtime.InteropServices;

namespace dll_loader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string LiabraryPath = @"C:\Programming Projects\dll loader\ClassLibrary1\bin\Debug\net8.0\ClassLibrary1.dll";

           var asm = Assembly.LoadFrom(LiabraryPath);
            if (asm == null) throw new Exception();
            var t = asm.GetType("ClassLibrary1.FooPrinter");
            if(t == null) throw new Exception();

            var methodInfo = t.GetMethod("Print");

            var instance = Activator.CreateInstance(t);

            var r = methodInfo.Invoke(instance, null);



            


        }
    }
}
