using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagmarsBodegaBooking.Models
{
    internal class FileTableRepository : ITableRepository
    {
        private readonly string _filePath;
        public List<Table> TableList { get; set; } = new List<Table>();

        public FileTableRepository(string filePath)
        {
            
            var projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
            _filePath = Path.Combine(projectRoot!, "Data", "tables.txt");

            
            string? directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
            }
        }

        public void CreateTable(Table newTable)
        {
            List<Table> tables = GetAllTables().ToList();
            try
            {
                File.AppendAllText(_filePath, newTable.ToString() + Environment.NewLine);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }

        public List<Table> FetchAvailableTables(DateTime date, int numGuests) //Der er fejl i denne, da der skal rettes nogle ting i bookingklassen
        {
            var bookingRepository = new FileBookingRepository(_filePath);

            // Henter alle bookinger for den givne dato
            var bookingsForDate = bookingRepository.GetAllBookings()
                .Where(b => b.Date.Date == date.Date)
                .ToList();

            // Henter alle borde
            var allTables = GetAllTables(); 

            // Finder navnene på de borde, der er booket på datoen, med null-tjek og konvertering til string
            var bookedTableNames = bookingsForDate
                .Where(b => b.NameTable != null)
                .Select(b => b.NameTable.ToString()) // Konverterer til string for at undgå typefejl
                .ToList();

            // Returnerer ledige borde, der har plads til numGuests
            return allTables
                .Where(t => t.NameTable != null && !bookedTableNames.Contains(t.NameTable) && t.NumSeats >= numGuests)
                .ToList();

        }

        public Table AssignTable(DateTime date, int numGuests)
        {
            var availableTables = FetchAvailableTables(date, numGuests);
            if (availableTables.Any())
            {
                // Vælger det første ledige bord (eller implementer anden logik, f.eks. mindste kapacitet)
                return availableTables.OrderBy(t => t.NumSeats).First();
            }
            throw new InvalidOperationException("Ingen ledige borde fundet for den valgte dato og antal gæster.");
        }

        public List<Table> GetAllTables()
        {
            try
            {
                return File.ReadAllLines(_filePath)
                           .Where(line => !string.IsNullOrEmpty(line)) // Undgå tomme linjer
                           .Select(Table.FromString)
                           .ToList();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Fejl ved læsning af fil: {ex.Message}");
                return new List<Table>();
            }
        }

        public Table GetTable(string nameTable)
        {
            return GetAllTables().FirstOrDefault(t => t.NameTable == nameTable);
        }
    }
}
