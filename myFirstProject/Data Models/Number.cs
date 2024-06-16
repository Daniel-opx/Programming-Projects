using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Models
{
    [Serializable]
    public class Number 
    {
        public int Id { get;private set; }
        public string PhoneNumber { get; private set; }

        public int PersonId { get;private set; }

        public Number( string phoneNumber, int personId,int id = 0)
        {
            Id = id;
            this.PhoneNumber = phoneNumber;
            PersonId = personId;
        }

       
    }
   
}
