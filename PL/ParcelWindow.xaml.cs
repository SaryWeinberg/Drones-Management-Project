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
        public ParcelWindow(IBL.IBL blMain)
        {
            InitializeComponent();
            bl = blMain;
            ParcelWeight.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            ParcelPriority.ItemsSource = Enum.GetValues(typeof(Priorities));
        }
    }
}
