using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DATH_project.Components
{
    public enum Categories
    {
        MilkTea,Cafe,FruitTea
    }
    public partial class Widget : UserControl
    {
        public Widget()
        {
            InitializeComponent();
        }
        private String ProductId;
        private Categories Category;
        public string productId
        {
            get { return ProductId; }
            set { ProductId = value; }
        } 
        public String product{
            get { return productName.Text; }
            set { productName.Text = value; }
        }
        public double price
        {
            get { return double.Parse(lbPrice.Text.Replace(",","").Replace("vnd","")); }
            set { lbPrice.Text = string.Format("{0:#,000}vnd",value); }
        }
        public Image image
        {
            get { return picture.Image; }
            set { picture.Image = value; }
        }
        public Categories category
        {
            get { return Category; }
            set {  Category = value; }
        }
        public Widget(string id, string product,Categories category, double price,Image image) {
            this.productId = id;
            this.product = product;
            this.price = price;
            this.image = image;
            this.category = category;
        }
        public event EventHandler buttonClicked;
        private void button1_Click(object sender, EventArgs e)
        {
            buttonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
    [Serializable]
    public class WidgetData {
        public string productId
        {
            get; set;
        }
        public string productName
        {
            get; set;
        }
        public double price
        {
            get; set;
        }
        public int quantity
        {
            get; set;
        }
        public WidgetData(string productId, string productName, double price, int quantity)
        {
            this.productId = productId;
            this.productName = productName;
            this.price = price;
            this.quantity = quantity;
        }
    }
}
