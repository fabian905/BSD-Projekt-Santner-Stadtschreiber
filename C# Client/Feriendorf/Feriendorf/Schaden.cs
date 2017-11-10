using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feriendorf
{
    public class Schaden
    {
        public string SchadenID { get; set; }
        public string Bezeichnung { get; set; }
        public string Augetreten { get; set; }
        public string Status { get; set; }
        public string MelderID { get; set; }
        public string HausID { get; set; }

        public Schaden (string schadenID, string bezeichnung, string aufgetreten, string status, string meldeid , string hausid)
        {
            this.SchadenID = schadenID;
            this.Bezeichnung = bezeichnung;
            this.Augetreten = aufgetreten;
            this.Status = status;
            this.MelderID = meldeid;
            this.HausID = hausid;
        }
    }
}
