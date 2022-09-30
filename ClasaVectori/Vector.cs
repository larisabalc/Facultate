using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    class Vector
    {
        private Intreg[] vectorul = new Intreg[50];
        int nr = 0;
        public Intreg[] Vectorul
        {
            get { return this.vectorul; }
            set { this.vectorul = value; }
        }
        public int lungime()
        {
            for (int i = 0; i < 50; i++)
                if (vectorul[i] != null)
                    nr++;
            return nr;
        }
        public void sortare(int nr)
        {
            for (int i = 0; i < nr - 1; i++)
                for (int j = i + 1; j < nr; j++)
                {
                    if (Convert.ToInt32(vectorul[i].Numar) > Convert.ToInt32(vectorul[j].Numar))
                    {
                        Intreg aux = vectorul[i];
                        vectorul[i] = vectorul[j];
                        vectorul[j] = aux;
                    }
                }
        }
        public void sortareadesc(int nr)
        {
            for (int i = 0; i < nr - 1; i++)
                for (int j = i + 1; j < nr; j++)
                {
                    if (Convert.ToInt32(vectorul[i].Numar) < Convert.ToInt32(vectorul[j].Numar))
                    {
                        Intreg aux = vectorul[i];
                        vectorul[i] = vectorul[j];
                        vectorul[j] = aux;
                    }
                }
        }
        public static Vector interclasare(Intreg[] vector1, Intreg[] vector2, int nr1, int nr2)
        {
            Vector vector3 = new Vector();
            int nr3 = 0;
            int i = 0;
            int j = 0;
            while (i < nr1 && j < nr2)
            {
                if (Convert.ToInt32(vector1[i].Numar) <= Convert.ToInt32(vector2[j].Numar))
                {
                    vector3.Vectorul[nr3++] = vector1[i];
                    i++;
                }
                else
                {
                    vector3.Vectorul[nr3++] = vector2[j];
                    j++;
                }
            }
            while (i < nr1)
            {
                vector3.Vectorul[nr3++] = vector1[i];
                i++;
            }
            while (j < nr2)
            {
                vector3.Vectorul[nr3++] = vector2[j];
                j++;
            }

            return vector3;
        }
        public void copy(Vector vector)
            {
                 int l = vector.lungime();
                 for(int i=0;i<l-1;i++)
                    {
                     this.vectorul[i] = vector.vectorul[i];
                     }
            }
        public int cautare(Intreg x)
        {
            nr = lungime();
            for (int st = 0, dr = nr - 1; st <= dr;)
            {
                int mij = (st + dr) / 2;
                if (vectorul[mij].Numar == x.Numar)
                    return 1;
                if (vectorul[mij].Numar > x.Numar)
                {
                    st = mij + 1;
                }
                else
                {
                    dr = mij - 1;
                }
            }
            return 0;
        }
    }
}

