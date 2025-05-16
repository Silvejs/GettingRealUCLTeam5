using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DagmarsBodegaBooking.Core;
using System.Windows.Input;
using System.DirectoryServices.ActiveDirectory;

namespace DagmarsBodegaBooking.ViewModels.Guest
{
     public class GuestShellViewModel : ViewModelBase
    {


        
        public ICommand ShowBookTableCommand { get; }
        public ICommand ShowBookPrivateRoomCommand { get; }

        private readonly MainViewModel _main;
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
            _main = main;

            _currentPage = new GuestLandingPageViewModel();
            ShowBookTableCommand = new RelayCommand(_ => OpenBookTablePopup());
            ShowBookPrivateRoomCommand = new RelayCommand(_ => OpenPrivateRoomPopup());
        }

        private void OpenBookTablePopup()
        {
            var window = new Views.Guest.BookTableWindow();
            window.ShowDialog();
        }

        private void OpenPrivateRoomPopup()
        {
            var window = new Views.Guest.BookPrivateRoomWindow();
            window.ShowDialog();
        }
    }

}
