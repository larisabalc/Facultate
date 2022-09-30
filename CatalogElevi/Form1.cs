using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace csharp
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        public static int id;
        public Form1()
        {
            InitializeComponent();
           
        }

        private void textBoxNumeInregistrare_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void buttonInregistrare_Click(object sender, EventArgs e)
        {

            string numeInregistrare = textBoxNumeInregistrare.Text;
            string parolaInregistrare = textBoxParolaInregistrare.Text;
            if (parolaInregistrare.Length <8)
            {
                MyNewMessageBox5 f1= new MyNewMessageBox5();
                this.Hide();
                f1.ShowDialog();
                return;
            }
            con.Open();
            cmd = new SqlCommand("SELECT Id FROM Elevi WHERE Nume=@Nume", con);
            cmd.Parameters.AddWithValue("Nume", numeInregistrare);
            id = Convert.ToInt32(cmd.ExecuteScalar());
            if (id != 0)
            {
                MyNewMessageBox6 f1 = new MyNewMessageBox6();
                this.Hide();
                f1.ShowDialog();
                textBoxNumeInregistrare.Clear();
                textBoxParolaInregistrare.Clear();
                con.Close();
                return;
            }
            cmd = new SqlCommand("INSERT INTO Elevi(Nume,Parola) VALUES (@Nume,@Parola)", con);
            cmd.Parameters.AddWithValue("Nume", numeInregistrare);
            cmd.Parameters.AddWithValue("Parola", parolaInregistrare);
            cmd.ExecuteNonQuery();
            MyNewMessageBox7 f = new MyNewMessageBox7();
            this.Hide();
            f.ShowDialog();
            textBoxNumeInregistrare.Clear();
            textBoxParolaInregistrare.Clear();
            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Larisa\Documents\catalog.mdf;Integrated Security=True;Connect Timeout=30");

        }

        private void buttonConectare_Click(object sender, EventArgs e)
        {
            string numeConectare = textBoxNumeConectare.Text;
            string parolaConectare = textBoxParolaConectare.Text;
            con.Open();
            cmd = new SqlCommand("SELECT Id FROM Elevi WHERE Nume=@nume",con);
            cmd.Parameters.AddWithValue("Nume", numeConectare);
            id = Convert.ToInt32(cmd.ExecuteScalar());
            if (id == 0)
            {
                MyNewMessageBox8 f = new MyNewMessageBox8();
                this.Hide();
                f.ShowDialog();
                textBoxNumeConectare.Clear();
                textBoxParolaConectare.Clear();
                con.Close();
                return;
            }
            cmd = new SqlCommand("SELECT Parola FROM Elevi WHERE Nume=@Nume", con);
            cmd.Parameters.AddWithValue("Nume", numeConectare);
            string parolaDB = Convert.ToString(cmd.ExecuteScalar());
            if (!parolaDB.Equals(parolaConectare))
            {
                MyNewMessageBox9 f = new MyNewMessageBox9();
                this.Hide();
                f.ShowDialog();
                textBoxNumeConectare.Clear();
                textBoxParolaConectare.Clear();
                con.Close();
                return;
            }
            MyNewMessageBox mesaj = new MyNewMessageBox();
            this.Hide();
            mesaj.ShowDialog();
           // this.Show();
            con.Close();
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
