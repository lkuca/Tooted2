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
using static System.Net.Mime.MediaTypeNames;

namespace Tooted2
{
   public partial class Form3 : Form
    {
        public Form3()
        {
            this.Height = 800;
            this.Width = 500;
            Form2.NicknameTextBox_KeyDown();
        }
    }
    

    
}
