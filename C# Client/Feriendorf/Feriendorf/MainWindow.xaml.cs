using System;
using System.Collections.Generic;
using System.Data.OleDb;
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

namespace Feriendorf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Database db = new Database();
        public MainWindow()
        {
            InitializeComponent();
                       
            string s = db.Connect();
            if (s != "CONNECTED!")
                MessageBox.Show("Error while connecting! " + s);

            OleDbDataReader r = db.ExecuteCommand("SELECT * from feriendorf");

            while (r.Read())
            {
                cbFeriendorfAuswahl.Items.Add(r[0].ToString());
            }

            db.Close();

        }

        private void bttnLogin_Click(object sender, RoutedEventArgs e)
        {
            Overview o = new Overview(cbFeriendorfAuswahl.SelectedValue.ToString());
            o.Show();
            this.Close();
        }

        private void bttnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
