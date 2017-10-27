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

namespace Feriendorf
{
    /// <summary>
    /// Interaction logic for EinzelansichtDefekt.xaml
    /// </summary>
    public partial class EinzelansichtDefekt : Window
    {
        public EinzelansichtDefekt()
        {
            InitializeComponent();
        }

        private void bttnBeheben_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSchließen_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
