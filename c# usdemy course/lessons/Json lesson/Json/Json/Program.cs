using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace Json
{
    


    internal class Program
    {


        /* this is the json that is in this url:https://my-json-server.typicode.com/typicode/demo/posts
         * [
{
    "id": 1,
    "title": "Post 1"
},
{
    "id": 2,
    "title": "Post 2"
},
{
    "id": 3,
    "title": "Post 3"
}
]
         */

        static async Task Main(string[] args)
        {
            string url = "https://my-json-server.typicode.com/typicode/demo/posts";//put the url into string.
            HttpClient client = new HttpClient();//an http client to send to get request
                                                 //https://my-json-server.typicode.com/
                                                 //in this url we can make fake json server

            try
            {
                var httpResponseMassege = await client.GetAsync(url);
                //read the string from the response content
                string jsonResponse = await httpResponseMassege.Content.ReadAsStringAsync();
                //print the jsonresponse
                Console.WriteLine(jsonResponse);

                /* at this stage we can use couple of tools to help us to make from this json 
                 the dat into c#: the process called deserialization
                 1.https://json2csharp.com
                2. https://codebeautify.org/ - tool that make your code mor asthietic- whitespaces
                new line etc...
           */

                //Deserialize the json response into c# array of type Root[]
                var myRoots = JsonConvert.DeserializeObject<Root[]>(jsonResponse);
                //print the objects
                foreach (var root in myRoots)
                {
                     Console.WriteLine($"{root.Id} {root.Title}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //MAKING SURE THAT WE DIXPOSE THE HTTPCLIENT OBJECT THAT WE OPENED
            finally
            {
                client.Dispose();
            }
           






        }
    }
}

