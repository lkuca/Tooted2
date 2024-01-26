using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tooded;
using static System.Net.Mime.MediaTypeNames;
using Aspose.Pdf;
using Image = System.Drawing.Image;


namespace Tooted2
{
   public partial class Form3 : Form
    {
        private bool isDragging = false;
        private System.Drawing.Point startPoint;
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\AppData\Toded.mdf;Integrated Security=True");
        SqlDataAdapter adapter_toode;
        private DataGridView dataGridView2;
        private Panel panelDropArea;
        private PictureBox pictureBox1;
        private Button btnAddPicture;
        private System.Drawing.Point originalPictureBoxLocation;
        private int dropCount = 0;




        
        private bool canTrigger = true;
        private Timer cooldownTimer;





        private Label labelDropCount1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label1;
        private Label label2;
        private TextBox textBox3;
        private Button button1;
        private Button button2;
        private DataTable toode;
        public Form3()
        {
            InitializeComponent();
            //panelDropArea.DragEnter += panelDropArea_DragEnter;
            //panelDropArea.DragDrop += panelDropArea_DragDrop;
            
            pictureBox1.AllowDrop = true;
            panelDropArea.AllowDrop = true;
            pictureBox1.MouseDown += btnAddDraggablePicture_MouseDown;
            dataGridView2.SelectionChanged += changepicturerow;
            btnAddPicture.Click += btnAddPicture_Click;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            cooldownTimer = new Timer();
            cooldownTimer.Interval = 3000; // 3000 milliseconds (3 seconds)
            cooldownTimer.Tick += CooldownTimer_Tick;
            LoadData();
            
            originalPictureBoxLocation = pictureBox1.Location;
            this.Height = 800;
            this.Width = 500;
            if (pictureBox1.Location.X == 42 && pictureBox1.Location.Y == 12)
            {
                pictureBox1.Visible = false;
            }

            //Form2.NicknameTextBox_KeyDown();

        }

        private void InitializeComponent()
        {
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.panelDropArea = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnAddPicture = new System.Windows.Forms.Button();
            this.labelDropCount1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(12, 364);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(164, 221);
            this.dataGridView2.TabIndex = 0;
            // 
            // panelDropArea
            // 
            this.panelDropArea.AllowDrop = true;
            this.panelDropArea.BackColor = System.Drawing.Color.Red;
            this.panelDropArea.Location = new System.Drawing.Point(42, 12);
            this.panelDropArea.Name = "panelDropArea";
            this.panelDropArea.Size = new System.Drawing.Size(200, 100);
            this.panelDropArea.TabIndex = 3;
            this.panelDropArea.MouseEnter += new System.EventHandler(this.panelDropArea_MouseEnter);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(191, 364);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 73);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btnAddPicture
            // 
            this.btnAddPicture.Location = new System.Drawing.Point(12, 335);
            this.btnAddPicture.Name = "btnAddPicture";
            this.btnAddPicture.Size = new System.Drawing.Size(75, 23);
            this.btnAddPicture.TabIndex = 2;
            this.btnAddPicture.Text = "asuta toode";
            this.btnAddPicture.UseVisualStyleBackColor = true;
            // 
            // labelDropCount1
            // 
            this.labelDropCount1.AutoSize = true;
            this.labelDropCount1.Location = new System.Drawing.Point(42, 119);
            this.labelDropCount1.Name = "labelDropCount1";
            this.labelDropCount1.Size = new System.Drawing.Size(34, 13);
            this.labelDropCount1.TabIndex = 5;
            this.labelDropCount1.Text = "count";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(83, 119);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(83, 146);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "prize";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Total prize";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(83, 176);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(94, 335);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Osta";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 306);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "katkesta";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form3
            // 
            this.ClientSize = new System.Drawing.Size(318, 597);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panelDropArea);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelDropCount1);
            this.Controls.Add(this.btnAddPicture);
            this.Controls.Add(this.dataGridView2);
            this.Name = "Form3";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }







        private void LoadData()
        {
            try
            {
                connect.Open();

                

                adapter_toode = new SqlDataAdapter("SELECT [toodeNimetus], [Pilt] FROM [dbo].[toode]", connect);
                toode = new DataTable();
                adapter_toode.Fill(toode);

                
                dataGridView2.DataSource = toode;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                connect.Close();
            }
        }
        private void changepicturerow(object sender, EventArgs e)
        {
            originalPictureBoxLocation = pictureBox1.Location;
            if (dataGridView2.SelectedRows.Count > 0)
            {
                string imageName = dataGridView2.SelectedRows[0].Cells["Pilt"].Value.ToString();
                string imagepath = Path.Combine(Path.GetFullPath(@"..\..\Pictures"), imageName);

                if (File.Exists(imagepath))
                {
                    Image image = Image.FromFile(imagepath);
                    
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
            pictureBox1.Location = new System.Drawing.Point(182, 364);

            // pictureBox1.MouseDown += btnAddDraggablePicture_MouseDown;
        }
        private void btnAddPicture_Click(object sender, EventArgs e)
        {
            changepicturerow(sender, e);
            pictureBox1.MouseDown += btnAddDraggablePicture_MouseDown;
        }
        private void btnAddDraggablePicture_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            startPoint = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                pictureBox1.Left += e.X - startPoint.X;
                pictureBox1.Top += e.Y - startPoint.Y;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
       

        private void panelDropArea_MouseEnter(object sender, EventArgs e)
        {
            if (canTrigger)
            {
                pictureBox1.Visible = true;
                dropCount++;
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"..\..\soundeffects\BeepSoundEffect.wav");
                player.Play();
               
                textBox1.Text = $"{dropCount}";
                Random random = new Random();
                double prize = random.Next(1, 1001) / 100.0; 

               
                textBox2.Text = $"{prize.ToString("F2")}";
             
                cooldownTimer.Start();
                canTrigger = false;
            }
            if (double.TryParse(textBox1.Text, out double number1) && double.TryParse(textBox2.Text, out double number2))
            {
              
                double result = number1 * number2;

                
                textBox3.Text = $"${result.ToString("F2")}"; 
            }
            else
            {
               
                textBox3.Text = "Invalid input";
            }
        }
        private void CooldownTimer_Tick(object sender, EventArgs e)
        {
            
            cooldownTimer.Stop();
            canTrigger = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" & textBox2.Text != "" & textBox3.Text != "" &&  Text != "0" & textBox2.Text != "0" & textBox3.Text != "0")
            {
                textBox1.Text = "0";
                textBox2.Text = "0";
                textBox3.Text = "0";
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"..\..\soundeffects\kassa.wav");
                player.Play();
                dropCount = 0;
            }
            else { MessageBox.Show("Osta midagi!!!"); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" & textBox2.Text != "" & textBox3.Text != "" && Text != "0" & textBox2.Text != "0" & textBox3.Text != "0")
            {
                textBox1.Text = "0";
                textBox2.Text = "0";
                textBox3.Text = "0";
                
                dropCount = 0;
            }
            else { MessageBox.Show("Osta midagi!!!"); System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"..\..\soundeffects\videoplayback (1).wav");
                player.Play();
            }
        }
    }
    

    
}
