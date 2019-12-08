using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class Street : Propriety
    {
        public string color { get; }
        public int house { get; set; }
        public int hotel { get; set; }
        public int rent1house { get; set; }
        public int rent2house { get; set; }
        public int rent3house { get; set; }
        public int rent4house { get; set; }
        public int renthotel { get; set; }
        public Street(string name, int pri, string col, int ren, int ren1, int ren2, int ren3, int ren4, int renh) : base(name, pri, ren)
        {
            color = col;
            house = 0;
            hotel = 0;
            rent1house = ren1;
            rent2house = ren2;
            rent3house = ren3;
            rent4house = ren4;
            renthotel = renh;
        }
        public int hprice()
        {
            int p = 0;
            if (color == "brown" || color == "sky_blue")
            {
                p = 50;
            }
            if (color == "pink" || color == "orange")
            {
                p = 100;
            }
            if (color == "red" || color == "yellow")
            {
                p = 150;
            }
            if (color == "blue" || color == "green")
            {
                p = 200;
            }
            return p;
        }
        public override string ToString()
        {
            return ("\n This is a street"
                    + base.ToString() +
                    "\n Color : " + color +
                    "\n Number of houses : " + house +
                    "\n Number of hotels : " + hotel
                    + "\n Rent with one house : " + rent1house
                    + "\n Rent with two houses : " + rent2house
                    + "\n Rent with three houses : " + rent3house
                    + "\n Rent with four houses : " + rent4house
                    + "\n Rent with an hotel : " + renthotel);
        }

    }
}