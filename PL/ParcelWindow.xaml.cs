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
            WindowStyle = WindowStyle.None;
            bl = blMain;

            int x = 70;
            foreach (string item in dataArr)
            {
                Label parcelItemT = new Label();
                parcelItemT.Content = $"Enter parcel {item}:";
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
                    parcelItem.VerticalAlignment = VerticalAlignment.Top;
                    parcelItem.Margin = new Thickness(199, x, 0, 0);
                    ParcelData.Children.Add(parcelItem);
                }
                x += 30;
            }

            Button sendNewParcel = new Button();
            sendNewParcel.Content = "Send";
            sendNewParcel.VerticalAlignment = VerticalAlignment.Top;
            sendNewParcel.Margin = new Thickness(43, x, 0, 0);
            sendNewParcel.Click += AddNewParcel;
            ParcelData.Children.Add(sendNewParcel);
        }

        private void AddNewParcel(object sender, RoutedEventArgs e)
        {
            string massage;
            string senderID = ParcelData.Children.OfType<TextBox>().First(txt => txt.Name == "parcelsender_ID").Text;
            string targetID = ParcelData.Children.OfType<TextBox>().First(txt => txt.Name == "parceltarget_ID").Text;
            int weight = ParcelData.Children.OfType<ComboBox>().First(txt => txt.Name == "parcelweight").SelectedIndex;
            int priority = ParcelData.Children.OfType<ComboBox>().First(txt => txt.Name == "parcelpriority").SelectedIndex;
            try
            {
                massage = bl.AddParcelBL(int.Parse(senderID), int.Parse(targetID), (WeightCategories)weight, (Priorities)priority);
                MessageBox.Show(massage);

                MessageBoxResult result =
                   MessageBox.Show(
                   bl.AddParcelBL(int.Parse(senderID), int.Parse(targetID), (WeightCategories)weight, (Priorities)priority),
                   $"Add parcel",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    new ParcelListWindow(bl).Show();
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
