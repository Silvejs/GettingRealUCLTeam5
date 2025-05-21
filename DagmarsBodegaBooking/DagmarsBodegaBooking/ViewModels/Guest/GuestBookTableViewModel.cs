using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using DagmarsBodegaBooking.Core;
using DagmarsBodegaBooking.Models;

namespace DagmarsBodegaBooking.ViewModels.Guest
{
    public class GuestBookTableViewModel : ViewModelBase
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly ITableRepository _tableRepo;

        public ObservableCollection<Booking> Bookings { get; set; }


        private int _numGuests;
        public int NumGuests
        {
            get => _numGuests;
            set { _numGuests = value; OnPropertyChanged(); UpdateAvailableDates(); }
        }
        private DateTime _selectedDate;
        public DateTime SelectedDate 
        {
            get => _selectedDate;
            set { _selectedDate = value; OnPropertyChanged(); }
        
        }

        private DateTime _selectedTime;
        public DateTime SelectedTime 
        {
            get => _selectedTime;
            set { _selectedTime = value; OnPropertyChanged(); }
        }

        private string _comment;
        public string Comment 
        {
            get => _comment;
            set { _comment = value; OnPropertyChanged(); }
        }

        public int _guestId;
        public int GuestId 
        {
            get => _guestId;
            set { _guestId = value; OnPropertyChanged(); }
        }


        private ObservableCollection<DateTime> _availableDates;
        public ObservableCollection<DateTime> AvailableDates
        {
            get => _availableDates;
            set { _availableDates = value; OnPropertyChanged(); }
        }

        public ICommand CreateBookingCommand { get; }

        public GuestBookTableViewModel()
        {
            _bookingRepo = new FileBookingRepository("Data/booking.txt");
            _tableRepo = new FileTableRepository("Data/Tables.txt");

            Bookings = new ObservableCollection<Booking>(_bookingRepo.GetAllBookings());
            AvailableDates = new ObservableCollection<DateTime>();

            CreateBookingCommand = new RelayCommand(_ => CreateBooking(), _ => CanCreateBooking());

        }
        
        private void UpdateAvailableDates()
        {
            AvailableDates.Clear();
            var dates = _bookingRepo.AvailableDates(NumGuests);
            foreach (var date in dates)
                AvailableDates.Add(date);
        }

        private bool CanCreateBooking()
        {
            return NumGuests > 0 && SelectedDate != default && SelectedTime != default && GuestId > 0;
        }

        private void CreateBooking()
        {
            var assignedTable = _tableRepo.AssignTable(SelectedDate, NumGuests);

            var booking = new Booking(
                0,
                NumGuests,
                SelectedDate,
                SelectedTime,
                Comment,
                GuestId,
                assignedTable.NameTable
                );
            _bookingRepo.CreateBooking(booking);
            Bookings.Add(booking);

        }
    }
}
