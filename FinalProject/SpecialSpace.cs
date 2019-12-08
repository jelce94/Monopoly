using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class SpecialSpace : Spaces
    {
        public bool chance { get; }
        public bool community_chest { get; }
        public bool go_to_prison { get; }
        public bool tax { get; }
        public bool luxe_tax { get; }
        bool prison_visit { get; }
        bool start { get; }
        bool parking { get; }
        public int money_in_parking { get; set; }
        public SpecialSpace(string name) : base(name)
        {
            chance = false;
            community_chest = false;
            go_to_prison = false;
            tax = false;
            prison_visit = false;
            start = false;
            parking = false;
            money_in_parking = 0;
            if (name == "Start")
            {
                start = true;
            }
            if (name == "Chance")
            {
                chance = true;
            }
            if (name == "Community")
            {
                community_chest = true;
            }
            if (name == "GtP")
            {
                go_to_prison = true;
            }
            if (name == "PrisonVisit")
            {
                prison_visit = true;
            }
            if (name == "Parking")
            {
                parking = true;
            }
            if (name == "Tax")
            {
                tax = true;
            }
            if (name == "LuxeTax")
            {
                luxe_tax = true;
            }
        }
    }
}
