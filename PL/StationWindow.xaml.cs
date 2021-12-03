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
                Label stationItemT = new Label();
                stationItemT.Content = $"Enter station {item}:";
                stationItemT.VerticalAlignment = VerticalAlignment.Top;
                stationItemT.Margin = new Thickness(43, x, 0, 0);
                StationData.Children.Add(stationItemT);
                TextBox stationItem = new TextBox();
                stationItem.Name = $"station{item}";
                stationItem.VerticalAlignment = VerticalAlignment.Top;
                stationItem.Margin = new Thickness(199, x, 0, 0);
                StationData.Children.Add(stationItem);
                x += 30;
            }

            Button sendNewStation = new Button();
            sendNewStation.Content = "Send";
            sendNewStation.VerticalAlignment = VerticalAlignment.Top;
            sendNewStation.Margin = new Thickness(43, x, 0, 0);
            sendNewStation.Click += AddNewStation;
            StationData.Children.Add(sendNewStation);
        }

        private void AddNewStation(object sender, RoutedEventArgs e)
        {
            //bl.AddStationBL();
        }

        public StationWindow(IBL.IBL blMain, IBL.BO.StationBL station)
        {
            InitializeComponent();
            bl = blMain;
            int x = 70;
            foreach (string item in dataArr)
            {
                Label stationItemT = new Label();
                stationItemT.Content = $"Upadate station {item}:";
                stationItemT.VerticalAlignment = VerticalAlignment.Top;
                stationItemT.Margin = new Thickness(43, x, 0, 0);
                StationData.Children.Add(stationItemT);
                TextBox stationItem = new TextBox();
                stationItem.Name = $"station{item}";
                stationItem.VerticalAlignment = VerticalAlignment.Top;
                stationItem.Margin = new Thickness(199, x, 0, 0);
                switch (item)
                {
                    case "ID":
                        stationItem.Text = station.ID.ToString(); stationItem.IsEnabled = false; break;
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

            Button updateStation = new Button();
            updateStation.Content = "update";
            updateStation.VerticalAlignment = VerticalAlignment.Top;
            updateStation.Click += UpdateCustomer;
            updateStation.Margin = new Thickness(43, x, 0, 0);
            StationData.Children.Add(updateStation);
        }

        private void UpdateCustomer(object sender, RoutedEventArgs e)
        {
            string ID = StationData.Children.OfType<TextBox>().First(txt => txt.Name == "stationID").Text;
            string name = StationData.Children.OfType<TextBox>().First(txt => txt.Name == "stationname").Text;
            bl.UpdateStationData(int.Parse(ID), name);
        }
    }
    
}
