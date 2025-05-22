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

        private string _name;
        private int _phoneNumber;
        private string _mail;
        private int _guestId;




        public string Name { get { return _name; } set { _name = value; } }
        public int PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; } }
        public string Mail { get { return _mail; } set { _mail = value; } }
        public int GuestId { get { return _guestId; } }


        public Guest(int guestId, string name, int phoneNumber, string mail)
        {
            if (guestId == 0)
                _guestId = GuestIdGen();
            else
                _guestId = guestId;

            Name = name;
            PhoneNumber = phoneNumber;
            Mail = mail;
        }

      
      
        
        public override string ToString()           //method override ToString() - Persistens
        {
            return $"{_guestId}-{_name}-{_phoneNumber}-{_mail}";
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


        public int GuestIdGen()                 //Guest Id Generator Metode
        {
            int guestId;
            Random idGen = new Random();
            List<int> usedGuestIds = new List<int>();

            //man kan fx bruge GUID men det er ret langt og meget kompleks => 4 bord x 365 dage = 1460 nr brugte hvert år ved en all-bord booking hver dag
            // var guestId = Guid.NewGuid(); 

            int rangeStart = 1000;
            int rangeStop = 9999;


            if (usedGuestIds.Count >= rangeStop - rangeStart + 1)
            {
                //hvis alle nr mellem 1000 og 9999 er brugte, så vil den ny min og max blive afregnet så den nye min bliver 10 000  og den nye max max blive 99 999
                rangeStart = rangeStop + 1;
                rangeStop = rangeStart * 10 - 1;
                //
            }

            //  do - while loop 
            do
            {
                guestId = idGen.Next(rangeStart, rangeStop + 1);
            }
            while (usedGuestIds.Contains(guestId));



            usedGuestIds.Add(guestId);
            return guestId;
        }


    }
}
