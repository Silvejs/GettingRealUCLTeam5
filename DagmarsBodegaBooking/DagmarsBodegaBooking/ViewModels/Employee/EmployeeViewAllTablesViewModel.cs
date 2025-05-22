using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Input;
using DagmarsBodegaBooking.Core;
using DagmarsBodegaBooking.Models;

namespace DagmarsBodegaBooking.ViewModels.Employee
{
    public class EmployeeViewAllTablesViewModel : ViewModelBase
    {
        private readonly ITableRepository _tableRepo;

        public ObservableCollection<Table> Tables { get; set; }

        private string _nameTable;
        public string NameTable
        {
            get => _nameTable;
            set { _nameTable = value; OnPropertyChanged(); }
        }

        private int _numSeats;
        public int NumSeats
        {
            get => _numSeats;
            set { _numSeats = value; OnPropertyChanged(); }
        }

        public ICommand CreateTableCommand { get; }

        public EmployeeViewAllTablesViewModel()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Tables.txt");
            _tableRepo = new FileTableRepository(filePath);

            Tables = new ObservableCollection<Table>(_tableRepo.GetAllTables());
            CreateTableCommand = new RelayCommand(_ => CreateTable(), _ => CanCreateTable());
        }

        private bool CanCreateTable()
        {
            return !string.IsNullOrWhiteSpace(NameTable) && NumSeats > 0;
        }

        private void CreateTable()
        {
            var newTable = new Table(NumSeats, NameTable);

            _tableRepo.CreateTable(newTable);
            Tables.Add(newTable);

            NameTable = string.Empty;
            NumSeats = 0;
        }
    }
}
