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
    public partial class Catalog : Form
    {

        SqlConnection con;
        SqlCommand cmd;
        public Catalog()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Larisa\Documents\catalog.mdf;Integrated Security=True;Connect Timeout=30");
        }

        private void adaugaNotaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.Hide();
            tabControl1.Show();
            tabPageAdaugareNota.Show();            
        }

        private void buttonAdaugaNota_Click(object sender, EventArgs e)
        {
            string numeMaterie = comboBoxNumeMaterie.Text;
            int nota = 0;

            if (Convert.ToInt32(textBoxNotaAdauga.Text) >= 1 && Convert.ToInt32(textBoxNotaAdauga.Text) <= 10 )
                nota = Convert.ToInt32(textBoxNotaAdauga.Text);
            else
            {
                //Nota trebuie sa se afle in intervalul [1,10]!
                MyNewMessageBox10 mesaj = new MyNewMessageBox10();
                this.Hide();
                mesaj.ShowDialog();
                this.Show();
                return;
            }

            con.Open();

            cmd = new SqlCommand("INSERT INTO Note(" + numeMaterie + ", IdElevi) VALUES (@nota, @id)", con);
            cmd.Parameters.AddWithValue("nota", nota);
            cmd.Parameters.AddWithValue("id", Form1.id);
            if(numeMaterie!=null&&numeMaterie!="")
            {
                 cmd.ExecuteNonQuery();
                //Nota a fost adaugata cu succes!
                MyNewMessageBox1 f = new MyNewMessageBox1();
                this.Hide();
                f.ShowDialog();
                this.Show();

                textBoxNotaAdauga.Clear();
            }
            else
            {
                MyNewMessageBox4 mesajul= new MyNewMessageBox4();
                this.Hide();
                mesajul.ShowDialog();
                this.Show();

            }
           

            
            con.Close();
        }
        
        private void deteleAllItemsInDataGridView()
        {
            dataGridView1.Rows.Clear();
        }

        private void deteleAllItemsInDataGridView1()
        {
            dataGridView2.Rows.Clear();
        }

        private void deteleAllItemsInDataGridView2()
        {
            dataGridView3.Rows.Clear();
        }

        private void deteleAllItemsInDataGridView3()
        {
            dataGridView4.Rows.Clear();
        }

        string[] vectorMaterii = {"Matematica","Romana","Informatica","Engleza","Istorie","Chimie","Biologie","Geografie","Fizica"}; 

        private void tabPageSituatie_Click(object sender, EventArgs e)
        {
            tabControl1.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            con.Open();

            int rowIndex = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[rowIndex];
            DataGridViewButtonColumn f = new DataGridViewButtonColumn();
            int idul = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells[1].Value);

            cmd = new SqlCommand("DELETE FROM Note WHERE id=@id", con);
            cmd.Parameters.AddWithValue("id", idul);
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            foreach (DataGridViewCell oneCell in dataGridView1.SelectedCells)
            {
                if (oneCell.Selected)
                    dataGridView1.Rows.RemoveAt(oneCell.RowIndex);
            }

            con.Close();
        }

        int[] medii=new int[10];
        double s=0;

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPageSituatie)
            {

                con.Open();
                deteleAllItemsInDataGridView();
                cmd = new SqlCommand("SELECT * FROM  Note WHERE IdElevi=@Id", con);
                cmd.Parameters.AddWithValue("Id", Form1.id);
                using (SqlDataReader date = cmd.ExecuteReader())
                {

                    while (date.Read())
                    {
                        for (int i = 0; i < vectorMaterii.Length; i++)
                            if (date[vectorMaterii[i]].ToString() != "")
                            {
                                dataGridView1.Rows.Add(null, date["Id"].ToString(), date[vectorMaterii[i]].ToString(), vectorMaterii[i]);

                            }
                    }
                }

                deteleAllItemsInDataGridView3();

               cmd = new SqlCommand("SELECT TezaMatematica FROM Teze WHERE IdElev=@id AND TezaMatematica IS NOT NULL", con);
                cmd.Parameters.AddWithValue("id", Form1.id);
                int teza = Convert.ToInt32(cmd.ExecuteScalar());
                dataGridView4.Rows.Add("Matematica", teza);

                cmd = new SqlCommand("SELECT TezaRomana FROM Teze WHERE IdElev=@id AND TezaRomana IS NOT NULL", con);
                cmd.Parameters.AddWithValue("id", Form1.id);
                int teza1 = Convert.ToInt32(cmd.ExecuteScalar());
                dataGridView4.Rows.Add("Romana", teza1); 

                cmd = new SqlCommand("SELECT TezaInformatica FROM Teze WHERE IdElev=@id AND TezaInformatica IS NOT NULL", con);
                cmd.Parameters.AddWithValue("id", Form1.id);
                int teza2 = Convert.ToInt32(cmd.ExecuteScalar());
                dataGridView4.Rows.Add("Informatica", teza2);

                cmd = new SqlCommand("SELECT TezaChimie FROM Teze WHERE IdElev=@id AND TezaChimie IS NOT NULL", con);
                cmd.Parameters.AddWithValue("id", Form1.id);
                int teza3 = Convert.ToInt32(cmd.ExecuteScalar());
                dataGridView4.Rows.Add("Chimie", teza3);

                cmd = new SqlCommand("SELECT TezaFizica FROM Teze WHERE IdElev=@id AND TezaFizica IS NOT NULL", con);
                cmd.Parameters.AddWithValue("id", Form1.id);
                int teza4 = Convert.ToInt32(cmd.ExecuteScalar());
                dataGridView4.Rows.Add("Fizica", teza4);

                cmd = new SqlCommand("SELECT TezaBiologie FROM Teze WHERE IdElev=@id AND TezaBiologie IS NOT NULL", con);
                cmd.Parameters.AddWithValue("id", Form1.id);
                int teza5 = Convert.ToInt32(cmd.ExecuteScalar());
                dataGridView4.Rows.Add("Biologie", teza5);

                con.Close();

            }
            if (tabControl1.SelectedTab == tabPageVeziAbsente)
            {
                functie();
            }
            if (tabControl1.SelectedTab == tabPageVeziMedii)
            {
                con.Open();
                for (int i = 0; i < vectorMaterii.Length; i++)
                    medii[i] = 0;
                deteleAllItemsInDataGridView1();

                for (int i = 0; i < vectorMaterii.Length; i++)
                {

                    cmd = new SqlCommand("SELECT " + vectorMaterii[i] + " FROM Note WHERE IdElevi=@id AND " + vectorMaterii[i] + " IS NOT NULL", con);
                    cmd.Parameters.AddWithValue("id", Form1.id);
                    using (SqlDataReader date = cmd.ExecuteReader())
                    {
                        int nota = 0, nrnote = 0;
                        while (date.Read())
                        {

                            nota += Convert.ToInt32(date[vectorMaterii[i]]);
                            nrnote++;

                        }

                        if (nrnote < 2)
                        {
                            dataGridView2.Rows.Add(vectorMaterii[i].ToString(), "Nu ai suficiente note!");
                            continue;
                        }

                        if ((nota / nrnote) - (int)(nota / nrnote) >= 0.5)
                        {
                            dataGridView2.Rows.Add(vectorMaterii[i].ToString(), Math.Ceiling((double)(nota / nrnote)));
                            medii[i] = Convert.ToInt32(Math.Ceiling((double)(nota / nrnote)));
                        }
                        else
                        {
                            dataGridView2.Rows.Add(vectorMaterii[i].ToString(), Math.Floor((double)(nota / nrnote)));
                            medii[i] = Convert.ToInt32(Math.Floor((double)(nota / nrnote)));

                        }
                    }
                }
                for (int i = 0; i < vectorMaterii.Length; i++)
                {
                    if (vectorMaterii[i].ToString().Equals("Matematica"))
                    { 
                        cmd = new SqlCommand("SELECT TezaMatematica FROM Teze WHERE IdElev=@id AND TezaMatematica IS NOT NULL", con);
                        cmd.Parameters.AddWithValue("id", Form1.id);
                        int teza = Convert.ToInt32(cmd.ExecuteScalar());
                        if (teza != 0)
                            medii[0] = (medii[0] * 3 + teza) / 4;
                    
                    }
                    else if (vectorMaterii[i].ToString().Equals("Romana"))
                    {
                        cmd = new SqlCommand("SELECT TezaRomana FROM Teze WHERE IdElev=@id AND TezaRomana IS NOT NULL", con);
                        cmd.Parameters.AddWithValue("id", Form1.id);
                        int teza = Convert.ToInt32(cmd.ExecuteScalar());
                        if (teza != 0)
                            medii[1] = (medii[1] * 3 + teza) / 4;
                       

                    }
                    else if (vectorMaterii[i].ToString().Equals("Informatica"))
                    {

                        cmd = new SqlCommand("SELECT TezaInformatica FROM Teze WHERE IdElev=@id AND TezaInformatica IS NOT NULL", con);
                        cmd.Parameters.AddWithValue("id", Form1.id);
                        int teza = Convert.ToInt32(cmd.ExecuteScalar());
                        if (teza != 0)
                            medii[2] = (medii[2] * 3 + teza) / 4;
                        
                    }
                    else if (vectorMaterii[i].ToString().Equals("Chimie"))
                    {

                        cmd = new SqlCommand("SELECT TezaChimie FROM Teze WHERE IdElev=@id AND TezaChimie IS NOT NULL", con);
                        cmd.Parameters.AddWithValue("id", Form1.id);
                        int teza = Convert.ToInt32(cmd.ExecuteScalar());
                        if (teza != 0)
                            medii[5] = (medii[5] * 3 + teza) / 4;
          
                    }
                    else
                     if (vectorMaterii[i].ToString().Equals("Biologie"))
                    {

                        cmd = new SqlCommand("SELECT TezaBiologie FROM Teze WHERE IdElev=@id AND TezaBiologie IS NOT NULL", con);
                        cmd.Parameters.AddWithValue("id", Form1.id);
                        int teza = Convert.ToInt32(cmd.ExecuteScalar());
                        if (teza != 0)
                            medii[6] = (medii[6] * 3 + teza) / 4;
                      
                    }
                    else if (vectorMaterii[i].ToString().Equals("Fizica"))
                    {

                        cmd = new SqlCommand("SELECT TezaFizica FROM Teze WHERE IdElev=@id AND TezaFizica IS NOT NULL", con);
                        cmd.Parameters.AddWithValue("id", Form1.id);
                        int teza = Convert.ToInt32(cmd.ExecuteScalar());
                        if (teza != 0)
                            medii[8] = (medii[8] * 3 + teza) / 4;
                     
                    }

                }

                deteleAllItemsInDataGridView2();
                s = 0;
                for (int i = 0; i < vectorMaterii.Length; i++)
                {
                    dataGridView3.Rows.Add(vectorMaterii[i].ToString(), medii[i]);
                    s += medii[i];
                }
                
                dataGridView3.Rows.Add("MEDIA GENERALA", (s / Convert.ToDouble(vectorMaterii.Length)));
             
                con.Close();
            }
        }

        private void catalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.Hide();
            tabPageSituatie.Hide();
            tabPageAdaugareNota.Hide();
         
        }

        private void veziSituatieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.Hide();
            tabControl1.Show();
            tabPageSituatie.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        
        private void buttonAdaugaTeza_Click(object sender, EventArgs e)
        {
            con.Open();

            string numeMaterie = comboBoxMaterieTeza.Text;
            int teza = Convert.ToInt32(textBoxAdaugareTeza.Text);
            if (Convert.ToInt32(textBoxAdaugareTeza.Text) >= 1 && Convert.ToInt32(textBoxAdaugareTeza.Text) <= 10)
            {
                teza = Convert.ToInt32(textBoxAdaugareTeza.Text);
            }
            else
            {
                //Nota trebuie sa se afle in intervalul [1,10]!
                MyNewMessageBox10 mesaj = new MyNewMessageBox10();
                this.Hide();
                mesaj.ShowDialog();
                this.Show();
                return;
                
            }
            if(numeMaterie==null||numeMaterie=="")
            {
                MyNewMessageBox4 mesajul1 = new MyNewMessageBox4();
                this.Hide();
                mesajul1.ShowDialog();
                this.Show();
            }
            if (numeMaterie.Equals("Matematica"))
            {
              
                cmd = new SqlCommand("SELECT TezaMatematica FROM Teze WHERE idElev=@id AND TezaMatematica IS NOT NULL", con);
                cmd.Parameters.AddWithValue("id", Form1.id);
                int teza1 = Convert.ToInt32(cmd.ExecuteScalar());
                if (teza1 != 0)
                {
                    //Poti adauga doar o nota in teza!
                    MyNewMessageBox3 mesaj1 = new MyNewMessageBox3();
                    this.Hide();
                    mesaj1.ShowDialog();
                    this.Show();
                    return;
                }

                cmd = new SqlCommand("INSERT INTO Teze (TezaMatematica,IdElev) VALUES (@notateza,@id)", con);
                cmd.Parameters.AddWithValue("notateza", teza);
                cmd.Parameters.AddWithValue("id", Form1.id);
                cmd.ExecuteNonQuery();

                //Nota adaugata cu succes!
                MyNewMessageBox1 mesaj= new MyNewMessageBox1();
                this.Hide();
                mesaj.ShowDialog();
                this.Show();
                textBoxAdaugareTeza.Clear();
           
               

            }
            else if (numeMaterie.Equals("Romana"))
            {
                cmd = new SqlCommand("SELECT TezaRomana FROM Teze WHERE idElev=@id AND TezaRomana IS NOT NULL", con);
                cmd.Parameters.AddWithValue("id", Form1.id);
                int teza1 = Convert.ToInt32(cmd.ExecuteScalar());
                if (teza1 != 0)
                {
                    //Poti adauga doar o nota in teza!
                    MyNewMessageBox3 mesaj1 = new MyNewMessageBox3();
                    this.Hide();
                    mesaj1.ShowDialog();
                    this.Show();
                    return;
                }

                cmd = new SqlCommand("INSERT INTO Teze (TezaRomana,IdElev) VALUES (@notateza,@id)", con);
                cmd.Parameters.AddWithValue("notateza", teza);
                cmd.Parameters.AddWithValue("id", Form1.id);
                cmd.ExecuteNonQuery();

                //Nota adaugata cu succes!
                MyNewMessageBox1 mesaj = new MyNewMessageBox1();
                this.Hide();
                mesaj.ShowDialog();
                this.Show();

                textBoxAdaugareTeza.Clear();
               

            }
            else if (numeMaterie.Equals("Informatica"))
            {
                cmd = new SqlCommand("SELECT TezaInformatica FROM Teze WHERE idElev=@id AND TezaInformatica IS NOT NULL", con);
                cmd.Parameters.AddWithValue("id", Form1.id);
                int teza1 = Convert.ToInt32(cmd.ExecuteScalar());
                if (teza1 != 0)
                {
                    //Poti adauga doar o nota in teza!
                    MyNewMessageBox3 mesaj1 = new MyNewMessageBox3();
                    this.Hide();
                    mesaj1.ShowDialog();
                    this.Show();
                    return;
                }

                cmd = new SqlCommand("INSERT INTO Teze (TezaInformatica,IdElev) VALUES (@notateza,@id)", con);
                cmd.Parameters.AddWithValue("notateza", teza);
                cmd.Parameters.AddWithValue("id", Form1.id);
                cmd.ExecuteNonQuery();

                //Nota adaugata cu succes!
                MyNewMessageBox1 mesaj = new MyNewMessageBox1();
                this.Hide();
                mesaj.ShowDialog();
                this.Show();
                textBoxAdaugareTeza.Clear();
               
            }
            else if (numeMaterie.Equals("Chimie"))
            {
                cmd = new SqlCommand("SELECT TezaFizica FROM Teze WHERE idElev=@id AND TezaFizica IS NOT NULL", con);
                cmd.Parameters.AddWithValue("id", Form1.id);
                int teza2 = Convert.ToInt32(cmd.ExecuteScalar());
                cmd = new SqlCommand("SELECT TezaBiologie FROM Teze WHERE idElev=@id AND TezaBiologie IS NOT NULL", con);
                cmd.Parameters.AddWithValue("id", Form1.id);
                int teza3 = Convert.ToInt32(cmd.ExecuteScalar());
                if (teza2 == 0 && teza3 == 0)
                {
                    cmd = new SqlCommand("SELECT TezaChimie FROM Teze WHERE idElev=@id AND TezaChimie IS NOT NULL", con);
                    cmd.Parameters.AddWithValue("id", Form1.id);
                    int teza1 = Convert.ToInt32(cmd.ExecuteScalar());
                    if (teza1 != 0)
                    {
                        //Poti adauga doar o nota in teza!
                        MyNewMessageBox3 mesaj1 = new MyNewMessageBox3();
                        this.Hide();
                        mesaj1.ShowDialog();
                        this.Show();
                        return;
                    }
                   
                    cmd = new SqlCommand("INSERT INTO Teze (TezaChimie,IdElev) VALUES (@notateza,@id)", con);
                    cmd.Parameters.AddWithValue("notateza", teza);
                    cmd.Parameters.AddWithValue("id", Form1.id);
                    cmd.ExecuteNonQuery();

                    //Nota adaugata cu succes!
                    MyNewMessageBox1 mesaj = new MyNewMessageBox1();
                    this.Hide();
                    mesaj.ShowDialog();
                    this.Show();
                    textBoxAdaugareTeza.Clear();
                    
                }
                else 
                {   //Poti da teza doar la o singura a patra materie!
                    MyNewMessageBox11 mesaj = new MyNewMessageBox11();
                    this.Hide();
                    mesaj.ShowDialog();
                    this.Show();
                   
                }
            }
            else if (numeMaterie.Equals("Biologie"))
            {
                cmd = new SqlCommand("SELECT TezaFizica FROM Teze WHERE idElev=@id AND TezaFizica IS NOT NULL", con);
                cmd.Parameters.AddWithValue("id", Form1.id);
                int teza2 = Convert.ToInt32(cmd.ExecuteScalar());
                cmd = new SqlCommand("SELECT TezaChimie FROM Teze WHERE idElev=@id AND TezaChimie IS NOT NULL", con);
                cmd.Parameters.AddWithValue("id", Form1.id);
                int teza3 = Convert.ToInt32(cmd.ExecuteScalar());
                if (teza2 == 0 && teza3 == 0)
                {
                    cmd = new SqlCommand("SELECT TezaBiologie FROM Teze WHERE idElev=@id AND TezaBiologie IS NOT NULL", con);
                    cmd.Parameters.AddWithValue("id", Form1.id);
                    int teza1 = Convert.ToInt32(cmd.ExecuteScalar());
                    if (teza1 != 0)
                    {
                        //Poti adauga doar o nota in teza!
                        MyNewMessageBox3 mesaj1 = new MyNewMessageBox3();
                        this.Hide();
                        mesaj1.ShowDialog();
                        this.Show();
                        return;
                    }
                   
                    cmd = new SqlCommand("INSERT INTO Teze (TezaBiologie,IdElev) VALUES (@notateza,@id)", con);
                    cmd.Parameters.AddWithValue("notateza", teza);
                    cmd.Parameters.AddWithValue("id", Form1.id);
                    cmd.ExecuteNonQuery();

                    //Nota adaugata cu succes!
                    MyNewMessageBox1 mesaj = new MyNewMessageBox1();
                    this.Hide();
                    mesaj.ShowDialog();
                    this.Show();
                    textBoxAdaugareTeza.Clear();
                   
                }
                else
                {
                    //Poti da teza doar la o singura a patra materie!
                    MyNewMessageBox11 mesaj = new MyNewMessageBox11();
                    this.Hide();
                    mesaj.ShowDialog();
                    this.Show();
                }
            }
            else if (numeMaterie.Equals("Fizica"))
            {
                cmd = new SqlCommand("SELECT TezaChimie FROM Teze WHERE idElev=@id AND TezaChimie IS NOT NULL", con);
                cmd.Parameters.AddWithValue("id", Form1.id);
                int teza2 = Convert.ToInt32(cmd.ExecuteScalar());
                cmd = new SqlCommand("SELECT TezaBiologie FROM Teze WHERE idElev=@id AND TezaBiologie IS NOT NULL", con);
                cmd.Parameters.AddWithValue("id", Form1.id);
                int teza3 = Convert.ToInt32(cmd.ExecuteScalar());
                if (teza2 == 0 && teza3 == 0)
                {
                    cmd = new SqlCommand("SELECT TezaFizica FROM Teze WHERE idElev=@id AND TezaFizica IS NOT NULL", con);
                    cmd.Parameters.AddWithValue("id", Form1.id);
                    int teza1 = Convert.ToInt32(cmd.ExecuteScalar());
                    if (teza1 != 0)
                    {
                        //Poti adauga doar o nota in teza!
                        MyNewMessageBox3 mesaj1 = new MyNewMessageBox3();
                        this.Hide();
                        mesaj1.ShowDialog();
                        this.Show();
                        return;
                    }
                    
                    cmd = new SqlCommand("INSERT INTO Teze (TezaFizica,IdElev) VALUES (@notateza,@id)", con);
                    cmd.Parameters.AddWithValue("notateza", teza);
                    cmd.Parameters.AddWithValue("id", Form1.id);
                    cmd.ExecuteNonQuery();

                    //Nota adaugata cu succes!
                    MyNewMessageBox1 mesaj = new MyNewMessageBox1();
                    this.Hide();
                    mesaj.ShowDialog();
                    this.Show();
                    textBoxAdaugareTeza.Clear();
                   

                }
                else
                {
                    //Poti da teza doar la o singura a patra materie!
                    MyNewMessageBox11 mesaj = new MyNewMessageBox11();
                    this.Hide();
                    mesaj.ShowDialog();
                    this.Show();
                }
               
            }
            

            con.Close();
        }

        private void tabPageAdaugareNota_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                con.Open();
                cmd= new SqlCommand("DELETE FROM ABSENTE WHERE id=@id",con);
                cmd.Parameters.AddWithValue("id", senderGrid.Rows[e.RowIndex].Cells["Id"].Value);
                cmd.ExecuteNonQuery();
                cmd.Dispose(); 
                con.Close();
                functie();
               

            }
        }
        private void functie()
        {
            dataGridView5.Columns.Clear();
            con.Open();
            cmd = new SqlCommand("SELECT Materie,Data,Id FROM Absente WHERE IdElev=@id ORDER BY Materie", con);
            cmd.Parameters.AddWithValue("id", Form1.id);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            DataGridViewButtonColumn butonSterge = new DataGridViewButtonColumn();
            butonSterge.HeaderText = "STERGE";
            butonSterge.UseColumnTextForButtonValue = true;
            butonSterge.Name = "btnSterge";
            butonSterge.Text = "STERGE";
            dataGridView5.DataSource = dataTable;
            dataGridView5.Columns.Add(butonSterge);
            con.Close();
        }
        private void buttonAdaugaAbsenta_Click(object sender, EventArgs e)
        {
            con.Open();
            string numeMaterie = comboBoxNumeMaterieAbsenta.Text;
            cmd = new SqlCommand("INSERT INTO Absente(Materie,IdElev,Data) VALUES (@materie,@idelev,@data)",con);
            cmd.Parameters.AddWithValue("materie",numeMaterie);
            cmd.Parameters.AddWithValue("idelev",Form1.id);
            cmd.Parameters.AddWithValue("data",DateTime.Now);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
           

            con.Close();functie();
        }

        private void Catalog_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
   
        }

         
        
    

