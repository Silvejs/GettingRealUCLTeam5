using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DagmarsBodegaBooking.Core;
using System.Windows.Input;

namespace DagmarsBodegaBooking.ViewModels.Guest
{
     public class GuestShellViewModel : ViewModelBase
    {


        public ICommand ShowLandingPageCommand { get; }
        public ICommand ShowBookTableCommand { get; }
        public ICommand ShowBookPrivateRoomCommand { get; }


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

        public GuestShellViewModel(MainViewModel main)
        {
            ShowLandingPageCommand = new RelayCommand(_ => CurrentPage = new GuestLandingPageViewModel());

            CurrentPage = new GuestLandingPageViewModel(); // default view
        }
    }

}
