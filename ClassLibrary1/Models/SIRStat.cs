using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SIRStat
    {
        public SIRStat(double ds, double di, double dr, double dt)
        {
            Ds = ds;
            Di = di;
            Dr = dr;
            Dt = dt;
        }

        public double Ds { get; set; }
        public double Di { get; set; }
        public double Dr { get; set; }
        public double Dt { get; set; }
    }
}
