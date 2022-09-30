using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Algoritm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Algoritm.SelectedItem.Equals("Sortare crescatoare"))
            {
                Numarul.Enabled = true;
                panel1.Visible = false;
                panel2.Visible = false;
                Numarul.Clear();
            }
            else
             if (Algoritm.SelectedItem.Equals("Sortare descrescatoare"))
            {
                Numarul.Enabled = true;
                panel1.Visible = false;
                panel2.Visible = false;
                Numarul.Clear();
            }
            else if (Algoritm.SelectedItem.Equals("Interclasare"))
            {
                panel2.Visible = true;
                panel1.Visible = false;
                Numarul.Enabled = false;
                sirul1.Enabled = true;
                sirul2.Enabled = true;
            }
            else if (Algoritm.SelectedItem.Equals("Cautare binara"))
            {
                panel1.Visible = true;
                panel2.Visible = false;
                Numarul.Enabled = true;
                Numarul.Clear();
            }
        }
        Vector vector = new Vector();
        int nr = 0;
        private void Adauga_Click(object sender, EventArgs e)
        {
            Numarul.Enabled = true;
            try
            {
             Intreg numar = new Intreg(Convert.ToInt32(Numarul.Text));
             vector.Vectorul[nr++] = numar;
            }
            catch
            {
                MessageBox.Show("EROARE");
            }
            Numarul.Clear();
        }
        Vector vector1 = new Vector();
        Vector vector2 = new Vector();
        Vector vector3 = new Vector();
        int nr1 = 0, nr2 = 0, nr3 = 0;
        private void adaugare2_Click(object sender, EventArgs e)
        {

            try
            {
                Intreg numar = new Intreg(Convert.ToInt32(sirul1.Text));
                vector1.Vectorul[nr1++] = numar;
            }
            catch
            {
                MessageBox.Show("EROARE");
            }
            sirul1.Clear();

        }
        private void adugare2_Click(object sender, EventArgs e)
        {
            try
            {
                Intreg numar1 = new Intreg(Convert.ToInt32(sirul2.Text));
                vector2.Vectorul[nr2++] = numar1;
            }
            catch
            {
                MessageBox.Show("EROARE");
            }
            sirul2.Clear();
        }
        Intreg x;
        private void Form1_Load(object sender, EventArgs e)
        {
            vector = new Vector();
            vector1 = new Vector(); 
            vector2 = new Vector();
            vector3 = new Vector(); ;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Afisare_Click(object sender, EventArgs e)
        {
            if (Algoritm.SelectedItem.Equals("Sortare crescatoare"))
            {
                this.panel3.Controls.Clear();
                vector.sortare(nr);
                Label[] a;
                a = new Label[nr];
                Numarul.Enabled = true;
                Numarul.Clear();
                ListBox listBox1 = new ListBox();
                listBox1.Location = new System.Drawing.Point(50, 50);
                listBox1.Size = new System.Drawing.Size(50, 50);
                this.panel3.Controls.Add(listBox1);
                for (int i = 0; i < nr; i++)
                {
                    listBox1.Items.Add(vector.Vectorul[i].ToString());
                }
                int ok = 0;
                if (ok == 1)
                {
                    for (int i = 0; i < nr; i++)
                        this.panel3.Controls.Remove(a[i]);
                }
                if (nr >= 20)
                {
                    MessageBox.Show("EROARE");
                    ok = 1;
                }
                else
                {
                    ok = 0;
                    int L = panel3.Height;
                    int l = L / (nr + 2);
                    for (int i = 0; i < nr*l; i+=l)
                    {
                        a[i / l] = new Label();
                        a[i / l].Name = (i + l).ToString();
                        a[i / l].Width = a[i / l].Height = l - 5;
                        a[i / l].Left = i ;
                        a[i / l].Top = 0;
                        a[i / l].BackColor = Color.Beige;
                        a[i / l].Text = vector.Vectorul[i/l].ToString();
                        this.panel3.Controls.Add(a[i / l]);
                    }
                }
                nr = 0;
            }
            else
           if (Algoritm.SelectedItem.Equals("Sortare descrescatoare"))
            {
                this.panel3.Controls.Clear();
                vector.sortareadesc(nr);
                ListBox listBox1 = new ListBox();
                listBox1.Location = new System.Drawing.Point(50, 50);
                listBox1.Size = new System.Drawing.Size(50, 50);
                this.panel3.Controls.Add(listBox1);
                for (int i = 0; i < nr; i++)
                {
                    listBox1.Items.Add(vector.Vectorul[i].ToString());
                }
                Label[] a;
                a = new Label[nr];
                Numarul.Enabled = true;
                Numarul.Clear();
                int ok = 0;
                if (ok == 0)
                {
                    for (int i = 0; i < nr; i++)
                        this.panel3.Controls.Remove(a[i]);
                }
                if (nr >= 20)
                {
                    MessageBox.Show("EROARE");
                    ok = 1;
                }
                else
                {
                    ok = 0;
                    int L = panel3.Height;
                    int l = L / (nr + 2);
                    for (int i = 0; i < nr*l; i+=l)
                    {
                        a[i / l] = new Label();
                        a[i / l].Name = (i + l).ToString();
                        a[i / l].Width = a[i / l].Height = l - 5;
                        a[i / l].Left = i ;
                        a[i / l].Top = 0;
                        a[i / l].BackColor = Color.Beige;
                        a[i / l].Text = vector.Vectorul[i/l].ToString();
                        this.panel3.Controls.Add(a[i / l]);
                    }
                }
                nr = 0;
            }
            else
            if (Algoritm.SelectedItem.Equals("Interclasare"))
            {
                this.panel3.Controls.Clear();
                vector1.sortare(nr1);
                vector2.sortare(nr2);
                vector3 = Vector.interclasare(vector1.Vectorul, vector2.Vectorul, nr1,nr2);
                nr3 = vector3.lungime();
                ListBox listBox1 = new ListBox();
                listBox1.Location = new System.Drawing.Point(50, 50);
                listBox1.Size = new System.Drawing.Size(50, 50);
                this.panel3.Controls.Add(listBox1);
                for (int i = 0; i < nr1; i++)
                {
                    listBox1.Items.Add(vector1.Vectorul[i].ToString());
                }
                ListBox listBox2 = new ListBox();
                listBox2.Location = new System.Drawing.Point(150, 50);
                listBox2.Size = new System.Drawing.Size(50, 50);
                this.panel3.Controls.Add(listBox2);
                for (int i = 0; i < nr2; i++)
                {
                    listBox2.Items.Add(vector2.Vectorul[i].ToString());
                }
                panel2.Visible = true;
                Numarul.Clear();
                Label[] a;
                int ok = 0;
                a = new Label[nr3];
                if (ok == 1)
                {
                    for (int i = 0; i < nr3; i++)
                        this.panel3.Controls.Remove(a[i]);
                }
                if (nr3 > 20)
                {
                    MessageBox.Show("Eroare");
                    ok = 1;
                    return;
                }
                else
                {
                    int L = panel3.Height;
                    ok = 0;
                    int l = L / (nr3 + 2);
                    for (int i = 0; i < nr3*l; i+=l)
                    {
                        a[i / l] = new Label();
                        a[i / l].Name = (i + l).ToString();
                        a[i / l].Width = a[i / l].Height = l - 5;
                        a[i / l].Left = i;
                        a[i / l].Top = 0;
                        a[i / l].BackColor = Color.Beige;
                        a[i / l].Text = vector3.Vectorul[i/l].ToString();
                        this.panel3.Controls.Add(a[i / l]);
                    }
                }
                nr1 = 0;
                nr2 = 0; 
                nr3 = 0;
            }
            else
                if (Algoritm.SelectedItem.Equals("Cautare binara"))
            {
                nr = 0;
                this.panel3.Controls.Clear();
                vector.sortare(nr);
                nr = vector.lungime();
                panel1.Visible = true;
                Numarul.Clear();
                ListBox listbox1 = new ListBox();
                listbox1.Location = new System.Drawing.Point(50, 50);
                listbox1.Size = new System.Drawing.Size(50, 50);
                this.panel3.Controls.Add(listbox1);
                for (int i = 0; i < nr; i++)
                {
                    listbox1.Items.Add(vector.Vectorul[i].ToString());
                }
                Vector vector4 = new Vector();
                vector4.copy(vector);
                if (  vector4.cautare(x) == 1)
                    MessageBox.Show("Exista in sir");
                else
                    MessageBox.Show("Nu exista in sir");
                panel1.Visible = false;
                nr = 0;

            }
            nr = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            TextBoxx.Enabled = true;
            try
            { 
                x = new Intreg(Convert.ToInt32(TextBoxx.Text));
               
            }
            catch
            {
                MessageBox.Show("EROARE");
            }
            TextBoxx.Clear();
        }

    }
}
