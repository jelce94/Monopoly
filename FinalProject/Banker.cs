using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class Banker
    {
        private Banker(Player pl)
        {
            pl.banker = true;
        }
        private static Banker _instance;
        public static Banker GetInstance(Player pl)
        {
            if (_instance == null)
            {
                _instance = new Banker(pl);
            }
            else
            {
                Console.WriteLine("There already is a banker in this game !");
            }
            return _instance;
        }
    }
}
