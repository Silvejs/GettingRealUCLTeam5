using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private readonly IGuestRepository _guestRepo;

        public ObservableCollection<Booking> Bookings { get; set; }


        private string _guestName;
        public string GuestName
        {
            get => _guestName;
            set { _guestName = value; OnPropertyChanged(); }

        }

        private int _guestPhoneNumber;
        public int GuestPhoneNumber
        {
            get => _guestPhoneNumber;
            set { _guestPhoneNumber = value; OnPropertyChanged(); }
        }

        private string _guestMail;
        public string GuestMail
        {
            get => _guestMail;
            set { _guestMail = value; OnPropertyChanged(); }
        }



        private int _numGuests;
        public int NumGuests
        {
            get => _numGuests;
            set { _numGuests = value; OnPropertyChanged(); UpdateAvailableDates(); }
        }
        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set { _selectedDate = value; OnPropertyChanged(); }
        }


        public List<string> TimeOptions { get; } = new List<string> { "17:00", "18:00", "19:00", "20:00", "21:00" };

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


        private bool _acceptTermsIsChecked;
        public bool AcceptTermsIsChecked
        {
            get => _acceptTermsIsChecked;
            set { _acceptTermsIsChecked = value; OnPropertyChanged(); }
        }

        private string _bookingMessage;
        public string BookingMessage
        {
            get => _bookingMessage;
            set { _bookingMessage = value; OnPropertyChanged(); }
        }

        public List<int> GuestAmountOptions { get; } = new List<int> { 8, 9, 10, 11, 12, 13, 14, 15 };

        public ICommand CreateBookingCommand { get; }



        public GuestBookTableViewModel()
        {
            _bookingRepo = new FileBookingRepository("Data/Booking.txt");
            _tableRepo = new FileTableRepository("Data/Tables.txt");
            _guestRepo = new FileGuestRepository("Data/Guest.txt");

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
            // Fx:
            return NumGuests > 0
                && SelectedDate != default
                && !string.IsNullOrWhiteSpace(GuestName)
                && GuestPhoneNumber > 0
                && !string.IsNullOrWhiteSpace(GuestMail)
                && AcceptTermsIsChecked; 
        }


        public event Action RequestClose; // Event til at lukke vindue

        private void CreateBooking()
        {
            try
            {
                var guest = new Models.Guest(0, GuestName, GuestPhoneNumber, GuestMail);
                int guestId = guest.GuestId;
                var assignedTable = _tableRepo.AssignTable(SelectedDate, NumGuests);

                var booking = new Booking(
                    0, NumGuests, SelectedDate, SelectedTime, Comment, guestId, assignedTable.NameTable
                );
                _bookingRepo.CreateBooking(booking);
                Bookings.Add(booking);
                _guestRepo.CreateGuest(guest);

                BookingMessage = "Booking gennemført! Tak for din reservation ";
                MessageBox.Show(BookingMessage, "Bekræftelse", MessageBoxButton.OK, MessageBoxImage.Information);
                RequestClose?.Invoke(); // <-- Her udløses eventet
            }
            catch (Exception ex)
            {
                BookingMessage = $"Der opstod en fejl: {ex.Message}";
            }
        }
    }
}
