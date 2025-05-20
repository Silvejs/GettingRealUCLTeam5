using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagmarsBodegaBooking.Models
{
    public class Booking
    {
        public int NumGuests { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Comment { get; set; }
        public int Id { get; set; }
        public int GuestId { get; set; }

        public Booking(int numGuests, DateTime date, TimeSpan time, string comment, int id, int guestId)
        {
            NumGuests = numGuests;
            Date = date;
            Time = time;
            Comment = comment;
            Id = id;
            GuestId = guestId;
        }

        public override string ToString()
        {
            return $"{NumGuests};{Date.ToShortDateString()};{Time};{Comment};{Id};{GuestId}";
        }

        public static Booking FromString(string data)
        {
            var parts = data.Split(';');
            if (parts.Length != 6)
            {
                throw new ArgumentException("Ugyldigt input");
            }

            int numGuests = int.Parse(parts[0]);
            DateTime date = DateTime.Parse(parts[1]);
            TimeSpan time = TimeSpan.Parse(parts[2]);
            string comment = parts[3];
            int id = int.Parse(parts[4]);
            int guestId = int.Parse(parts[5]);

            return new Booking(numGuests, date, time, comment, id, guestId);
        }
    }
}
    

