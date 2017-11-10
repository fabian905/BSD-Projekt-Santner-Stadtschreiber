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
    /// Interaction logic for EinzelansichtDefekt.xaml
    /// </summary>
    public partial class EinzelansichtDefekt : Window
    {
        Database db = new Database();
        public EinzelansichtDefekt(Schaden schaden)
        {
            InitializeComponent();

         
            string s = db.Connect();
            if (s != "CONNECTED!")
                MessageBox.Show("Error while connecting! " + s);

            init(schaden);
            db.Close();
            
        }

        private void init(Schaden s)
        {
            try
            {
                tbBeschreibung.Text = s.Bezeichnung;
                tbOrt.Text = s.HausID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void bttnBeheben_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //OleDbDataReader r = db.ExecuteCommand("");
                //while (r.Read())
                //{
                //    
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void bttn_InArbeit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                //OleDbDataReader r = db.ExecuteCommand("");
                //while (r.Read())
                //{
                //    
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSchließen_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
