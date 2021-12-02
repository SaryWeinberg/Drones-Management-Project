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
            DroneStatus.ItemsSource = Enum.GetValues(typeof(DroneStatus));
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
            droneWin.Children.Add(update);
            update.VerticalAlignment = VerticalAlignment.Top;
            update.Margin = new Thickness(43, 190, 0, 0);
            Button sendDroneCharge = new Button();
            sendDroneCharge.Content = "send drone to charge";
            droneWin.Children.Add(sendDroneCharge);
            sendDroneCharge.VerticalAlignment = VerticalAlignment.Top;
            sendDroneCharge.Margin = new Thickness(43, 210, 0, 0);
            Button releaseDrone = new Button();
            releaseDrone.Content = "release Drone from Charge";
            droneWin.Children.Add(releaseDrone);
            releaseDrone.VerticalAlignment = VerticalAlignment.Top;
            releaseDrone.Margin = new Thickness(43, 230, 0, 0);
/*            Button update = new Button();
            update.Content = "drone location";
            droneWin.Children.Add(update);
            update.VerticalAlignment = VerticalAlignment.Top;
            update.Margin = new Thickness(43, 162, 0, 0);
*/


            /*            TextBlock Location = new TextBlock();
                        Location.Text = "drone location";
                        droneWin.Children.Add( Location);
                        Location.TextWrapping = TextWrapping.Wrap;
                        Location.VerticalAlignment = VerticalAlignment.Top;
                        Location.Margin = new Thickness(43, 162,0,0);

                        TextBox LocationData = new TextBox();
                        LocationData.Text = (drone.Location).ToString();
                        droneWin.Children.Add(LocationData);
                        LocationData.TextWrapping = TextWrapping.Wrap;
                        LocationData.VerticalAlignment = VerticalAlignment.Top;
                        LocationData.Margin = new Thickness(199, 162, 0, 0);
                        Location.IsEnabled = false;
            */
        }
    }
}
