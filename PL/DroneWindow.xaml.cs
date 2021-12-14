using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        IBL.BO.DroneBL Drone;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blMain"></param>
        public DroneWindow(IBL.IBL blMain)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            StationIdLabel.Visibility = Visibility.Visible;
            batteryStatusLabel.Visibility = Visibility.Hidden;
            max_weight.ItemsSource = Enum.GetValues(typeof(WeightCategories));
/*            update.Visibility = Visibility.Hidden;
            sendDroneToCharge.Visibility = Visibility.Hidden;
            assignParcelToDrone.Visibility = Visibility.Hidden;
            releaseDronefromCharge.Visibility = Visibility.Hidden;
            collectParcelByDrone.Visibility = Visibility.Hidden;
            deliveryParcelByDrone.Visibility = Visibility.Hidden;*/

            /*      string[] dataArr = { "ID", "max_weight", "model", "station_ID" };*/

            /*         int x = 70;
                     foreach (string item in dataArr)*/
            /*  {*/
            /*                Label droneItemT = new Label();
                            droneItemT.Content = $"Enter drone {item}:";
                            droneItemT.VerticalAlignment = VerticalAlignment.Top;
                            droneItemT.Margin = new Thickness(43, x, 0, 0);
                            DroneData.Children.Add(droneItemT);*/
            /*                if (item == "max_weight")
                            {
                                ComboBox droneItemC = new ComboBox();
                                droneItemC.Name = $"drone{item}";
                                droneItemC.VerticalAlignment = VerticalAlignment.Top;
                                droneItemC.Margin = new Thickness(199, x, 0, 0);*/

            /*                    DroneData.Children.Add(droneItemC);*/
            /* }*/
            /*       else
                   {*/
            /*                    TextBox droneItem = new TextBox();*/
            /*                    droneItem.Name = $"drone{item}";
                                droneItem.VerticalAlignment = VerticalAlignment.Top;
                                droneItem.Margin = new Thickness(199, x, 0, 0);
                                DroneData.Children.Add(droneItem);*/
            /*               }
                           x += 30;*/
            /* }*/

            /*           Button sendNewDrone = new Button();
                       sendNewDrone.Content = "Send";
                       sendNewDrone.VerticalAlignment = VerticalAlignment.Top;
                       sendNewDrone.Margin = new Thickness(43, x, 0, 0);
                       sendNewDrone.Click += AddNewDrone;
                       DroneData.Children.Add(sendNewDrone);


                       Button CancelButton = new Button();
                       CancelButton.Content = "Cancel";
                       CancelButton.VerticalAlignment = VerticalAlignment.Top;
                       CancelButton.Margin = new Thickness(43, x + 30, 0, 0);
                       CancelButton.Click += DataWindowClosing;
                       DroneData.Children.Add(CancelButton);*/
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewDrone(object sender, RoutedEventArgs e)
        {
            string ID = DroneID.Text;
            int weight = max_weight.SelectedIndex + 1;
            string model = DroneModel.Text;
            string stationID = station_ID.Text;
            try
            {

                int id = int.Parse(ID);
                int station = int.Parse(stationID);
                MessageBoxResult result =
                 MessageBox.Show(
                   bl.AddDroneBL(id, model, (WeightCategories)weight, station),
                   $"Add drone ID - {ID}",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    new DroneListWindow(bl).Show();
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blMain"></param>
        /// <param name="drone"></param>
        public DroneWindow(IBL.IBL blMain, IBL.BO.DroneBL drone)
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            bl = blMain;
            Drone = drone;
            StationIdLabel.Visibility = Visibility.Hidden;
            batteryStatusLabel.Visibility = Visibility.Visible;
/*            sendNewDrone.Visibility = Visibility.Hidden;
*/            /*            string[] dataArr = { "ID", "max_weight", "battery_status", "status", "longitude", "latitude" };*/
            /*        int x = 100;*/

            DroneModelLabel.Content = "Upadate drone model:";
            DroneModel.Text = drone.Model.ToString();
            DroneModel.TextChanged += AddUpdateBTN;
            DroneID.IsEnabled = false;
            DroneIDLabel.Content = " drone ID:";
            MaxWeightLabel.Content = "Max Weight";

            /*            Label DroneModel = new Label();
                        DroneModel.Content = $"Upadate drone model:";
                        DroneModel.VerticalAlignment = VerticalAlignment.Top;
                        DroneModel.Margin = new Thickness(43, 70, 0, 0);*/
            /*            DroneData.Children.Add(DroneModel);*/
            /*            TextBox dronemodel = new TextBox();
                        dronemodel.Name = "dronemodel";*/

            /*            dronemodel.VerticalAlignment = VerticalAlignment.Top;
                        dronemodel.Margin = new Thickness(199, 70, 0, 0);*/
            /*            dronemodel.Text = drone.Model.ToString();
                        dronemodel.TextChanged += AddUpdateBTN;*/
            /*            DroneData.Children.Add(dronemodel);*/
            /*           foreach (string item in dataArr)
                       {*/
            /*                Label droneItemT = new Label();
                            droneItemT.Content = $"Upadate drone {item}:";
                            droneItemT.VerticalAlignment = VerticalAlignment.Top;
                            droneItemT.Margin = new Thickness(43, x, 0, 0);
                            DroneData.Children.Add(droneItemT);*/
            /*                if (item == "max_weight" || item == "status")
                            {*/
            /*                    ComboBox droneItemC = new ComboBox();
                                droneItemC.Name = $"drone{item}";
                                droneItemC.VerticalAlignment = VerticalAlignment.Top;
                                droneItemC.Margin = new Thickness(199, x, 0, 0);*/
            /*                    switch (item)
                                {*/

            /*    case "max_weight":*/
            max_weight.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            max_weight.Text = drone.MaxWeight.ToString();
            max_weight.IsEnabled = false;
            max_weight.IsEditable = true;
            /*    break;*/
            Status.ItemsSource = Enum.GetValues(typeof(Status));
            Status.Text = drone.Status.ToString();
            Status.IsEnabled = false;
            Status.IsEditable = true;

            DroneID.Text = drone.ID.ToString();

            batteryStatus.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            batteryStatus.Text = drone.BatteryStatus.ToString();
            batteryStatus.IsEditable = true;
            longitude.Text = drone.Location.Longitude.ToString();
            latitude.Text = drone.Location.Latitude.ToString();


            /*              case "status":*/
            /*                droneItemC.ItemsSource = Enum.GetValues(typeof(Status));
                            droneItemC.Text = drone.Status.ToString();
                            droneItemC.IsEnabled = false;
                            droneItemC.IsEditable = true; break;*/
            /*      }*/
            /*       DroneData.Children.Add(droneItemC);*/
            /*  }*/
            /*                else
                        {*/
            /*                TextBox droneItem = new TextBox();
                            droneItem.Name = $"drone{item}";
                            droneItem.VerticalAlignment = VerticalAlignment.Top;
                            droneItem.Margin = new Thickness(199, x, 0, 0);*/
            /*                switch (item)
                            {*/
            /*                   case "ID":
                                   drone.Text = drone.ID.ToString(); droneItem.IsEnabled = false; break;
                               case "battery_status":
                                   droneItem.Text = drone.BatteryStatus.ToString(); droneItem.IsEnabled = false; break;
                               case "longitude":
                                   droneItem.Text = drone.Location.Longitude.ToString(); droneItem.IsEnabled = false; break;
                               case "latitude":
                                   droneItem.Text = drone.Location.Latitude.ToString(); droneItem.IsEnabled = false; break;
                           }*/
            /*                DroneData.Children.Add(droneItem);*/
            /*            }
                x += 30;*/

            /*position = 400;*/
/*            update.Visibility = Visibility.Hidden;
            sendDroneToCharge.Visibility = Visibility.Hidden;
            assignParcelToDrone.Visibility = Visibility.Hidden;
            releaseDronefromCharge.Visibility = Visibility.Hidden;
            collectParcelByDrone.Visibility = Visibility.Hidden;
            deliveryParcelByDrone.Visibility = Visibility.Hidden;*/
            if (drone.Status == DroneStatus.Available)
            {
/*                AddBTN("send drone to charge");
*/                sendDroneToCharge.Visibility = Visibility.Visible;
                sendDroneToCharge.Margin = new Thickness(0,450,0,0);
                assignParcelToDrone.Visibility = Visibility.Visible;
                assignParcelToDrone.Margin = new Thickness(0, 450, -450, 0);
/*                AddBTN("assign Parcel To Drone");
*/          }
            else if (drone.Status == DroneStatus.Maintenance)
            {                
                timechargeLabel.Margin = new Thickness(43, 270, 710, 0);
                timecharge.Margin = new Thickness(199, 270, 500, 0);
                timecharge.Visibility = Visibility.Visible;
                timechargeLabel.Visibility = Visibility.Visible;
                timecharge.TextChanged += releseDrone;
                               /*                AddBTN("release Drone from Charge");
                */
            }
            else if (drone.Status == DroneStatus.Delivery)
            {
                if (bl.GetSpesificParcelBL(drone.Parcel.ID).PickedUp == null)
                {
/*                    AddBTN("collect Parcel By Drone");
*/                  collectParcelByDrone.Visibility = Visibility.Visible;
                    collectParcelByDrone.Margin = new Thickness(0, 450, 0, 0);


                }
                else
                {
                    deliveryParcelByDrone.Visibility = Visibility.Visible;
                    deliveryParcelByDrone.Margin = new Thickness(0, 450, 0, 0);


                    /*                    AddBTN("delivery Parcel By Drone");
                    */
                }
            }
        }
       /* static int position = 400;*/
        /*private void AddBTN(string item)
        {
            Button botton = new Button();
            botton.Content = item;
            botton.VerticalAlignment = VerticalAlignment.Top;
            switch (item)
            {
                case "send drone to charge": botton.Click += SendDroneToCharge; break;
                case "release Drone from Charge":
                    botton.Click += ReleaseDronefromCharge;
                    timechargeLabel.Margin = new Thickness(43, position + 30, 0, 0);
                    timecharge.Margin = new Thickness(199, position + 30, 0, 0);
                    timecharge.Visibility = Visibility.Visible;
                    timechargeLabel.Visibility = Visibility.Visible;
                    break;
                case "assign Parcel To Drone": botton.Click += AssignParcelToDrone; break;
                case "collect Parcel By Drone": botton.Click += CollectParcelByDrone; break;
                case "delivery Parcel By Drone": botton.Click += DeliveryParcelByDrone; break;
            }
            botton.Margin = new Thickness(43, position, 0, 0);
            DroneData.Children.Add(botton);
            position += 30;
        }*/


        private void AddUpdateBTN(object sender, RoutedEventArgs e)
        {
           /* Button botton = new Button();
            botton.Content = "update";
            botton.VerticalAlignment = VerticalAlignment.Top;
            botton.Click += UpdateDrone;
            botton.Margin = new Thickness(43, 250, 0, 0);
            DroneData.Children.Add(botton);*/

            update.Visibility = Visibility.Visible;
            update.Margin = new Thickness(0, 450, 450, 0);
        }

        private void releseDrone(object sender, RoutedEventArgs e) {

            releaseDronefromCharge.Visibility = Visibility.Visible;
            releaseDronefromCharge.Margin = new Thickness(0, 450, 0, 0);
        }

        private void ClosingWindow(object sender, RoutedEventArgs e) => Close();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateDrone(object sender, RoutedEventArgs e)
        {
            /*            string ID = DroneID.Text;*/
            string model = DroneModel.Text;
            try
            {
                MessageBoxResult result =
                  MessageBox.Show(
                    bl.UpdateDroneData(Drone.ID, model),
                    $"Update drone - {Drone.ID} MODEL",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    new DroneListWindow(bl).Show();
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendDroneToCharge(object sender, RoutedEventArgs e)
        {
            /*            string ID =DroneID.Text;*/
            try
            {
                MessageBoxResult result =
                MessageBox.Show(
                  bl.SendDroneToCharge(Drone.ID),
                  $"Send drone ID - {Drone.ID} to charge",
                  MessageBoxButton.OK,
                  MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    new DroneListWindow(bl).Show();
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReleaseDronefromCharge(object sender, RoutedEventArgs e)
        {
            /*            string ID = DroneData.Children.OfType<TextBox>().First(txt => txt.Name == "droneID").Text;*/
            try
            {
                MessageBoxResult result =
                MessageBox.Show(
                  bl.ReleaseDroneFromCharge(Drone.ID, int.Parse(timecharge.Text)),
                  $"Release drone ID - {Drone.ID} from charge",
                  MessageBoxButton.OK,
                  MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    new DroneListWindow(bl).Show();
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssignParcelToDrone(object sender, RoutedEventArgs e)
        {
            /*            string ID = DroneData.Children.OfType<TextBox>().First(txt => txt.Name == "droneID").Text;*/
            try
            {
                MessageBoxResult result =
                 MessageBox.Show(
                 bl.AssignParcelToDrone(Drone.ID),
                 $"Assign parcel",
                 MessageBoxButton.OK,
                 MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    new DroneListWindow(bl).Show();
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CollectParcelByDrone(object sender, RoutedEventArgs e)
        {
            /*            string ID = DroneID.Text;*/
            try
            {
                MessageBoxResult result =
                MessageBox.Show(
                bl.CollectParcelByDrone(Drone.ID),
                $"Collect parcel",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    new DroneListWindow(bl).Show();
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeliveryParcelByDrone(object sender, RoutedEventArgs e)
        {
            /*            string ID = DroneData.Children.OfType<TextBox>().First(txt => txt.Name == "droneID").Text;*/
            try
            {
                MessageBoxResult result =
               MessageBox.Show(
               bl.DeliveryParcelByDrone(Drone.ID),
               $"Delivery parcel",
               MessageBoxButton.OK,
               MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    new DroneListWindow(bl).Show();
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

    }
}
