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

        public Overview(string feriendorf)
        {
            InitializeComponent();

            string s = db.Connect();
            if (s != "CONNECTED!")
                MessageBox.Show("Error while connecting! " + s);

           // getAndDrawPolygons();
            getAndDrawPoints(feriendorf);

            db.Close();
        }

        private void getAndDrawPolygons()
        {
            
            canvas.Children.Clear();

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
            lbAlleDefekte.Items.Clear();
            PointCollection points = new PointCollection();
            OleDbDataReader r = db.ExecuteCommand("select bezeichnung from schaden where sid in (select sid from vorhanden where hid in (select hid from haus where fid like '"+feriendorf+"'))");

            while (r.Read())
            {
                //points = new pointcollection();
                //points.add(new point(int.parse(r[0].tostring()) * 2, int.parse(r[1].tostring()) * 2));
                //addtocanvas(points);

                lbAlleDefekte.Items.Add(r[0].ToString());
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
            EinzelansichtDefekt ed = new EinzelansichtDefekt();
            ed.Show();

        }
    }
}
