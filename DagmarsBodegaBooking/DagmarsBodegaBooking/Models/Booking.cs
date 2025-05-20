using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;
using System.Xml;

namespace DagmarsBodegaBooking.Models
{
    public class Booking
    {

        private int _numGuests;
        private DateTime _date;
        private DateTime _time;
        private string _comment;
        private int _bookingId;
        private int _guestId;
        private string _nameTable;


        public int NumGuests { get { return _numGuests; } set { _numGuests = value; } }
        public DateTime Date { get { return _date; } set { _date = value; } }
        public DateTime Time { get { return _time; } set { _time = value; } }
        public string Comment { get { return _comment; } set { _comment = value; } }
        public int BookingId { get { return _bookingId; } set { _bookingId = BookingIdGen(); } }
        public int GuestId { get { return _guestId; } set { _guestId = guest.GuestId; } }
        public string NameTable { get { return _nameTable; } set { _nameTable = value; } }

        public Guest guest; //initialiser et objekt som en property for at bruge GuestId fra Guest i Booking konstruktør
        



        public Booking(int bookingId, int numGuests, DateTime date, DateTime time, string comment, int guestId, string nameTable)
        {
            BookingId = bookingId;
            NumGuests = numGuests;
            Date = date;
            Time = time;
            Comment = comment;
            GuestId = guestId;
            NameTable = nameTable;
            
        }

        public override string ToString()
        {
            return $"{BookingId};{NumGuests};{Date.ToShortDateString()};{Time};{Comment};{GuestId};{NameTable}";
        }

        public static Booking FromString(string data)
        {
            var parts = data.Split(';');
            if (parts.Length != 7)
            {
                throw new ArgumentException("Ugyldigt input");
            }
            int bookingId = int.Parse(parts[0]);
            int numGuests = int.Parse(parts[1]);
            DateTime date = DateTime.Parse(parts[2]);
            DateTime time = DateTime.Parse(parts[3]);
            string comment = parts[4];
            int guestId = int.Parse(parts[5]);
            string nameTable = parts[6];

            return new Booking(bookingId, numGuests, date, time, comment, guestId, nameTable);
        }



        public int BookingIdGen()                 //Guest Id Generator Metode
        {
            
            int bookingId;
            Random idGen = new Random();
            List<int> usedBookingIds = new List<int>();
            
            //man kan fx bruge GUID men det er ret langt og meget kompleks => 4 bord x 365 dage = 1460 nr brugte hvert år ved en all-bord booking hver dag
            // var guestId = Guid.NewGuid(); 

            int rangeStart = 10000;
            int rangeStop = 99999;


            if (usedBookingIds.Count >= rangeStop - rangeStart + 1)
            {
                //hvis alle nr mellem 1000 og 9999 er brugte, så vil den ny min og max blive afregnet så den nye min bliver 10 000  og den nye max max blive 99 999
                rangeStart = rangeStop + 1;
                rangeStop = rangeStart * 10 - 1;
            }

            //  do - while loop => den tildele en tilfældig nummer mellem 10000 og 99999 imens den tjekker for duplikater i usedbookingids 
            do
            {
                bookingId = idGen.Next(rangeStart, rangeStop + 1);
            }
            while (usedBookingIds.Contains(bookingId));



            usedBookingIds.Add(bookingId);
            return bookingId;
        }
    }
}
    

