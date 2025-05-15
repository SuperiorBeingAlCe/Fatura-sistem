using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Fatura_sistem.Models;

namespace Fatura_sistem
{
    public partial class AddProduct : Form
    {
        public ProductManager manager { get; set; }
        public AddProduct()
        {
            InitializeComponent();
            this.FormClosing += AddProduct_FormClosing;
            label1.Text = "Ürün adı:";
            label2.Text = "Ürün fiyatı:";

            label1.ForeColor = Color.White;
            label2.ForeColor = Color.White;

            this.BackColor = Color.FromArgb(40, 40, 40);

            textBox1.BackColor = Color.FromArgb(80, 80, 80);
            textBox2.BackColor = Color.FromArgb(80, 80, 80);
            textBox1.ForeColor = Color.White;
            textBox2.ForeColor = Color.White;

            button1.Text = "Ekle";
            button1.BackColor = Color.FromArgb(30, 144, 255); // Dodger Blue
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
        }
        public string UrunAdi { get; set; }
        public decimal UrunFiyati;


        private void AddProduct_FormClosing(object sender, FormClosingEventArgs e)
        {

            manager.Show();
            this.Hide();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Ürün adı boş olamaz.");
                return;
            }

            UrunAdi = textBox1.Text.Trim();

            decimal fiyat;
            if (!decimal.TryParse(textBox2.Text.Trim().Replace(".", ","),
                                  NumberStyles.Number,
                                  CultureInfo.CurrentCulture,
                                  out fiyat))
            {
                MessageBox.Show("Lütfen geçerli bir fiyat girin.\nÖrnek: 12,50",
                                "Hata",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;

            }

            Product yeniUrun = new Product(textBox1.Text.Trim(), fiyat);
            string path = Path.Combine(Application.StartupPath, "Urunler.xml");
            List<Product> urunler = new List<Product>();

            if (File.Exists(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Product>));
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    urunler = (List<Product>)serializer.Deserialize(fs);
                }
            }

            urunler.Add(yeniUrun);
            XmlSerializer writer = new XmlSerializer(typeof(List<Product>));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                writer.Serialize(fs, urunler);
            }

            MessageBox.Show("Ürün başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            UrunAdi = textBox1.Text.Trim();
            UrunFiyati = fiyat;
            this.DialogResult = DialogResult.OK;
            this.Close();


        }
    }
}
