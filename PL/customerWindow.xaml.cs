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
    /// Interaction logic for customerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        IBL.IBL bl;
        string[] dataArr = { "ID", "phone", "name", "longitude", "latitude" };
        public CustomerWindow(IBL.IBL blMain)
        {
            InitializeComponent();
            bl = blMain;
            int x = 70;
            foreach (string item in dataArr)
            {
                TextBlock customerItemT = new TextBlock();
                customerItemT.Text = $"Enter customer {item}:";
                customerItemT.TextWrapping = TextWrapping.Wrap;
                customerItemT.VerticalAlignment = VerticalAlignment.Top;
                customerItemT.Margin = new Thickness(43, x, 0, 0);
                CustomerDate.Children.Add(customerItemT);
                TextBox customerItem = new TextBox();
                customerItem.Name = $"customer{item}";
                customerItem.TextWrapping = TextWrapping.Wrap;
                customerItem.VerticalAlignment = VerticalAlignment.Top;
                customerItem.Margin = new Thickness(199, x, 0, 0);
                CustomerDate.Children.Add(customerItem);
                x += 30;
            }
        }
    }
}
