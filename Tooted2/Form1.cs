using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.Pdf;
using Image = System.Drawing.Image;


namespace Tooded
{
    public partial class Form1 : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\AppData\Toded.mdf;Integrated Security=True");
        SqlDataAdapter adapter_toode, adapter_kategooria;
        SqlCommand comand;
        public Form1()
        {
            InitializeComponent();
            NaitaAndmed();
            NaitaKategooria();
            dataGridView1.SelectionChanged += changepicturerow;
            button5.Click += Kustuta;

        }
        public void NaitaKategooria()
        {
            connect.Open();
            adapter_kategooria = new SqlDataAdapter("Select KategooriaNimetus From TKategooria", connect);
            DataTable dt_kat = new DataTable();
            adapter_kategooria.Fill(dt_kat);
            foreach (DataRow item in dt_kat.Rows)
            {
                comboBox1.Items.Add(item["KategooriaNimetus"]);
            }
            connect.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            comand = new SqlCommand("Insert INTO TKategooria (KategooriaNimetus)Values (@kat)", connect);
            connect.Open();
            comand.Parameters.AddWithValue("@kat", comboBox1.Text);
            comand.ExecuteNonQuery();
            connect.Close();
            comboBox1.Items.Clear();
            NaitaKategooria();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        int Id = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim() != string.Empty && textBox1.Text.Trim() != string.Empty && textBox3.Text.Trim() != string.Empty && comboBox1.SelectedItem != null)
            {
                try
                {
                    connect.Open();
                    comand = new SqlCommand("INSERT INTO toode (toodeNimetus, Kogus, Hind, Pilt, Kategooriad) VALUES(@toode, @kogus, @hind, @pilt, @kat)", connect);
                    //d = Convert.ToInt32(comand.ExecuteScalar());
                    comand.Parameters.AddWithValue("@toode", textBox2.Text);
                    comand.Parameters.AddWithValue("@kogus", textBox1.Text);
                    comand.Parameters.AddWithValue("@hind", textBox3.Text);
                    comand.Parameters.AddWithValue("@pilt", textBox2.Text + ".jpg");
                    //FileAttributes attributes = File.GetAttributes("@pilt");
                    comand.Parameters.AddWithValue("@kat", comboBox1.SelectedIndex + 1);
                    //select_row; select cell;
                    comand.ExecuteNonQuery();
                    connect.Close();
                    NaitaAndmed();
                }
                catch (Exception)
                {
                    MessageBox.Show("Andmebaasiga viga!");
                }
            }
            else
            {
                MessageBox.Show("sissesta andmed");
            }
        }
        private void changepicturerow(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string imageName = dataGridView1.SelectedRows[0].Cells["Pilt"].Value.ToString();
                string imagepath = Path.Combine(Path.GetFullPath(@"..\..\Pictures"), imageName);

                if (File.Exists(imagepath))
                {
                    Image image = Image.FromFile(imagepath);
                    pictureBox1.ClientSize = new Size(150, 150);
                    pictureBox1.Image = (Image)(new Bitmap(image, pictureBox1.ClientSize));
                }
                else
                {
                    MessageBox.Show($"Pilt '{imageName}' ei ole leitud.");
                }
            }
            else
            {
                pictureBox1.Image = null;
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string val_kat = comboBox1.SelectedItem.ToString();

                connect.Open();
                SqlCommand command = new SqlCommand("Delete From TKategooria Where KategooriaNimetus = @kat", connect);
                command.Parameters.AddWithValue("@kat", val_kat);
                command.ExecuteNonQuery();
                connect.Close();
                comboBox1.Items.Clear();
                NaitaKategooria();

            }
        }


        string kat;
        SaveFileDialog save;
        OpenFileDialog open;


        private void button4_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            open.InitialDirectory = @"C:\Kasutajad\opilane\source\repos\gluhhovtarpv22\Tooded\Pictures";
            open.Multiselect = true;
            open.Filter = "Image Files(*.jpeg;*.bmp;*.png;*.jpg)|*.jpeg;*.bmp;*.png;*.jpg";

            FileInfo open_info = new FileInfo(@"C:\Kasutajad\opilane\source\repos\gluhhovtarpv22\Tooded\Pictures" + open.FileName);
            if (open.ShowDialog() == DialogResult.OK && textBox2.Text != null)
            {
                save = new SaveFileDialog();
                save.InitialDirectory = Path.GetFullPath(@"..\..\Pictures");
                save.FileName = textBox2.Text + Path.GetExtension(open.FileName);
                save.Filter = "Image" + Path.GetExtension(open.FileName) + "|" + Path.GetExtension(open.FileName);
                if (save.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(open.FileName, save.FileName);
                    pictureBox1.Image = Image.FromFile(save.FileName);
                }
            }
            else
            {
                MessageBox.Show("Puudub toode nimetus või oli vajutatud Cancel");
            }
        }

        public void NaitaAndmed()
        {
            connect.Open();
            DataTable dt_toode = new DataTable();
            adapter_toode = new SqlDataAdapter("Select toode.ID, toode.toodeNimetus, toode.Kogus, toode.Hind, toode.Pilt, TKategooria.KategooriaNimetus From toode INNER JOIN TKategooria on toode.Kategooriad=TKategooria.Id", connect);
            adapter_toode.Fill(dt_toode);
            dataGridView1.DataSource = dt_toode;
            DataGridViewComboBoxColumn combobox1 = new DataGridViewComboBoxColumn();
            foreach (DataRow item in dt_toode.Rows)
            {
                if (!combobox1.Items.Contains(item["KategooriaNimetus"]))
                {
                    combobox1.Items.Add(item["KategooriaNimetus"]);
                }
            }
            dataGridView1.Columns.Add(combobox1);
            connect.Close();

        }
        private void lets_go(object sender, DataGridViewCellMouseEventArgs e)
        {
            Id = (int)dataGridView1.Rows[e.RowIndex].Cells["id"].Value;
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            try
            {
                pictureBox1.Image = Image.FromFile(Path.Combine(Path.GetFullPath(@"..\..\Pictures"), dataGridView1.Rows[e.RowIndex].Cells["Pilt"].Value.ToString()));
            }
            catch (Exception)
            {
                MessageBox.Show("Pilt puudub");
            }
            comboBox1.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells[5].Value;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox3.Text != "" && textBox1.Text != "" && pictureBox1.Image != null)
            {
                comand = new SqlCommand("Update toode set ToodeNimetus=@toode,Kogus=@kogus,Hind=@hind, Pilt=@pilt where Id=@id", connect);
                connect.Open();
                comand.Parameters.AddWithValue("@id", Id);
                comand.Parameters.AddWithValue("@toode", textBox3.Text);
                comand.Parameters.AddWithValue("@kogus", textBox1.Text);
                comand.Parameters.AddWithValue("@hind", textBox2.Text.Replace(",", "."));
                string file_pilt = textBox3.Text;
                comand.Parameters.AddWithValue("@pilt", file_pilt);
                comand.ExecuteNonQuery();
                connect.Close();
                NaitaAndmed();

                MessageBox.Show("Andmed uuendatud");
            }
            else
            {
                MessageBox.Show("Viga");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void Kustuta(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {

                DataGridViewRow selectedrow = dataGridView1.SelectedRows[0];
                int selectedRowID = Convert.ToInt32(selectedrow.Cells["Id"].Value);

                try
                {
                    if (connect.State == ConnectionState.Closed)
                    {
                        connect.Open();
                    }
                    string deleteQuery = "DELETE From toode WHERE Id = @id";
                    comand = new SqlCommand(deleteQuery, connect);
                    comand.Parameters.AddWithValue("@id", selectedRowID);
                    comand.ExecuteNonQuery();
                    connect.Close();

                    dataGridView1.Rows.Remove(selectedrow);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problems tekkis kustutamisel: " + ex.Message);
                }
                finally
                {
                    if (connect.State == ConnectionState.Open)
                    {
                        connect.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vali rida DatagridView kustutamiseks");
            }
        }



    }
}