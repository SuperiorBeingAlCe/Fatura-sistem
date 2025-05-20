using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Fatura_sistem.Models;

namespace Fatura_sistem
{
    public partial class Register : Form
    {
        bool hasUpper;
        bool hasLower;
        bool hasDigit;
        bool hasSpecial;
        bool hasMinLength;
        public MainMenu mainMenu { get; set; }
        public Register()
        {
            InitializeComponent();
            Font ortakFont = new Font("Segoe UI", 10, FontStyle.Regular);

  

            this.BackColor = Color.FromArgb(40, 40, 40);
            groupBox1.BackColor = Color.FromArgb(60, 60, 60);
            groupBox1.Text = "Kayıt Ol";
            groupBox1.ForeColor = Color.White;

            label1.ForeColor = Color.White;
            label2.ForeColor = Color.White;
            label3.ForeColor = Color.White;
            label4.ForeColor = Color.White;
            label5.ForeColor = Color.White;
            label6.ForeColor = Color.White;

            textBox1.BackColor = Color.FromArgb(80, 80, 80);
            textBox2.BackColor = Color.FromArgb(80, 80, 80);
            textBox3.BackColor = Color.FromArgb(80, 80, 80);
            textBox4.BackColor = Color.FromArgb(80, 80, 80);

            textBox1.ForeColor = Color.White;
            textBox2.ForeColor = Color.White;
            textBox3.ForeColor = Color.White;
            textBox4.ForeColor = Color.White;

            comboBox1.BackColor = Color.FromArgb(80, 80, 80);
            comboBox1.ForeColor = Color.White;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            radioButton1.ForeColor = Color.White;
            radioButton2.ForeColor = Color.White;

            button1.BackColor = Color.FromArgb(30, 144, 255); // Dodger Blue
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;

            label1.Font = ortakFont;
            label2.Font = ortakFont;
            label3.Font = ortakFont;
            label4.Font = ortakFont;
            label5.Font = ortakFont;
            label6.Font = ortakFont;

            label1.Text = "Kullanıcı adı:";
            textBox1.MaxLength = 14;
            textBox2.MaxLength = 40;
            label2.Text = "E-mail:";
            label3.Text = "Şifre:";
            textBox3.MaxLength = 50;
            textBox3.PasswordChar = '*';
            textBox4.PasswordChar = '*';
            label4.Text = "Şifre tekrar:";
            radioButton1.Text = "Erkek";
            radioButton2.Text = "Kadın";
            
            for (int i = 0; i
                <= 100; i++)
            {
                comboBox1.Items.Add(i.ToString());
            }
            label5.Text = "Yaş:";
            label6.Text = "Cinsiyet:";
           
            button1.Text = "Kayıt Ol";
        }

       

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
    if (string.IsNullOrWhiteSpace(textBox1.Text))
    {
        ShowWarning("Kullanıcı adı boş bırakılamaz!");
        return;
    }

    if (string.IsNullOrWhiteSpace(textBox2.Text))
    {
        ShowWarning("E-mail alanı boş bırakılamaz!");
        return;
    }

    string email = textBox2.Text;
    string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    if (!Regex.IsMatch(email, emailPattern))
    {
        ShowWarning("E-mail adresi geçerli değil!\n\nDoğru format:\n - isim@site.com");
        return;
    }

    if (string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
    {
        ShowWarning("Şifre ve şifre tekrar alanı boş bırakılamaz!");
        return;
    }

    string password = textBox3.Text;
    if (!IsValidPassword(password))
    {
        ShowWarning(
            "Şifre kurallarına uymuyor!\n\n" +
            "- En az 8 karakter\n" +
            "- En az 1 büyük harf\n" +
            "- En az 1 küçük harf\n" +
            "- En az 1 rakam\n" +
            "\b- Özel karakter kullanma\b  (!@#$%^&*)"
        );
        return;
    }

    if (comboBox1.SelectedIndex < 18)
    {
        ShowWarning("18 Yaşından küçükler kayıt olamaz!");
        return;
    }

    string path = Path.Combine(Application.StartupPath, "Users.xml");
    string username = textBox1.Text;
    string age = comboBox1.SelectedItem.ToString();
    string gender = radioButton1.Checked ? "Erkek" : "Kadın";

    try
    {
        List<User> users = new List<User>();

        if (File.Exists(path))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                users = (List<User>)serializer.Deserialize(fs);
            }
        }

        users.Add(new User(username, email, password, age, gender));

        XmlSerializer writer = new XmlSerializer(typeof(List<User>));
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            writer.Serialize(fs, users);
        }

        MessageBox.Show("Kayıt başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        mainMenu?.Show();
        this.Close();
    }
    catch (Exception ex)
    {
        MessageBox.Show("HATA: " + ex.Message);
    }
}

private void ShowWarning(string message)
{
    MessageBox.Show(message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
}

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int score = 0;
            string password = textBox3.Text;
            if (password.Length >= 8) score += 20;
            if (password.Any(char.IsLower)) score += 20;
            if (password.Any(char.IsUpper)) score += 20;
            if (password.Any(char.IsDigit)) score += 20;
            if (password.Any(ch => "!@#$%^&*()_+[]{};':\"\\|,.<>?/`~".Contains(ch))) score += 20;

            progressBar1.Value = score;

        }
    }

}
