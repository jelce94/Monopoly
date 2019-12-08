using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class Company : Propriety
    {
        public Company(string name, int pri, int ren) : base(name, pri, ren)
        {

        }
        public override string ToString()
        {
            return ("\n This is a public service company"
                    + base.ToString()
                    + "\n Rent with one company owned : 4*dice"
                    + "\n Rent with two companies owned : 10*dice");
        }
    }
}

