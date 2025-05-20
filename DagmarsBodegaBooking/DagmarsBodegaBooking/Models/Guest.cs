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

        public int GuestIdGen()                 //Guest Id Generator Metode
        {
            Random idGen = new Random();            
            List<int> usedGuestIds =new List<int>();

            //man kan fx bruge GUID men det er ret langt og meget kompleks => 4 bord x 365 dage = 1460 nr brugte hvert år ved en all-bord booking hver dag
            // var guestId = Guid.NewGuid(); 

            int rangeStart = 1000;
            int rangeStop = 9999;


            if (usedGuestIds.Count >= rangeStop - rangeStart + 1)
            {
                //hvis alle nr mellem 1000 og 9999 er brugte, så vil den ny min og max blive afregnet så den nye min bliver 10 000  og den nye max max blive 99 999
                rangeStart = rangeStop + 1;
                rangeStop = rangeStart * 10 - 1;
            }

                                        //  do - while loop 
            do
            {
                GuestId = idGen.Next(rangeStart, rangeStop+1);
            }
            while (usedGuestIds.Contains(GuestId));



            usedGuestIds.Add(GuestId);
            return GuestId;
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
