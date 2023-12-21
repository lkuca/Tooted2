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
using Tooted2;
using static System.Net.Mime.MediaTypeNames;

namespace Tooded
{
    public partial class Form2 : Form
    {
        

        Form3 form3;
        static List<string> logins = new List<string>();
        static List<string> passwords = new List<string>();
        TextBox nicknameTextBox;
        public Form2()
        {

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
            
            Button btn5 = new Button();
            btn5.Text = "Logivälja";

            btn5.Click += Logivälja;
            btn5.Visible = true;
            btn5.Location = new Point(240, btn2.Bottom);
            this.Controls.Add(btn5);





        }
        
        private void tekst_to_lbl(object sender, EventArgs e)
        {
            Console.WriteLine("Enter a text:");
            string text = Console.ReadLine();
            Console.WriteLine("Entered text: " + text);
        }
        public static bool salsasona(int k, out string password)
        {

            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < k; i++)
            {
                char t = (char)random.Next('A', 'Z' + 1);
                int num = random.Next(0, 10);
                //char sym = random.Next(Char(new[] { '*', '-', '.', '!', '_' }));
                char[] sym = { '*', '-', '.', '!', '_' };
                int randomIndex = random.Next(sym.Length);
                char[] t_num = { t, num.ToString()[0], sym[randomIndex] };
                sb.Append(t_num[random.Next(0, 3)]);
            }

            password = sb.ToString();
            return true;
        }


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
            nicknameTextBox.KeyDown += NicknameTextBox_KeyDown;
        }

        private void NicknameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (e.KeyCode == Keys.Enter && text.Text == "Töötaja")
            {
                Form3 form3 = new Form3();
                form3.Show();
            }
            else if(e.KeyCode == Keys.Enter && text.Text == "Ostja")
            {
                Form3 form3 = new Form3();
                form3.Show();
            }
            
        }

        

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            string nickname = nicknameTextBox.Text;
            TextBox password = new TextBox();
            string password1 = password.Text;

            if (logins.Contains(nickname))
            {
                MessageBox.Show("This nickname is already taken.");
            }
            else
            {
                if (string.IsNullOrEmpty(password1))
                {
                    password1 = GenerateRandomPassword(8);
                }

                logins.Add(nickname);
                passwords.Add(password1);

                MessageBox.Show($"Your password: {password1}");
            }
        }
        private string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
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