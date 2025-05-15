using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Fatura_sistem.Models;

namespace Fatura_sistem
{
    public partial class ReciteManager : Form
    {
        private List<Product> products = new List<Product>();
        private List<CartItem> cartItems = new List<CartItem>();
        private string productPath = Path.Combine(Application.StartupPath, "Urunler.xml");
        

       public MainMenu mainMenu = new MainMenu();
    
        public ReciteManager()
        {

            InitializeComponent();
            
            this.FormClosing += ReciteManager_FormClosing;
            this.BackColor = Color.FromArgb(40, 40, 40);
            labelTotal.Font = new Font("Arial", 40);

            products = LoadProducts();
            CreateProductButtons();
            UpdateCartDisplay();

            listBox1.Items.Clear();
            foreach (var p in products)
            {
                listBox1.Items.Add($"{p.Name} - {p.Price:C}");
            }

            this.Controls.Add(flowLayoutPanel2);
            this.Controls.Add(listBox1);
            this.Controls.Add(labelTotal);
            flowLayoutPanel2.AutoScroll = true;
            labelTotal.ForeColor = Color.White;
        }

        public void ListeyiYenidenYukle()
        {
            products = LoadProducts();
            listBox1.Items.Clear();

            foreach (var p in products)
            {
                listBox1.Items.Add(p);
            }
        }

        private List<Product> LoadProducts()
        {
            try
            {
                if (File.Exists(productPath))
                {


                    XmlSerializer serializer = new XmlSerializer(typeof(List<Product>));
                    using (FileStream fs = new FileStream(productPath, FileMode.Open))
                    {
                        var list = (List<Product>)serializer.Deserialize(fs);
                        MessageBox.Show($"Ürün sayısı: {list.Count}");
                        return list;
                    }
                }
                else
                {
                    MessageBox.Show("Dosya yok!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Deserialize HATASI: " + ex.Message);
            }
            return new List<Product>();
        }
       


        private void CreateProductButtons()
        {
            flowLayoutPanel2.Controls.Clear();

            foreach (var product in products)
            {
                Button btn = new Button();
                btn.Text = $"{product.Name}\n{product.Price:C}";
                btn.Tag = product;
                btn.Width = 140;
                btn.Height = 60;
                btn.Margin = new Padding(5);
                btn.BackColor = Color.FromArgb(30, 144, 255); 
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Click += SepeteEkle;
                flowLayoutPanel2.Controls.Add(btn);
            }
        }

        private void SepeteEkle(object sender, EventArgs e)
        {
            var button = sender as Button;
            var product = button.Tag as Product;

            var existingItem = cartItems.Find(ci => ci.Product.Name == product.Name);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cartItems.Add(new CartItem(product, 1));
            }

            UpdateCartDisplay();
        }

        private void UpdateCartDisplay()
        {
            listBox1.Items.Clear();
            decimal total = 0;

            foreach (var item in cartItems)
            {
                listBox1.Items.Add($"{item.Product.Name} x {item.Quantity} = {item.TotalPrice:C}");
                total += item.TotalPrice;
            }

            labelTotal.Text = $"Toplam: {total:C}";
        }

        public void SaveProducts(List<Product> productsToSave)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Product>));
                using (FileStream fs = new FileStream(productPath, FileMode.Create))
                {
                    serializer.Serialize(fs, productsToSave);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("KAYDETME HATASI: " + ex.Message);
            }
        }

        private void ReciteManager_FormClosing(object sender, FormClosingEventArgs e)
        {

            mainMenu.Show();
            this.Hide();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
