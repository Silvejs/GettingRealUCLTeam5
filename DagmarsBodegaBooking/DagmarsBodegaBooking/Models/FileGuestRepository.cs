using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DagmarsBodegaBooking.Models
{
    public class FileGuestRepository : IGuestRepository
    {
        private readonly string _filePath;
        
        public void CreateGuest(Guest guest)
        {
            File.AppendAllText(_filePath, guest.ToString() + Environment.NewLine);
        }

        public FileGuestRepository(string filePath)
        {
            var projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
            _filePath = Path.Combine(projectRoot!, "Data", "Guest.txt");

            
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

       


        public List<Guest> GetGuestByID(int guestId)
        {

            // Henter en booking med det angivne id fra filen
            return GetAllGuests().Where(b => b.GuestId == guestId).ToList();

        }




        public List<Guest> GetAllGuests()
        {
       
            try
            {
                return File.ReadAllLines(_filePath)
                           .Where(line => !string.IsNullOrEmpty(line))  // Undgå tomme linjer
                           .Select(Guest.FromString)
                           .ToList();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Fejl ved læsning af fil: {ex.Message}");
                return new List<Guest>();
            }

        }

    }






}