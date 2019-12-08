using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    abstract class Spaces
    {
        public string name { get; }
        public Spaces next { get; set; }
        public Spaces(string street_name) => (name, next) = (street_name, null);
        public override string ToString()
        {
            return ("\n Name of space: " + name);
        }
        public bool Equals(Spaces other)
        {
            return this.GetHashCode() == other.GetHashCode();
        }
        public override int GetHashCode()
        {
            return name.GetHashCode();
        }
        public object Shallowcopy()
        {
            return this.MemberwiseClone();
        }
    }
}
