using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    class Intreg
    {
        private int numar;
        public Intreg(int nr)
        {
            this.numar=nr;
        }
        public override string ToString()
        {
            return Convert.ToString(numar);
        }
        public int Numar
        {
            get { return this.numar; }
            set { this.numar = value; }
        }

    }
}
