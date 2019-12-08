using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class Station : Propriety
    {
        public int rent2stations { get; set; }
        public int rent3stations { get; set; }
        public int rent4stations { get; set; }

        public Station(string name, int pri, int ren, int ren2, int ren3, int ren4) : base(name, pri, ren)
        {
            rent2stations = ren2;
            rent3stations = ren3;
            rent4stations = ren4;
        }
        public override string ToString()
        {
            return ("\n This is a train station"
                    + base.ToString()
                    + "\n Rent if 2 stations are owned : " + rent2stations
                    + "\n Rent if 3 stations are owned : " + rent3stations
                    + "\n Rent if 4 stations are owned : " + rent4stations);
        }
    }
}