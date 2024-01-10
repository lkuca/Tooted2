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
        
        
        private Label labelDropCount1;
        private TextBox textBox1;
        private DataTable toode;
        public Form3()
        {
            InitializeComponent();
            panelDropArea.DragEnter += panelDropArea_DragEnter;
            panelDropArea.DragDrop += panelDropArea_DragDrop;
            pictureBox1.DragEnter += pictureBox1_DragEnter;
            pictureBox1.DragDrop += pictureBox1_DragDrop;
            pictureBox1.AllowDrop = true;
            panelDropArea.AllowDrop = true;
            pictureBox1.MouseDown += btnAddDraggablePicture_MouseDown;
            dataGridView2.SelectionChanged += changepicturerow;
            btnAddPicture.Click += btnAddPicture_Click;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            LoadData();
            
            originalPictureBoxLocation = pictureBox1.Location;
            this.Height = 800;
            this.Width = 500;
            
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
            this.panelDropArea.DragDrop += new System.Windows.Forms.DragEventHandler(this.panelDropArea_DragDrop_1);
            this.panelDropArea.DragEnter += new System.Windows.Forms.DragEventHandler(this.panelDropArea_DragEnter_1);
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
            // Form3
            // 
            this.ClientSize = new System.Drawing.Size(318, 597);
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
        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            // Set PictureBox image to null
            pictureBox1.Image = null;

            // Increment drop count
            dropCount++;

            // Update label with drop count (replace 'labelDropCount' with the actual name of your Label)
            labelDropCount1.Text = $"Drop Count: {dropCount}";
        }
        private void panelDropArea_DragEnter(object sender, DragEventArgs e)
        {
           
            
                e.Effect = DragDropEffects.All;
            pictureBox1.Visible = false;
            pictureBox1.Image = null;

            // Increment drop count
            dropCount++;

            // Update label with drop count (replace 'labelDropCount' with the actual name of your Label)
            textBox1.Text = $"Drop Count: {dropCount}";
        }
            
        
        private void panelDropArea_DragDrop(object sender, DragEventArgs e)
        {
            string[] files =(string[])e.Data.GetData(DataFormats.Bitmap);
            // Set PictureBox image to null
            pictureBox1.Visible = false;
            pictureBox1.Image = null;

            // Increment drop count
            dropCount++;

            // Update label with drop count (replace 'labelDropCount' with the actual name of your Label)
            textBox1.Text = $"Drop Count: {dropCount}";
        }

        private void panelDropArea_DragEnter_1(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.Bitmap);
            // Set PictureBox image to null
            pictureBox1.Visible = false;
            pictureBox1.Image = null;

            // Increment drop count
            dropCount++;

            // Update label with drop count (replace 'labelDropCount' with the actual name of your Label)
            textBox1.Text = $"Drop Count: {dropCount}";
        }

        private void panelDropArea_DragDrop_1(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
    }
    

    
}
