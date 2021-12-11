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
                TextBlock droneItemT = new TextBlock();
                droneItemT.Text = $"Enter parcel {item}:";
                droneItemT.TextWrapping = TextWrapping.Wrap;
                droneItemT.VerticalAlignment = VerticalAlignment.Top;
                droneItemT.Margin = new Thickness(43, x, 0, 0);
                DroneData.Children.Add(droneItemT);
                if (item == "weight")
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
                    droneItem.TextWrapping = TextWrapping.Wrap;
                    droneItem.VerticalAlignment = VerticalAlignment.Top;
                    droneItem.Margin = new Thickness(199, x, 0, 0);
                    DroneData.Children.Add(droneItem);
                }
                x += 30;
            }
        }

        public DroneWindow(IBL.IBL blMain, IBL.BO.DroneBL drone)
        {
            InitializeComponent();
            bl = blMain;
            DroneStatus.IsEditable = true;
            DroneStatus.Text = (drone.MaxWeight).ToString();
            DroneStatus.IsEnabled = false;


            ModelBlock.Text = "update model here:";
            Model.Text = drone.Model;

            ID.Text = (drone.ID).ToString();
            ID.IsEnabled = false;
            DroneStatus.SelectedValue = (drone.MaxWeight).ToString();


            Button update = new Button();
            update.Content = "update";
            DroneData.Children.Add(update);
            update.VerticalAlignment = VerticalAlignment.Top;
            update.Margin = new Thickness(43, 190, 0, 0);
            Button sendDroneCharge = new Button();
            sendDroneCharge.Content = "send drone to charge";
            DroneData.Children.Add(sendDroneCharge);
            sendDroneCharge.VerticalAlignment = VerticalAlignment.Top;
            sendDroneCharge.Margin = new Thickness(43, 210, 0, 0);
            Button releaseDrone = new Button();
            releaseDrone.Content = "release Drone from Charge";
            DroneData.Children.Add(releaseDrone);
            releaseDrone.VerticalAlignment = VerticalAlignment.Top;
            releaseDrone.Margin = new Thickness(43, 230, 0, 0);
            /*            Button update = new Button();
                        update.Content = "drone location";
                        droneWin.Children.Add(update);
                        update.VerticalAlignment = VerticalAlignment.Top;
                        update.Margin = new Thickness(43, 162, 0, 0);
            */


            TextBlock Location = new TextBlock();
            Location.Text = "drone location";
            DroneData.Children.Add(Location);
            Location.TextWrapping = TextWrapping.Wrap;
            Location.VerticalAlignment = VerticalAlignment.Top;
            Location.Margin = new Thickness(43, 162, 0, 0);

            TextBox LocationData = new TextBox();
            LocationData.Text = (drone.Location).ToString();
            DroneData.Children.Add(LocationData);
            LocationData.TextWrapping = TextWrapping.Wrap;
            LocationData.VerticalAlignment = VerticalAlignment.Top;
            LocationData.Margin = new Thickness(199, 162, 0, 0);
            Location.IsEnabled = false;

            TextBlock BatteryStatus = new TextBlock();
            BatteryStatus.Text = $"Battery Status {drone.BatteryStatus}";
            DroneData.Children.Add(BatteryStatus);
            BatteryStatus.TextWrapping = TextWrapping.Wrap;
            BatteryStatus.VerticalAlignment = VerticalAlignment.Top;
            BatteryStatus.Margin = new Thickness(43, 192, 0, 0);

/*            TextBox LocationData = new TextBox();
            LocationData.Text = (drone.Location).ToString();
            DroneData.Children.Add(LocationData);
            LocationData.TextWrapping = TextWrapping.Wrap;
            LocationData.VerticalAlignment = VerticalAlignment.Top;
            LocationData.Margin = new Thickness(199, 162, 0, 0);
            Location.IsEnabled = false;*/

            TextBlock Status = new TextBlock();
            Status.Text = $"Drone Status {drone.Status}";
            DroneData.Children.Add(Status);
            Status.TextWrapping = TextWrapping.Wrap;
            Status.VerticalAlignment = VerticalAlignment.Top;
            Status.Margin = new Thickness(43, 222, 0, 0);
            /*
                        TextBox LocationData = new TextBox();
                        LocationData.Text = (drone.Location).ToString();
                        DroneData.Children.Add(LocationData);
                        LocationData.TextWrapping = TextWrapping.Wrap;
                        LocationData.VerticalAlignment = VerticalAlignment.Top;
                        LocationData.Margin = new Thickness(199, 162, 0, 0);
                        Location.IsEnabled = false;*/

            TextBlock Parcel = new TextBlock();
            Parcel.Text = "parcel ID" + (drone.Parcel);
            DroneData.Children.Add(Parcel);
            Parcel.TextWrapping = TextWrapping.Wrap;
            Parcel.VerticalAlignment = VerticalAlignment.Top;
            Parcel.Margin = new Thickness(43, 252, 0, 0);
            // ID + " model:  " + Model + " MaxWeight: " + MaxWeight + " BatteryStatus: " + BatteryStatus + " Status: " + Status + " Parcel: " + Parcel + " Location: " + Location;
        }
        
    }
}
