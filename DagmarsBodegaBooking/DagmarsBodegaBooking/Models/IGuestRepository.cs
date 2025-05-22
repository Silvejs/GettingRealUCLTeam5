using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagmarsBodegaBooking.Models
{
    public interface IGuestRepository
    {

        void CreateGuest(Guest guest);
        List<Guest> GetAllGuests();

        List<Guest> GetGuestByID(int guestId);


    }
}
