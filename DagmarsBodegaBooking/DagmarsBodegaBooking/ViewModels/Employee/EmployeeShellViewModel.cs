using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DagmarsBodegaBooking.Core;

namespace DagmarsBodegaBooking.ViewModels.Employee
{
    public class EmployeeShellViewModel : ViewModelBase
    {
        public ICommand ShowDashboardCommand { get; }
        public ICommand ShowViewTablesCommand { get; }
        public ICommand ShowReservationsCommand { get; }
        private object _currentPage;
        public object CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        public EmployeeShellViewModel(MainViewModel main)
        {
            ShowDashboardCommand = new RelayCommand(_ => CurrentPage = new EmployeeDashboardViewModel());
            
            ShowViewTablesCommand = new RelayCommand(_ => CurrentPage = new EmployeeViewAllTablesViewModel());

            ShowReservationsCommand = new RelayCommand(_ => CurrentPage = new EmployeeReservationsViewModel());

            CurrentPage = new EmployeeDashboardViewModel(); // default view
        }
    }
}
