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
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        IBL.IBL bl;
        string[] dataArr = { "ID", "name", "longitude", "latitude", "charge_slots" };

        public StationWindow(IBL.IBL blMain)
        {
            InitializeComponent();
            bl = blMain;
            int x = 70;
            foreach (string item in dataArr)
            {
                TextBlock stationItemT = new TextBlock();
                stationItemT.Text = $"Enter station {item}:";
                stationItemT.TextWrapping = TextWrapping.Wrap;
                stationItemT.VerticalAlignment = VerticalAlignment.Top;
                stationItemT.Margin = new Thickness(43, x, 0, 0);
                StationData.Children.Add(stationItemT);
                TextBox stationItem = new TextBox();
                stationItem.Name = $"station{item}";
                stationItem.TextWrapping = TextWrapping.Wrap;
                stationItem.VerticalAlignment = VerticalAlignment.Top;
                stationItem.Margin = new Thickness(199, x, 0, 0);
                StationData.Children.Add(stationItem);
                x += 30;
            }
        }

        public StationWindow(IBL.IBL blMain, IBL.BO.StationBL station)
        {
            InitializeComponent();
            bl = blMain;
            int x = 70;
            foreach (string item in dataArr)
            {
                TextBlock stationItemT = new TextBlock();
                stationItemT.Text = $"Upadate station {item}:";
                stationItemT.TextWrapping = TextWrapping.Wrap;
                stationItemT.VerticalAlignment = VerticalAlignment.Top;
                stationItemT.Margin = new Thickness(43, x, 0, 0);
                StationData.Children.Add(stationItemT);
                TextBox stationItem = new TextBox();
                stationItem.Name = $"station{item}";
                stationItem.TextWrapping = TextWrapping.Wrap;
                stationItem.VerticalAlignment = VerticalAlignment.Top;
                stationItem.Margin = new Thickness(199, x, 0, 0);
                switch (item)
                {
                    case "ID":
                        stationItem.Text = station.ID.ToString() ; break;
                    case "name":
                        stationItem.Text = station.Name.ToString(); break;
                    case "longitude":
                        stationItem.Text = station.Location.Longitude.ToString(); stationItem.IsEnabled = false; break;
                    case "latitude":
                        stationItem.Text = station.Location.Latitude.ToString(); stationItem.IsEnabled = false; break;
                    case "charge_slots":
                        stationItem.Text = station.AveChargeSlots.ToString(); break;
                }
                StationData.Children.Add(stationItem);
                x += 30;
            }
        }
    }
}
