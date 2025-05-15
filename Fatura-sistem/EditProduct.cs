using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fatura_sistem.Models;

namespace Fatura_sistem
{
    public partial class EditProduct : Form
    {
        
        public Product ProductToEdit { get; set; }

       
        
        public ProductManager productManager;
        public EditProduct()
        {
            InitializeComponent();
            this.FormClosing += EditProduct_FormClosing;

          
                textBox1.Text = ProductToEdit.Name;
                textBox2.Text = ProductToEdit.Price.ToString();
        

            this.BackColor = Color.FromArgb(40, 40, 40);
            button1.Text = "düzenle";
            button1.BackColor = Color.FromArgb(30, 144, 255); // Dodger Blue
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void EditProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            
                productManager.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            decimal price;

            if (decimal.TryParse(textBox2.Text, out price))
            {
                ProductToEdit.Name = name;
                ProductToEdit.Price = price;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Geçerli bir fiyat girin.");
            }
        }
    }
}
