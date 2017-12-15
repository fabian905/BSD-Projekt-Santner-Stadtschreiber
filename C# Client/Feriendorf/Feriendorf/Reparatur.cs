using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feriendorf
{
    public class Reparatur
    {
        public string ReparaturID { get; set; }
        public string Materialaufwand { get; set; }
        public string Repariert { get; set; }
        public string Notiz { get; set; }
        public string Beheber { get; set; }
       

        public Reparatur(string reparaturID, string materialaufwand, string repariert, string notiz, string beheber)
        {
            this.ReparaturID = reparaturID;
            this.Materialaufwand = materialaufwand;
            this.Repariert = repariert;
            this.Notiz = notiz;
            this.Beheber = beheber;
        }
    }
}
