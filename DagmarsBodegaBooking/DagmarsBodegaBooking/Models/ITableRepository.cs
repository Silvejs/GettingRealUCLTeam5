using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagmarsBodegaBooking.Models
{
    public interface ITableRepository
    {
        List<Table> GetAllTables();
        Table GetTable(string nameTable);
        void CreateTable(Table newTable);
        List<Table> FetchAvailableTables(DateTime date, int numGuests);
        Table AssignTable(DateTime date, int numGuests);
     
    }
}
