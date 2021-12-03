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
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        IBL.IBL bl;

        public DroneWindow(IBL.IBL blMain)
        {
            InitializeComponent();
            bl = blMain;
            string[] dataArr = { "ID", "max_weight", "model", "station_ID" };

            int x = 70;
            foreach (string item in dataArr)
            {
                Label droneItemT = new Label();
                droneItemT.Content = $"Enter drone {item}:";
                droneItemT.VerticalAlignment = VerticalAlignment.Top;
                droneItemT.Margin = new Thickness(43, x, 0, 0);
                DroneData.Children.Add(droneItemT);
                if (item == "max_weight")
                {
                    ComboBox droneItemC = new ComboBox();
                    droneItemC.Name = $"drone{item}";
                    droneItemC.VerticalAlignment = VerticalAlignment.Top;
                    droneItemC.Margin = new Thickness(199, x, 0, 0);
                    droneItemC.ItemsSource = Enum.GetValues(typeof(WeightCategories));
                    DroneData.Children.Add(droneItemC);
                }
                else
                {
                    TextBox droneItem = new TextBox();
                    droneItem.Name = $"drone{item}";
                    droneItem.VerticalAlignment = VerticalAlignment.Top;
                    droneItem.Margin = new Thickness(199, x, 0, 0);
                    DroneData.Children.Add(droneItem);
                }
                x += 30;
            }

            Button sendNewDrone = new Button();
            sendNewDrone.Content = "Send";
            sendNewDrone.VerticalAlignment = VerticalAlignment.Top;
            sendNewDrone.Margin = new Thickness(43, x, 0, 0);
            sendNewDrone.Click += AddNewDrone;
            DroneData.Children.Add(sendNewDrone);
        }

        private void AddNewDrone(object sender, RoutedEventArgs e)
        {
            string massage;
            string ID = DroneData.Children.OfType<TextBox>().First(txt => txt.Name == "droneID").Text;
            int weight = DroneData.Children.OfType<ComboBox>().First(txt => txt.Name == "dronemax_weight").SelectedIndex;
            string model = DroneData.Children.OfType<TextBox>().First(txt => txt.Name == "dronemodel").Text;
            string stationID = DroneData.Children.OfType<TextBox>().First(txt => txt.Name == "dronestation_ID").Text;
            try
            {
                massage = bl.AddDroneBL(int.Parse(ID), model, (WeightCategories)weight, int.Parse(stationID));
                MessageBox.Show(massage);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public DroneWindow(IBL.IBL blMain, IBL.BO.DroneBL drone)
        {
            InitializeComponent();
            bl = blMain;

            string[] dataArr = { "ID", "max_weight", "model", "battery_status", "status", "longitude", "latitude" };
            int x = 70;
            foreach (string item in dataArr)
            {
                Label droneItemT = new Label();
                droneItemT.Content = $"Upadate station {item}:";
                droneItemT.VerticalAlignment = VerticalAlignment.Top;
                droneItemT.Margin = new Thickness(43, x, 0, 0);
                DroneData.Children.Add(droneItemT);
                if (item == "max_weight" || item == "status")
                {
                    ComboBox droneItemC = new ComboBox();
                    droneItemC.Name = $"drone{item}";
                    droneItemC.VerticalAlignment = VerticalAlignment.Top;
                    droneItemC.Margin = new Thickness(199, x, 0, 0);
                    switch (item)
                    {
                        case "max_weight":
                            droneItemC.ItemsSource = Enum.GetValues(typeof(WeightCategories));
                            droneItemC.Text = drone.MaxWeight.ToString();
                            droneItemC.IsEnabled = false;
                            droneItemC.IsEditable = true; break;
                        case "status":
                            droneItemC.ItemsSource = Enum.GetValues(typeof(Status)); 
                            droneItemC.Text = drone.Status.ToString();
                            droneItemC.IsEnabled = false;
                            droneItemC.IsEditable = true; break;
                    }
                    DroneData.Children.Add(droneItemC);
                }
                else
                {
                    TextBox droneItem = new TextBox();
                    droneItem.Name = $"station{item}";
                    droneItem.VerticalAlignment = VerticalAlignment.Top;
                    droneItem.Margin = new Thickness(199, x, 0, 0);
                    switch (item)
                    {
                        case "ID":
                            droneItem.Text = drone.ID.ToString(); droneItem.IsEnabled = false; break;
                        case "model":
                            droneItem.Text = drone.Model.ToString(); break;
                        case "battery_status":
                            droneItem.Text = drone.BatteryStatus.ToString(); droneItem.IsEnabled = false; break;
                        case "longitude":
                            droneItem.Text = drone.Location.Longitude.ToString(); droneItem.IsEnabled = false; break;
                        case "latitude":
                            droneItem.Text = drone.Location.Latitude.ToString(); droneItem.IsEnabled = false; break;
                    }
                    DroneData.Children.Add(droneItem);
                }
                x += 30;
            }

            Button update = new Button();
            update.Content = "update";
            update.VerticalAlignment = VerticalAlignment.Top;
            update.Margin = new Thickness(43, x, 0, 0);
            DroneData.Children.Add(update);

            Button sendDroneCharge = new Button();
            sendDroneCharge.Content = "send drone to charge";
            sendDroneCharge.VerticalAlignment = VerticalAlignment.Top;
            sendDroneCharge.Margin = new Thickness(43, x + 30, 0, 0);
            DroneData.Children.Add(sendDroneCharge);

            Button releaseDrone = new Button();
            releaseDrone.Content = "release Drone from Charge";
            releaseDrone.VerticalAlignment = VerticalAlignment.Top;
            releaseDrone.Margin = new Thickness(43, x + 60, 0, 0);
            DroneData.Children.Add(releaseDrone);
        }
    }
}
