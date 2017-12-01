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



            if (sch.Status == "inarbeit")
            {
                bttn_InArbeit.IsEnabled = false;
                cbTechniker.IsEnabled = true;
                tbMaterialaufwand.IsEnabled = true;
                tbSonstigeInfos.IsEnabled = true;
            }
            else
            {
                bttnBeheben.IsEnabled = false;
                
                cbTechniker.IsEnabled = false;
                tbMaterialaufwand.IsEnabled = false;
                tbSonstigeInfos.IsEnabled = false;
            }

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

                r = db.ExecuteCommand("select vorname, nachname from mitarbeiter where beruf = 'techniker'");

                while (r.Read())
                {
                    cbTechniker.Items.Add(String.Format(r[0].ToString()+" "+r[1].ToString()));
                }
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
        
                OleDbDataReader r = db.ExecuteCommand("select count(*) from reparatur");
                int id = 0;
                while (r.Read())
                {
                    id = Convert.ToInt32(r[0].ToString());
                    
                }
                id++;
       
                string name = cbTechniker.SelectedItem.ToString();
                //MessageBox.Show(id.ToString());
                //MessageBox.Show(name);
                string[] y = name.Split(' ');
                //MessageBox.Show(y[0].ToString());
                //MessageBox.Show(y[1].ToString());

                r = db.ExecuteCommand("select mid from mitarbeiter where vorname like '"+y[0].ToString()+ "' and nachname like '" + y[1].ToString()+"'");
                int mid = 0;
                //MessageBox.Show(mid.ToString());
                while (r.Read())
                {
                    mid = Convert.ToInt32(r[0].ToString());
                }
                //MessageBox.Show(mid.ToString());
                int matauf = Convert.ToInt32(tbMaterialaufwand.Text);

                string date = string.Format(DateTime.Now.Day.ToString()+"."+ DateTime.Now.Month.ToString()+"."+DateTime.Now.Year.ToString());

                db.ExecuteCommand("insert into reparatur values (" + id + ", " + matauf + ", to_date('"+ date + "','dd.mm.yyyy'), '" + tbSonstigeInfos.Text + "', " + mid + ")");

                //MessageBox.Show(id.ToString());
                db.ExecuteCommand("update schaden set status = 'behoben' where sid like " + sch.SchadenID);
                
                db.Close();
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                db.Close();
            }
        }

        private void bttn_InArbeit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db.Connect();
                db.ExecuteCommand("update schaden set status = 'inarbeit' where sid like "+ sch.SchadenID);

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
