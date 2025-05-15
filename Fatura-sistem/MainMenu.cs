using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Fatura_sistem.Models;

namespace Fatura_sistem
{
    public partial class MainMenu : Form
    {

        public static bool IsLoggedIn = false;
       
        public static string CurrentUsername = null;
        
        public MainMenu()
        {
            InitializeComponent();
            

            label4.Text = "HOŞGELDİN";

            
            this.BackColor = Color.FromArgb(40, 40, 40);
            label1.ForeColor = Color.White;
            label2.ForeColor = Color.White;
            label4.ForeColor = Color.Red;
            label4.Font = new Font("Arial",25);
            label4.Hide();

            textBox1.BackColor = Color.FromArgb(80, 80, 80);
            textBox2.BackColor = Color.FromArgb(80, 80, 80);
            textBox1.ForeColor = Color.White;
            textBox2.ForeColor = Color.White;

            button1.BackColor = Color.FromArgb(30, 144, 255); // Dodger Blue
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;

            button2.BackColor = Color.FromArgb(30, 144, 255); // Dodger Blue
            button2.ForeColor = Color.White;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;

           

            button5.BackColor = Color.FromArgb(30, 144, 255); // Dodger Blue
            button5.ForeColor = Color.White;
            button5.FlatStyle = FlatStyle.Flat;
            button5.FlatAppearance.BorderSize = 0;

         

            button5.Text = "giriş yap";
            groupBox1.Text = "Giriş yap";
            groupBox1.ForeColor = Color.White;
            label3.Text = "Hesabın yok mu?";
            label3.ForeColor = Color.Yellow;
            label3.Font = new Font(label3.Font, FontStyle.Underline);
            label1.Text = "Kullanıcı adı";
            textBox1.MaxLength = 14;
            label2.Text = "Şifre";
            textBox2.MaxLength = 14;
            textBox2.PasswordChar = '*';

            this.Text = "Fatura Sistemi";
            this.Size = new Size(600, 300);
            this.Font = new Font("Segoe UI", 10);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            button1.Text = "Fatura Hesapla";
            button2.Text = "Ürün ekle, sil, düzenle";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
               

                    if (IsLoggedIn != false)
                    {

                     ReciteManager reciteManager = new ReciteManager();
                reciteManager.mainMenu = this;
                reciteManager.Show();
                this.Hide();


                    }else { MessageBox.Show("Bir hesaba giriş yapmadan bu özelliği kullanamazsın!","Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
               


            }
                

        

        private void label3_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.mainMenu = this;
            register.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string path = Path.Combine(Application.StartupPath, "Users.xml");

            if (File.Exists(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    List<User> users = (List<User>)serializer.Deserialize(fs);
                    var match = users.FirstOrDefault(u =>
                     u.Username.Trim().Equals(username, StringComparison.OrdinalIgnoreCase) &&
                     u.Password.Trim() == password);

                    if (match != null)
                    {
                       IsLoggedIn = true;
                        CurrentUsername = match.Username;

                        MessageBox.Show("Giriş başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        groupBox1.Hide();
                        label4.Show();
                        label4.Text = $"HOŞGELDİN, {CurrentUsername.ToUpper()}";

                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı verisi bulunamadı!");
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (IsLoggedIn != false)
            {

              ProductManager productManager = new ProductManager();
                productManager.mainMenu = this;
                productManager.Show();
                this.Hide();
               

            }
            else { MessageBox.Show("Bir hesaba giriş yapmadan bu özelliği kullanamazsın!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

       
    }
}
