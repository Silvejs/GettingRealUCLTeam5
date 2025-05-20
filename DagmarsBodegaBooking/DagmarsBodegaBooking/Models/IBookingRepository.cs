using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagmarsBodegaBooking.Models
{
    interface IBookingRepository
    {
        void CreateBooking(Booking newBooking);

        List<Booking> SaveBooking();        //Hvad er med de parametre som er angivede i DCD?
        List<Booking> GetAllBookings();

        List<Booking> GetBooking(int id);

        List<DateTime> AvailableDates(int numGeusts);

        bool CheckBookingConditions();
        bool CheckPayment();




    }
}
