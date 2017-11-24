using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading;
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
        String feriend;
        Schaden s;
        bool track;
        ThreadStart ts;
        Thread t;
        bool b = false;

        public Overview(string feriendorf)
        {
            InitializeComponent();
            db.Connect();

            feriend = feriendorf;
            getAndDrawPoints(feriend);

            fillListBoxes();

            track = true;
            try
            {
                ts = new ThreadStart(threadDrawing);
                t = new Thread(ts);
                t.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            db.Close();
        }
        private void fillListBoxes()
        {
            db.Connect();

            fillLbNeu(feriend);
            fillLbInArbeit(feriend);
            
            db.Close();
        }
        private void threadDrawing()
        {
            while (track)
            {
                if (b != false)
                {
                    Dispatcher.Invoke(fillListBoxes);
                    Thread.Sleep(1000);
                }

            }
        }
        private void fillLbInArbeit(string feriendorf)
        {
            try
            {
                lbInArbeit.Items.Clear();
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
                lbAlleDefekte.Items.Clear();
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

        private void getAndDrawPoints(string feriendorf)
        {
            try
            {
                PointCollection points = new PointCollection();
                canvas.Children.Clear();

                OleDbDataReader r = db.ExecuteCommand("SELECT t.X, t.Y, t.id, s.bezeichnung FROM haus s, TABLE(SDO_UTIL.GETVERTICES(s.shape)) t ORDER BY s.bezeichnung, t.id");
                
                int counter = 5; 
                while (r.Read())
                {
                    if (counter == 5)
                    {

                        addToCanvas(points, false);
                        counter = 1;
                        points = new PointCollection();
                    }
                    if (int.Parse(r[2].ToString()) == counter)
                    {
                        points.Add(new Point(int.Parse(r[0].ToString()) * 4, int.Parse(r[1].ToString()) * 4));
                        counter++;
                    }

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void getAndDrawPoints(string feriendorf, string haus)
        {
            try
            {
                PointCollection points = new PointCollection();

                OleDbDataReader r = db.ExecuteCommand("SELECT t.X, t.Y, t.id, s.hid FROM haus s, TABLE(SDO_UTIL.GETVERTICES(s.shape)) t WHERE s.hid like "+haus+" ORDER BY s.hid, t.id");

                points = new PointCollection();

                while (r.Read())
                {         
                    points.Add(new Point(int.Parse(r[0].ToString()) * 4, int.Parse(r[1].ToString()) * 4));              
                }
                addToCanvas(points, true);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void addToCanvas(PointCollection points, bool isdefekt)
        {
            try
            {
                if (isdefekt == true)
                {
                    Polygon pol1 = new Polygon();
                    pol1.Stroke = Brushes.Red;
                    pol1.StrokeThickness = 4;
                    pol1.Points = points;
                    canvas.Children.Add(pol1);
                }
                else
                {
                    Polygon pol1 = new Polygon();
                    pol1.Stroke = Brushes.Black;
                    pol1.StrokeThickness = 2;
                    pol1.Points = points;
                    canvas.Children.Add(pol1);
                }
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
            t.Abort();

            this.Close();
            
        }

        private void lbAlleDefekte_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                db.Connect();
                string x = lbAlleDefekte.SelectedValue.ToString();
                string[] y = x.Split(' ');

                OleDbDataReader r = db.ExecuteCommand("select * from schaden where sid LIKE " + y[0]);
                while (r.Read())
                {
                    s = new Schaden(r[0].ToString(), r[1].ToString(), r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString());
                }

                EinzelansichtDefekt ed = new EinzelansichtDefekt(s);
                ed.Show();
                db.Close();
                lbAlleDefekte.UnselectAll();
                b = true;
            }
            catch
            {
                db.Close();
                lbAlleDefekte.UnselectAll();
                b = true;
            }
        }

        private void lbInArbeit_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                db.Connect();
                string x = lbInArbeit.SelectedValue.ToString();
                string[] y = x.Split(' ');

                OleDbDataReader r = db.ExecuteCommand("select * from schaden where sid LIKE " + y[0]);
                while (r.Read())
                {
                    s = new Schaden(r[0].ToString(), r[1].ToString(), r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString());
                }

                EinzelansichtDefekt ed = new EinzelansichtDefekt(s);
                ed.Show();
                db.Close();
                lbInArbeit.UnselectAll();
                b = true;
            }
            catch
            {
                db.Close();
                lbAlleDefekte.UnselectAll();
                b = true;
            }
        }

        private void lbAlleDefekte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                lbInArbeit.UnselectAll();
                b = false;
                db.Connect();

                string x = lbAlleDefekte.SelectedItem.ToString();
                string[] y = x.Split(' ');
                getAndDrawPoints(feriend);
                getAndDrawPoints(feriend, y[0]);

                db.Close();
            }
            catch (Exception ex)
            {
                lbAlleDefekte.UnselectAll();
                db.Close();
            }
          
        }

        private void lbInArbeit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                lbAlleDefekte.UnselectAll();
                b = false;
                db.Connect();

                string x = lbInArbeit.SelectedItem.ToString();
                string[] y = x.Split(' ');
                getAndDrawPoints(feriend);
                getAndDrawPoints(feriend, y[0]);

                db.Close();
            }
            catch (Exception ex)
            {
                db.Close();
                lbInArbeit.UnselectAll();

            }
        }

        private void bttnAlleSchaeden_Click(object sender, RoutedEventArgs e)
        {
            AlleSchaeden a = new AlleSchaeden();
            a.Show();
        }
    }
}
