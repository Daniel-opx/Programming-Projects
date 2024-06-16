using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Data_Models
{
    [Serializable]
    public class Person 
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("first_name")]
        public string? FirstName { get; private set; }
        [JsonPropertyName("last_name")]
        public string? LastName { get; private set; }

        public Person(string firstName, string lastName,int id = 0)
        {
            Id = id;
            this.FirstName = firstName
                ?? throw new Exception($"{nameof(firstName)}cant be null ");
            this.LastName = lastName
                ?? throw new Exception($"{nameof(lastName)}cant be null ");
        }

        
        
    }

    
}
