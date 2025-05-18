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
        public List<Table> tableList { get; set; }

        public FileTableRepository(string filePath)
        {
            _filePath = filePath;
            // Sikrer at filen eksisterer
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

        public List<Table> FetchAvailableTables(DateTime date, TimeSpan Time, int numGuests, int numSeats) //Ikke færdig endnu!!
        {
            return GetAllTables()
                .Where(t => t.NumSeats >= numGuests)
                .ToList();

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
