using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagmarsBodegaBooking.Models
{
    public class FileBookingRepository : IBookingRepository
    {
        /* FileBookingRepository implementerer IBookingRepository 
         * og indeholder metoder til at tilføje, opdatere, slette og hente bookinger fra en fil.
         */
        public void AddBooking(Booking booking)
        {
            throw new NotImplementedException();
        }

        public void DeleteBooking(Booking bookingId)
        {
            throw new NotImplementedException();
        }

        public List<Booking> GetAllBookings()
        {
            throw new NotImplementedException();
        }

        public void UpdateBooking(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}
