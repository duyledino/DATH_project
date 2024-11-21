using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.IO;
using DATH_project.Components;

namespace DATH_project
{
    public partial class ManageForm : System.Windows.Forms.Form
    {
        private List<order> orders;
        private List<order> qrOrders = new List<order>();
        private List<order> posOrders = new List<order>();
        private OrderForm of = new OrderForm();
        order selected = null;
        private String placeHolderText = "Nhập mã đơn hàng...";
        private Color foreColor = Color.Gray; 
        private Functional func = new Functional();
        private order getOrder;
        public ManageForm()
        {
            InitializeComponent();
        }
        public void Received(List<Widget> cart,Dictionary<string,int> quantity,DateTime orderDate)
        {
            double id = 0;
            List<WidgetData> drink = new List<WidgetData>();
            List<String> Quantity = new List<String>();
            List<order> temp;
            string total = of.totalCart(quantity,cart).ToString();
            foreach (Widget w in cart)
            {
                drink.Add(new WidgetData(w.productId, w.product, w.price, quantity[w.productId]));
                Quantity.Add(quantity[w.productId].ToString());
            }
            getOrder = new order(id, "Buy through Menu Pos","Buy throught Menu Pos",drink,Quantity,orderDate,total);
            if(func.readFile("posOrders.dat",out temp))
            {
                temp.Add(getOrder);
                if (func.writeFile("posOrders.dat", temp)) MessageBox.Show("Tranfered data succesfully", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                temp = new List<order>();
                temp.Add(getOrder);
                if (func.writeFile("posOrders.dat", temp)) MessageBox.Show("Tranfered data succesfully", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            of = new OrderForm();
            orders = new List<order>();
            if (FindText.Text == "")
            {
                FindText.Text = placeHolderText;
                FindText.ForeColor = foreColor;
            }
        }
        private void show()
        {
            dataGridView1.AutoGenerateColumns = false;
            BindingSource bs = new BindingSource();
            bs.DataSource = orders;
            dataGridView1.DataSource = bs;
        }   

        private void button1_Click_1(object sender, EventArgs e)
        {
            getData mydata = new getData();
            mydata.get(qrOrders);
            if (func.readFile("qrData.dat", out qrOrders) && func.readFile("posOrders.dat", out posOrders))
            {
                orders.Clear();
                foreach (order o in qrOrders)
                {
                    orders.Add(o);
                }
                if (posOrders != null)
                {
                    foreach (order o in posOrders)
                    {
                        o.OrderId = orders.Count < 9 ? $"DH0{orders.Count + 1}" : $"DH{orders.Count + 1}";
                        orders.Add(o);
                    }
                }
                show();
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            String find = FindText.Text;
            FindText.Text = "";
            try
            {
                order temp = func.find(orders, find);
                if (temp != null)
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = temp;
                    dataGridView1.DataSource = bs;
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy đơn nào có mã là: {find}", "Waring", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show($"Chưa có gì để hiện!!", "Waring", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
            }
        }

        private void FindText_Enter(object sender, EventArgs e)
        {
            if(FindText.Text == placeHolderText)
            {
                FindText.Text = "";
                FindText.ForeColor = Color.Black;
            }
        }

        private void FindText_Leave(object sender, EventArgs e)
        {
            if( FindText.Text == "") {
                FindText.Text = placeHolderText;
                FindText.ForeColor = foreColor;
            }
        }

        private void FindText_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                String find = FindText.Text;
                FindText.Text = "";
                try
                {
                    order temp = func.find(orders, find);
                    if (temp != null)
                    {
                        BindingSource bs = new BindingSource();
                        bs.DataSource = temp;
                        dataGridView1.DataSource = bs;
                    }
                    else
                    {
                        MessageBox.Show($"Không tìm thấy đơn nào có mã là: {find}", "Waring", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show($"Chưa có gì để hiện!!", "Waring", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                }
                e.SuppressKeyPress = true; //Ngăn không cho tiếng Ding của window
            }
        }

        private void print_Click(object sender, EventArgs e)
        {
            Functional func = new Functional();
            if (func.PrintReceipt(selected) == true)
            {
                MessageBox.Show("In biên lai thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("In biên lai không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    String id = row.Cells[0].Value.ToString();
                    for (int i = 0; i < orders.Count; i++)
                    {
                        if (orders[i].OrderId == id)
                        {
                            selected = orders[i];
                            break;
                        }
                    }
                    if (selected != null)
                    {
                        receiptShow.Text = "";
                        receiptShow.Text += "QUÁN THỨC UỐNG MẸ NẤU 😋\n\n";
                        receiptShow.Text += $"Mã Đơn Hàng: {selected.IdNumber.ToString()}\n\n";
                        receiptShow.Text += $"Thời gian mua hàng: {selected.orderDate.ToString()}\n\n";
                        receiptShow.Text += $"Họ Tên Khách Hang: {selected.customerName.ToString()}\n\n";
                        receiptShow.Text += $"SĐT: {selected.Phone.ToString()}\n\n";
                        receiptShow.Text += $"Đơn mua: \n\n";
                        for (int i = 0; i < selected.Drink.Count; i++)
                        {
                            receiptShow.Text += $"+ {selected.Drink[i].productName.ToString()}\n SL: {selected.Drink[i].quantity.ToString()}\n\n";
                        }
                        receiptShow.Text += $"Thành tiền: {selected.Total} VNĐ\n\n";
                        receiptShow.Text += "\t\t\tKý Tên: ";
                        receiptShow.Text += "\t\t\t\tChủ Quán ";
                        break;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Chưa có gì để hiện!!", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
            }
        }

        private void saveData_Click(object sender, EventArgs e)
        {
            if (func.writeFile("orders.dat",orders) == true)
            {
                MessageBox.Show("Lưu file thành công", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lưu file thất bại", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
        }
    }
}
