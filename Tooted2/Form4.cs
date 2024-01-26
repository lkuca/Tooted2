using Aspose.Pdf;
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
using Tooded;
using Tooted2;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;



namespace Tooted2
{
    public partial class Form4 : Form
    {
        private PictureBox pictureBox1;

        public Form4()
        {
            InitializeComponent();
            InitializePictureBox();
            pictureBox1.Visible= true;
            pictureBox1.MouseClick += panel1_MouseClick;
            // Assuming your image file is in the project's root folder
            string imagePath = @"..\..\Pictures\forest.jpg";
            string imagegaph = @"..\..\Pictures\rimi.png";
            // Set the background image
            this.BackgroundImage = Image.FromFile(imagePath);
            pictureBox1.BackgroundImage = Image.FromFile(imagegaph);
            
            // Adjust the layout style if needed (optional)
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
        private void InitializePictureBox()
        {
            string imagepath = Path.Combine(Path.GetFullPath(@"..\..\Pictures"), "rimi.png");
            Image image = Image.FromFile(imagepath);
            pictureBox1.ClientSize = new Size(159, 126);
            pictureBox1.Image = (Image)(new Bitmap(image, pictureBox1.ClientSize));
        }




        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(54, 62);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(159, 126);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Form4
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form4";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            Form2 existingForm = new Form2();

            // Show the existing form
            existingForm.Show();
        }
    }
    

}

