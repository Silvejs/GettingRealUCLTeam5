using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DagmarsBodegaBooking.Core;
using DagmarsBodegaBooking.ViewModels.Guest;
using DagmarsBodegaBooking.ViewModels.Employee;

namespace DagmarsBodegaBooking.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private object _currentView;

            public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            CurrentView = new StartUpViewModel(this);
        }

        public void NavigateToGuestShell()
        {
            CurrentView = new GuestShellViewModel(this);
        }

        public void NavigateToEmployeeShell()
        {
            CurrentView = new EmployeeShellViewModel(this);
        }

    }
}
