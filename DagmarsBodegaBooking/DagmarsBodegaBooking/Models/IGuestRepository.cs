using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagmarsBodegaBooking.Models
{
    public interface IGuestRepository
    {

        List<Guest> GetAllGuests();

        List<Guest> GetGuestByID(int guestId);


    }
}
