using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DagmarsBodegaBooking.Core;

namespace DagmarsBodegaBooking.ViewModels
{
    public class StartUpViewModel : ViewModelBase
    {
        public ICommand ShowGuestCommand { get; }

        public ICommand ShowEmployeeCommand { get; }

        public StartUpViewModel(MainViewModel main)
        {
            ShowGuestCommand = new RelayCommand(_ => main.NavigateToGuestShell());
            ShowEmployeeCommand = new RelayCommand(_ => main.NavigateToEmployeeShell());
        }
    }
}
