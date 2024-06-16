using Data_Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace Client
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new() { BaseAddress = new Uri("http://localhost:5229") };



        public Form1()
        {
            InitializeComponent();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private async void GetAllPeople_Click(object sender, EventArgs e)
        {
            string? response;
            if(textBox1.Text == null || textBox1.Text == "")
            {
                response = await GetPeopleAsync(client);
            }
            else
            {
                response = await GetPeopleAsync(client,textBox1.Text);
            }
            
            richTextBox1.Text = response;
            Person[] people = JsonSerializer.Deserialize<Person[]>(response);
            if (people != null)
                DataGrid.DataSource = people;

        }




        static async Task<string> GetPeopleAsync(HttpClient httpClient,string firstName = "")
        {
            using HttpResponseMessage response = await httpClient.GetAsync("people?firstname=" + firstName);

            response.EnsureSuccessStatusCode();
            

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse;

            // Expected output:
            //   GET https://jsonplaceholder.typicode.com/todos/3 HTTP/1.1
            //   {
            //     "userId": 1,
            //     "id": 3,
            //     "title": "fugiat veniam minus",
            //     "completed": false
            //   }
        }


    }
}
