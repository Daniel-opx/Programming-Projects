namespace parsing_game


{
    using System.IO;
    internal class Program
    {
        static void Main(string[] args)
        {
            int numofsente = 0;
            string buffer;
            using (StreamReader stream = new StreamReader(@"C:\Programming Projects\c# usdemy course\parsing game\input.txt"))
            {
                
                while( ( buffer = stream.ReadLine()) != null)
                {
                    if(buffer.Contains("split"))
                    {
                        string[] array=buffer.Split(' ', StringSplitOptions.None);

                        File.AppendAllText(@"C:\Programming Projects\c# usdemy course\parsing game\outputdraft1.txt", array[4] + " ");
                        numofsente++;
                    }
                }
            }
            
        }
    }
}
