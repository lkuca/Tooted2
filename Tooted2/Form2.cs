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
using static System.Net.Mime.MediaTypeNames;

namespace Tooded
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            List<string> logins = new List<string>();
            List<string> passwords = new List<string>();
            string[] log = { "User1", "User2" };
            string[] parool = { "s1mple", "juppi" };
            this.Height = 400;
            this.Width = 500;
            Button btn1 = new Button();
            btn1.Text = "registreerimine";

            btn1.Click += registerimine_nimi;
            btn1.Visible = true;
            btn1.Location = new Point(240, 190);
            this.Controls.Add(btn1);
            Button btn2 = new Button();
            btn2.Text = "autoriseerimine";

            btn2.Click += autoreserimine;
            btn2.Visible = true;
            btn2.Location = new Point(240, btn1.Bottom);
            this.Controls.Add(btn2);
            Button btn3 = new Button();
            btn3.Text = "muuta";

            btn3.Click += muuta;
            btn3.Visible = true;
            btn3.Location = new Point(240, btn2.Bottom);
            this.Controls.Add(btn3);
            Button btn4 = new Button();
            btn4.Text = "Unustasidparooli";
            btn4.Click += Unustasidparooli;
            btn4.Visible = true;
            btn4.Location = new Point(240, btn3.Bottom);
            this.Controls.Add(btn4);
            Button btn5 = new Button();
            btn5.Text = "Logivälja";

            btn5.Click += Logivälja;
            btn5.Visible = true;
            btn5.Location = new Point(240, btn4.Bottom);
            this.Controls.Add(btn5);




        }
        private void tekst_to_lbl(object sender, EventArgs e)
        {
            Console.WriteLine("Enter a text:");
            string text = Console.ReadLine();
            Console.WriteLine("Entered text: " + text);
        }
        //public static bool salsasona(int k, out string password)
        //{

        //    StringBuilder sb = new StringBuilder();
        //    Random random = new Random();

        //    for (int i = 0; i < k; i++)
        //    {
        //        char t = (char)random.Next('A', 'Z' + 1);
        //        int num = random.Next(0, 10);
        //        char sym = random.//Char(new[] { '*', '-', '.', '!', '_' });

        //        char[] t_num = { t, num.ToString()[0], sym };
        //        sb.Append(t_num[random.Next(0, 3)]);
        //    }

        //    password = sb.ToString();
        //    return true;
        //}

        private void registerimine_nimi(object sender, EventArgs e)
        {
            Form aken1 = new Form();
            aken1.Size = new System.Drawing.Size(400, 500);

            Label label_nickname = new Label();
            TextBox nicknameTextBox = new TextBox();

            label_nickname.Text = "Enter your nickname:";
            label_nickname.BackColor = System.Drawing.Color.Silver;


            label_nickname.Height = 22;
            label_nickname.Width = 146;
            label_nickname.Location = new Point(240, 190);

            nicknameTextBox.ForeColor = System.Drawing.Color.Blue;
            nicknameTextBox.Text = "Enter your nickname:";
            nicknameTextBox.BackColor = System.Drawing.Color.LightBlue;
            nicknameTextBox.Height = 1;
            nicknameTextBox.Width = 146;

            nicknameTextBox.TextAlign = HorizontalAlignment.Center;
            nicknameTextBox.Location = new Point(244, 217);
            aken1.Controls.Add(label_nickname);
            aken1.Controls.Add(nicknameTextBox);
            aken1.Show();
        }

        private void autoreserimine(object sender, EventArgs e)
        {
            Form aken2 = new Form();
            aken2.Size = new System.Drawing.Size(400, 500);
            // Code for autoreserimine button click event
        }

        private void muuta(object sender, EventArgs e)
        {
            Form aken3 = new Form();
            aken3.Size = new System.Drawing.Size(400, 500);
            // Code for muuta button click event
        }

        private void Unustasidparooli(object sender, EventArgs e)
        {
            Form aken4 = new Form();
            aken4.Size = new System.Drawing.Size(400, 500);
            // Code for Unustasidparooli button click event
        }

        private void Logivälja(object sender, EventArgs e)
        {
            // Code for Logivälja button click event
        }
    }

}
