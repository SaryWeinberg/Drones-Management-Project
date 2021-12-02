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
            string[] dataArr = { "ID", "max_weight" , "model" , "station_ID" };

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
        }
    }    
}
