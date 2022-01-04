﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BLApi.IBL bl;
        public MainWindow(BLApi.IBL blMain)
        {
            bl = blMain;
            InitializeComponent();
        }

        private void ViewDroneListWindow(object sender, RoutedEventArgs e)
        {
            new DroneListWindow(bl).Show(); Close();
        }

        private void ViewParcelListWindow(object sender, RoutedEventArgs e)
        {
            new ParcelListWindow(bl).Show(); Close();
        }

        private void ViewCustomerListWindow(object sender, RoutedEventArgs e)
        {
            new CustomerListWindow(bl).Show(); Close();
        }

        private void ViewStationListWindow(object sender, RoutedEventArgs e)
        {
            new StationListWindow(bl).Show(); Close();
        }
    }
}
