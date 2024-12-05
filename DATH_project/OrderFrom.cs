using DATH_project.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DATH_project
{
    public partial class OrderForm : System.Windows.Forms.Form
    {
        public OrderForm()
        {
            InitializeComponent();
        }
        private List<WidgetData> data;
        private List<Widget> widgets;
        public List<Widget> cart;
        private Dictionary<string, int> quantity;
        public List<Widget> Cart { get { return cart; } }
        public Dictionary<string, int> _quantity { get { return quantity; } }
        public DateTime orderTime { get; set; }
        public String getId(Widget w)
        {
            w.productId = (widgets.Count < 9) ? $"SP0{widgets.Count + 1}" : $"SP{widgets.Count + 1}" ;
            return w.productId;
        }
        public List<Widget> GetWidgets()
        {
            return this.widgets;
        }
        public void AddItem(String Product,double Price,Categories category,String Images)
        {
            Widget w = new Widget()
            {
                product = Product,
                price = Price,
                image = Image.FromFile("ProductImage\\" + Images),
                category = category,
                Tag = category
            };
            Widget temp = new Widget();
            w.productId = getId(temp);
            flowMenu.Controls.Add(w);
            widgets.Add(w);
        }
        public bool readFile(string FileName,out List<WidgetData> getWidgets)
        {
            try
            {
                FileStream f = new FileStream(FileName, FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                getWidgets = bf.Deserialize(f) as List<WidgetData>;
                f.Close();
                return true;
            }
            catch (Exception)
            {
                getWidgets = null;
                return false;
            }
        }
        public bool WriteFile(string FileName)
        {
            try
            {
                FileStream f = new FileStream(FileName, FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(f, data);
                f.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void userClick(object sender,EventArgs e)
        {
            Widget w = (Widget)sender;
            if (quantity.ContainsKey(w.productId)==false)
            {
                cart.Add(w);
                quantity.Add(w.productId, 1);
            }
            else
            {
                quantity[w.productId]++;
            }
            showCart();
        }
        private void OrderForm_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            cart = new List<Widget>();
            widgets = new List<Widget>();
            quantity = new Dictionary<string, int>();
            data = new List<WidgetData>();
            AddItem("Hồng trà sữa", 25000,Categories.MilkTea, "HTS.jpg");
            AddItem("Trà sữa oolong rang", 25000, Categories.MilkTea, "TSOR.jpg");
            AddItem("Trà sữa thái sen", 25000, Categories.MilkTea, "TSTS.png");
            AddItem("Trà chuối", 30000, Categories.FruitTea, "TraChuoi.jpg");
            AddItem("Trà Sầu Riêng", 35000, Categories.FruitTea, "traSauRieng.png");
            AddItem("Cafe Bạc Xĩu", 25000, Categories.Cafe, "BacXiu.png");
            AddItem("Cafe Kem Muối", 25000, Categories.Cafe, "cafeKemMuoi.jpg");
            foreach (Widget w in widgets)
            {
                data.Add(new WidgetData(w.productId, w.product, w.price, 0));
            }
            if (WriteFile("Product.dat"))
            {
                timer2.Start();
            }
            else
            {
                MessageBox.Show("Khong luu duoc");
            }
            for(int i = 0; i < widgets.Count; i++)
            {
                widgets[i].buttonClicked += new EventHandler(this.userClick);
            }
        }
        private void showCart()
        {
            dataGridView1.Rows.Clear();
            for (int i=0;i<cart.Count; i++)
            {
                int q = quantity[cart[i].productId];
                dataGridView1.Rows.Add(cart[i].product.ToString(), quantity[cart[i].productId].ToString(), string.Format("{0:#,000}vnd", cart[i].price));
            }
            total.Text = String.Format("{0:#,000}vnd", totalCart(quantity,cart));
        }
        public double totalCart(Dictionary<string,int> _quantity,List<Widget> cart)
        {
            double sum = 0;
            foreach (Widget widget in cart)
            {
                sum+= widget.price * _quantity[widget.productId];
            }
            return sum;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            total.Text = "0vnd";
            quantity.Clear();
            cart.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            orderTime = DateTime.Now;
            ManageForm form2 = new ManageForm();
            DialogResult result=MessageBox.Show("Bạn có chắc chắn với đơn hàng này chứ ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.None);
            if (result == DialogResult.Yes)
            {
                dataGridView1.Rows.Clear();
                form2.Received(cart, quantity, orderTime);
                cart.Clear();
                total.Text = "0vnd";
                MessageBox.Show("Mua Hàng thành công!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }
        int i = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (i <= 30)
            {
                done.Visible = true;
                i++;
            }
            else if(i==31)
            {
                done.Visible = false;
                timer2.Stop();
            }
            
        }

        private void AllBtn_Click(object sender, EventArgs e)
        {
            show.Text = "Xin Chào";
            foreach (var item in flowMenu.Controls)
            {
                var widget = (Widget)item;
                widget.Visible = true;
            }
        }

        private void milkTeaBtn_Click(object sender, EventArgs e)
        {
            show.Text = "Trà Sữa";
            foreach (var item in flowMenu.Controls)
            {
                var widget = (Widget)item;
                widget.Visible = milkTeaBtn.Tag.ToString().Trim().ToLower() == widget.Tag.ToString().Trim().ToLower();
            }
        }

        private void fruitTeaBtn_Click(object sender, EventArgs e)
        {
            show.Text = "Trà Trái Cây";
            foreach (var item in flowMenu.Controls)
            {
                var widget = (Widget)item;
                widget.Visible = fruitTeaBtn.Tag.ToString().Trim().ToLower() == widget.Tag.ToString().Trim().ToLower();
            }
        }

        private void cafeBtn_Click(object sender, EventArgs e)
        {
            show.Text = "Cà Phê";
            foreach (var item in flowMenu.Controls)
            {
                var widget = (Widget)item;
                widget.Visible = cafeBtn.Tag.ToString().Trim().ToLower() == widget.Tag.ToString().Trim().ToLower();
            }
        }
    }
}
