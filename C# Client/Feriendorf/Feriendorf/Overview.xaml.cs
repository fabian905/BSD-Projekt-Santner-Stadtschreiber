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
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class Overview : Window
    {
        Database db = new Database();
        Schaden s;
        public Overview(string feriendorf)
        {
            InitializeComponent();

            string s = db.Connect();
            if (s != "CONNECTED!")
                MessageBox.Show("Error while connecting! " + s);

            fillLbNeu(feriendorf);
            fillLbInArbeit(feriendorf);
            //getAndDrawPolygons();
            getAndDrawPoints(feriendorf);

            db.Close();
        }

        private void fillLbInArbeit(string feriendorf)
        {
            try
            {
                OleDbDataReader rInArbeit = db.ExecuteCommand("select * from schaden where status LIKE 'inarbeit' AND hid in (select hid from haus where fid like '" + feriendorf + "')");

                while (rInArbeit.Read())
                {
                    lbInArbeit.Items.Add(String.Format(rInArbeit[0].ToString() + " " + rInArbeit[1].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fillLbNeu(string feriendorf)
        {
            
            try
            {
                OleDbDataReader rNeu = db.ExecuteCommand("select * from schaden where status LIKE 'offen' AND hid in (select hid from haus where fid like '" + feriendorf + "')");
                while (rNeu.Read())
                {
                    lbAlleDefekte.Items.Add(String.Format(rNeu[0].ToString() + " " + rNeu[1].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getAndDrawPolygons()
        {        
            OleDbDataReader r = db.ExecuteCommand("SELECT t.X, t.Y, t.id FROM polygon p, TABLE(SDO_UTIL.GETVERTICES(p.shape)) t ORDER BY t.id");
            canvas.Children.Clear();
            PointCollection points = null;
            points = new PointCollection();
            while (r.Read())
            {
                points.Add(new Point(int.Parse(r[0].ToString()), int.Parse(r[1].ToString())));
            }
            addToCanvas(points);
        }

        private void getAndDrawPoints(string feriendorf)
        {
            try
            {
                PointCollection points = new PointCollection();
                OleDbDataReader r = db.ExecuteCommand("SELECT t.X, t.Y, t.id FROM haus p, TABLE(SDO_UTIL.GETVERTICES(p.shape)) t ORDER BY t.id");

                while (r.Read())
                {
                    points = new PointCollection();
                    points.Add(new Point(int.Parse(r[0].ToString()) * 2, int.Parse(r[1].ToString()) * 2));
                    addToCanvas(points);
                }               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addToCanvas(PointCollection points)
        {
            try
            {
                Polygon pol1 = new Polygon();
                pol1.Stroke = Brushes.Black;
                pol1.StrokeThickness = 2;
                pol1.Points = points;
                canvas.Children.Add(pol1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured while trying to paint on the canvas! " + ex.Message);
            }
        }


        private void bttnClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
            
        }

        private void lbAlleDefekte_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string x = lbAlleDefekte.SelectedValue.ToString();
            string[] y = x.Split(' ');
            MessageBox.Show(y[0]);

            OleDbDataReader r = db.ExecuteCommand("select * from schaden where sid LIKE " + y[0]);
            s = new Schaden(r[0].ToString(), r[1].ToString(), r[2].ToString(),r[3].ToString(),r[4].ToString(), r[5].ToString());

            EinzelansichtDefekt ed = new EinzelansichtDefekt(s);
            ed.Show();
        }

        private void lbInArbeit_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            //OleDbDataReader rNeu = db.ExecuteCommand("select  from schaden where status LIKE 'offen' AND hid in (select hid from haus where fid like '" + feriendorf + "')");

            //EinzelansichtDefekt ed = new EinzelansichtDefekt(lbInArbeit.SelectedValue.ToString());
            //ed.Show();
        }
    }
}
