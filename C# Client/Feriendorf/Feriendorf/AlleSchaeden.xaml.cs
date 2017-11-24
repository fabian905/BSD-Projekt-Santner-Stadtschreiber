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
using System.Windows.Shapes;

namespace Feriendorf
{
    /// <summary>
    /// Interaction logic for AlleSchaeden.xaml
    /// </summary>
    public partial class AlleSchaeden : Window
    {
        public AlleSchaeden()
        {
            Database db = new Database();
            InitializeComponent();

            try
            {
                db.Connect();
                OleDbDataReader rd = db.ExecuteCommand("select * from schaden");
 
                while (rd.Read())
                {
                    dataGrid.Items.Add(new Schaden(rd[0].ToString(), rd[1].ToString(), rd[2].ToString(), rd[3].ToString(), rd[4].ToString(), rd[5].ToString()));
                }
          
                db.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                db.Close();
            }
        }

        private void bttnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
