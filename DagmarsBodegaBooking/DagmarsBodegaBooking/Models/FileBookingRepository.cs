using System;
using System.Collections.Generic;
using System.IO;
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

        private readonly string _filePath; // Stien til filen, hvor bookingerne gemmes.


        public FileBookingRepository(string filePath) // Konstruktør, der tager stien til filen som parameter.
        {
            _filePath = filePath;
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
            }
        }

        public void CreateBooking(Booking newBooking) // Opretter en ny booking og tilføjer den til filen.
        {
            // Tjek om bordet allerede er booket på den valgte dato
            bool isTableBooked = GetAllBookings()
                .Any(b => b.NameTable == newBooking.NameTable && b.Date.Date == newBooking.Date.Date);

            if (isTableBooked)
            {
                throw new InvalidOperationException("Bordet er allerede booket på den valgte dato.");
            }

            if (!CheckBookingConditions() || !CheckPayment())
            {
                throw new InvalidOperationException("Bookingbetingelserne er ikke opfyldt.");
            }

            // Tilføjer den nye booking til filen
            {
                File.AppendAllText(_filePath, newBooking.ToString() + Environment.NewLine);
            }
        }

        public List<Booking> GetAllBookings() // Henter alle bookinger fra filen og konverterer dem til Booking-objekter.
        {
            // Læser alle bookinger fra filen og konverterer dem til Booking-objekter
            return File.ReadAllLines(_filePath)  
                       .Where(line => !string.IsNullOrWhiteSpace(line))
                       .Select(Booking.FromString)
                       .ToList();
        }

        public List<Booking> GetBooking(int id)  // Henter en booking med det angivne id fra filen.
        {
            // Henter en booking med det angivne id fra filen
            return GetAllBookings().Where(b => b.Id == id).ToList();
        }

        public void UpdateBooking(Booking booking)  // Opdaterer en booking i filen.
        {
            // Opdaterer en booking i filen
            var bookings = GetAllBookings();
            var index = bookings.FindIndex(b => b.Id == booking.Id);
            if (index >= 0)
            {
                bookings[index] = booking;
                File.WriteAllLines(_filePath, bookings.Select(b => b.ToString()));
            }
        }
       
        public void DeleteBooking(Booking booking) // Sletter en booking fra filen.
        {
            // Sletter en booking fra filen
            var bookings = GetAllBookings();
            bookings.RemoveAll(b => b.Id == booking.Id);
            File.WriteAllLines(_filePath, bookings.Select(b => b.ToString()));
        }

        public List<Booking> SaveBooking() // Gemmer bookingerne til filen.
        {
            // Gemmer bookingerne til filen
            var bookings = GetAllBookings();
            File.WriteAllLines(_filePath, bookings.Select(b => b.ToString()));
            return bookings;
        }
       
        public List<DateTime> AvailableDates(int numGuests) // Henter alle bookinger og finder de datoer, der ikke er booket.
        {
            // Henter alle bookinger og finder de datoer, der ikke er booket
            var bookedDates = GetAllBookings()
                .Select(b => b.Date.Date)
                .Distinct()
                .ToList();

            FileTableRepository tableRepository = new FileTableRepository(_filePath);

            var allTables = tableRepository.GetAllTables();


            // Genererer en liste over alle datoer i de næste 30 dage undtagen mandag og søndag
            var allDates = Enumerable.Range(0, 30)
                .Select(offset => DateTime.Today.AddDays(offset).Date)
                .Where(date => date.DayOfWeek != DayOfWeek.Monday && date.DayOfWeek != DayOfWeek.Sunday)
                .ToList();

            var availableDates = allDates
                .Where(date => !bookedDates.Contains(date) || tableRepository.FetchAvailableTables(date, numGuests).Any())
                .ToList();
            return availableDates;
        }

        

        public bool CheckBookingConditions() // Tjekker bookingbetingelserne.
        {
            return true;
        }

        public bool CheckPayment() // Tjekker betalingsbetingelserne.
        {
            return true;
        }
    }
}
