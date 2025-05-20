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
        public List<Guest> GuestList { get; set; } = new List<Guest>();

        public FileGuestRepository(string filePath)
        {
            _filePath = filePath;
            // Sikrer at filen eksisterer
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
            }
        }

        public int GuestIdGen(int guestId)                 //Guest Id Generator Metode
        {
            Random idGen = new Random();
            List<int> usedGuestIds = new List<int>();

            //man kan fx bruge GUID men det er ret langt og meget kompleks => 4 bord x 365 dage = 1460 nr brugte hvert år ved en all-bord booking hver dag
            // var guestId = Guid.NewGuid(); 

            int rangeStart = 1000;
            int rangeStop = 9999;


            if (usedGuestIds.Count >= rangeStop - rangeStart + 1)
            {
                //hvis alle nr mellem 1000 og 9999 er brugte, så vil den ny min og max blive afregnet så den nye min bliver 10 000  og den nye max max blive 99 999
                rangeStart = rangeStop + 1;
                rangeStop = rangeStart * 10 - 1;
            }

            //  do - while loop 
            do
            {
                guestId = idGen.Next(rangeStart, rangeStop + 1);
            }
            while (usedGuestIds.Contains(guestId));



            usedGuestIds.Add(guestId);
            return guestId;
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