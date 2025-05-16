using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DagmarsBodegaBooking.ViewModels.Guest;
namespace DagmarsBodegaBooking.Views.Guest
{
    /// <summary>
    /// Interaction logic for BookTableWindow.xaml
    /// </summary>
    public partial class BookTableWindow : Window
    {
        public BookTableWindow()
        {
            InitializeComponent();
            DataContext = new GuestBookPrivateRoomViewModel();
        }
    }
}
