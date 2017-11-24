using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feriendorf
{
    public static class IDGenerator
    {
        private static int myId = 0;
        public static int nextId
        {
            get
            {
                myId++;
                return myId;
            }
        }

        public static void init()
        {
            myId = 0;
        }

    }
}
