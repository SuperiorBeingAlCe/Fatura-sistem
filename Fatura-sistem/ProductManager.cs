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
    public partial class ProductManager : Form
    {
       

        public List<Product> products = new List<Product>();


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

        public void ListeyiYenidenYukle()
        {
            products = LoadProducts();
            listBox1.Items.Clear();
            
            foreach (var p in products)
            {
                listBox1.Items.Add(p);
            }
        }

        public string productPath = Path.Combine(Application.StartupPath, "Urunler.xml");

        public List<Product> LoadProducts()
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
        public string UrunAdi { get;  set; }
        public decimal UrunFiyati { get;  set; }

        public MainMenu mainMenu { get; set; }
        public ProductManager()
        {
            InitializeComponent();
            this.FormClosing += ProductManager_FormClosing;

            products = LoadProducts();
            listBox1.Items.Clear();
            foreach (var p in products)
            {
                listBox1.Items.Add(p);
               
            }

            this.BackColor = Color.FromArgb(40, 40, 40);
            this.Size = new Size(500, 600);
            this.Text = "Ürün Yönetimi";
            listBox1.Text = "Ürünler";

            button1.Text = "Ekle";
            button1.BackColor = Color.FromArgb(30, 144, 255); // Dodger Blue
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;

            button2.Text = "Sil";
            button2.BackColor = Color.FromArgb(30, 144, 255); // Dodger Blue
            button2.ForeColor = Color.White;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;

            button3.Text = "düzenle";
            button3.BackColor = Color.FromArgb(30, 144, 255); // Dodger Blue
            button3.ForeColor = Color.White;
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;

            button4.Text = "<-";
            button4.BackColor = Color.FromArgb(30, 144, 255); // Dodger Blue
            button4.ForeColor = Color.White;
            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.BorderSize = 0;

            groupBox1.Text = "İşlemler";
            groupBox1.ForeColor = Color.White;
            groupBox1.BackColor = Color.Purple;
            
            
        }

        private void ProductManager_FormClosing(object sender, FormClosingEventArgs e)
        {
           
                mainMenu.Show();
             this.Hide();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddProduct addProduct = new AddProduct();
            addProduct.manager = this;
            var result = addProduct.ShowDialog();

            if (result == DialogResult.OK)
            {
                
                ListeyiYenidenYukle();
            }


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainMenu.Show();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Product selectedProduct)
            {
                DialogResult result = MessageBox.Show(
                    $"'{selectedProduct.Name}' ürününü silmek istiyor musun?",
                    "Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                   
                    products.RemoveAll(p => p.Name == selectedProduct.Name && p.Price == selectedProduct.Price);

                    SaveProducts(products);

                   
                    listBox1.Items.Clear();
                    foreach (var p in products)
                        listBox1.Items.Add(p);

                    MessageBox.Show("Ürün başarıyla silindi.");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Product selectedProduct)
            {
                EditProduct editForm = new EditProduct();
                editForm.productManager = this;
                editForm.ProductToEdit = selectedProduct; 
                var result = editForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                  
                    products.RemoveAll(p => p.Name == selectedProduct.Name && p.Price == selectedProduct.Price);

                    
                    Product updated = editForm.ProductToEdit;
                    products.Add(updated);

                    SaveProducts(products);
                    ListeyiYenidenYukle();
                }
            }
           
            
        }
    }
}
