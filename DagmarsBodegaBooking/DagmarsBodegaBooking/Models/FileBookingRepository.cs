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
            // Sikrer at filen eksisterer
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
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
       
        public List<DateTime> AvailableDates() // Henter alle bookinger og finder de datoer, der ikke er booket.
        {
            // Henter alle bookinger og finder de datoer, der ikke er booket
            var bookedDates = GetAllBookings()
                .Select(b => b.Date.Date)
                .Distinct()
                .ToList();

            // Genererer en liste over alle datoer i de næste 30 dage
            var allDates = Enumerable.Range(0, 30)
                .Select(offset => DateTime.Today.AddDays(offset).Date)
                .ToList();

            // Returnerer de datoer, der ikke er booket
            return allDates
                .Where(date => !bookedDates.Contains(date))
                .ToList();
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
