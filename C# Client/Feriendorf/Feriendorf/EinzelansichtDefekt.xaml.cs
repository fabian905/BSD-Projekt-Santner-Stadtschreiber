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
        Schaden sch;
        public EinzelansichtDefekt(Schaden schaden)
        {
            InitializeComponent();

            sch = schaden;
            string s = db.Connect();
            if (s != "CONNECTED!")
                MessageBox.Show("Error while connecting! " + s);

            init();
            db.Close();
            
        }

        private void init()
        {
            try
            {
                OleDbDataReader r = db.ExecuteCommand("select bezeichnung from Haus where hid like "+ sch.HausID);
                while (r.Read())
                {
                    tbOrt.Text = r[0].ToString();
                }
                tbBeschreibung.Text = sch.Bezeichnung;              
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
                db.Connect();
                OleDbDataReader r = db.ExecuteCommand("update schaden set status = 'behoben' where sid like " + sch.SchadenID);
                db.Close();
                this.Close();
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
                db.Connect();
                OleDbDataReader r = db.ExecuteCommand("update schaden set status = 'inarbeit' where sid like "+ sch.SchadenID);
                db.Close();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                db.Close();
            }
        }
        private void btnSchließen_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
