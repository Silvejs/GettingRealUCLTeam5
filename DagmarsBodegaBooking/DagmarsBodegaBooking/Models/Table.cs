using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagmarsBodegaBooking.Models
{
    internal class Table
    {
        private int _numSeats;
        private string _nameTable;
        public List<Table> tableList { get; set; }

        public int NumSeats
        {
            get { return _numSeats; }
            set { _numSeats = value; }
        }
        public string NameTable
        {
            get { return _nameTable; }
            set { _nameTable = value; }
        }

        public Table(int numSeats, string nameTable)
        {
            _numSeats = numSeats;
            _nameTable = nameTable;
        }

        public override string ToString()
        {
            return $"{NameTable}, {NumSeats}";
        }
        public static Table FromString(string data)
        {
            var parts = data.Split(',');
            if (parts.Length != 2)
            {
                throw new FormatException("Invalid file string format for Table.");
            }


            return new Table(
             int.Parse(parts[1]), // NumSeats
                            parts[0] // NameTable
                            );

        }
    }
}
