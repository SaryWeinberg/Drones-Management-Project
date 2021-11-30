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

namespace PL
{
    /// <summary>
    /// Interaction logic for StationListWindow.xaml
    /// </summary>
    public partial class StationListWindow : Window
    {        
        IBL.IBL bl;

        public StationListWindow(IBL.IBL blMain)
        {
            InitializeComponent();
            bl = blMain;
            StationListView.ItemsSource = bl.GetStationsBL();
        }

        private void ViewStationWindow(object sender, RoutedEventArgs e)
        {
            new StationWindow(bl).Show();
        }
    }
}
