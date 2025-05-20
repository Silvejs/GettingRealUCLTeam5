using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DagmarsBodegaBooking.Models
{
    public class Guest
    {
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Mail { get; set; }
        public int GuestId { get; set; }


        public Guest(int guestId, string name, int phoneNumber, string mail)
        {
            GuestId = guestId;
            Name = name;
            PhoneNumber = phoneNumber;
            Mail = mail;
        }

      
      
        
        public override string ToString()           //method override ToString() - Persistens
        {
            return $"{GuestId}-{Name}-{PhoneNumber}-{Mail}";
        }
       
        
        
        public static Guest FromString(string data)     //FromString metode - Persistens
        {
            var parts = data.Split('-');
            if (parts.Length !=4)
            {
                throw new FormatException("Invalid file string format for Table.");
            }


            return new Guest(   
             int.Parse(parts[0]),   // GuestID
             parts[1],              // Name
             int.Parse(parts[2]),   // PhoneNumber
             parts[3]               // Email
             );

        }





    }
}
