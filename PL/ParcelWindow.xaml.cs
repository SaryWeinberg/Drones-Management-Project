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
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
        IBL.IBL bl;
        string[] dataArr = { "sender_ID", "target_ID", "weight", "priority" };
        public ParcelWindow(IBL.IBL blMain)
        {
            InitializeComponent();
            bl = blMain;

            int x = 70;
            foreach (string item in dataArr)
            {
                TextBlock parcelItemT = new TextBlock();
                parcelItemT.Text = $"Enter parcel {item}:";
                parcelItemT.TextWrapping = TextWrapping.Wrap;
                parcelItemT.VerticalAlignment = VerticalAlignment.Top;
                parcelItemT.Margin = new Thickness(43, x, 0, 0);
                ParcelData.Children.Add(parcelItemT);
                if (item == "weight" || item == "priority")
                {
                    ComboBox parcelItemC = new ComboBox();
                    parcelItemC.Name = $"parcel{item}";
                    parcelItemC.VerticalAlignment = VerticalAlignment.Top;
                    parcelItemC.Margin = new Thickness(199, x, 0, 0);
                    switch (item)
                    {
                        case "weight":
                            parcelItemC.ItemsSource = Enum.GetValues(typeof(WeightCategories)); break;
                        case "priority":
                            parcelItemC.ItemsSource = Enum.GetValues(typeof(Priorities)); break;

                    }
                    ParcelData.Children.Add(parcelItemC);
                }
                else
                {
                    TextBox parcelItem = new TextBox();
                    parcelItem.Name = $"parcel{item}";
                    parcelItem.TextWrapping = TextWrapping.Wrap;
                    parcelItem.VerticalAlignment = VerticalAlignment.Top;
                    parcelItem.Margin = new Thickness(199, x, 0, 0);
                    ParcelData.Children.Add(parcelItem);
                }
                x += 30;
            }
        }
    }
}
