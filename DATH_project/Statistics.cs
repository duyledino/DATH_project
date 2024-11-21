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

namespace DATH_project
{
    public partial class Statistics : Form
    {
        private Functional func = new Functional();
        private OrderForm orderForm = new OrderForm();
        private List<WidgetData> getData = new List<WidgetData>();
        private List<order> orders = new List<order>(); 
        public Statistics()
        {
            InitializeComponent();
        }
        //private 

        private void show()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = getData;
            dataGridView1.DataSource = bs;
        }
        private bool updateData(out List<WidgetData> data)
            //done
        {
            if (orderForm.readFile("Product.dat", out data) && func.readFile("orders.dat", out orders))
            {
                List<WidgetData> temp = data;
                foreach(WidgetData w in temp)
                {
                    w.price = 0;
                }
                foreach (WidgetData w in temp)
                {
                    foreach (order o in orders)
                    {
                        for(int i = 0; i < o.Drink.Count; i++)
                        {
                            if(w.productId == o.Drink[i].productId)
                            {
                                w.quantity += o.Drink[i].quantity;
                                w.price += o.Drink[i].price * o.Drink[i].quantity;
                                break;
                            }
                        }
                    }
                }
                data = temp;
                return true;
            }
            data = null;
            return false;
        }
        private void Statistics_Load(object sender, EventArgs e)
        {
            if (updateData(out getData))
            {
                show();
            }
            else
            {
                MessageBox.Show("Chưa có dữ liệu đơn hàng để thống kê !!!","Thông báo",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
            }
        }

        private void Statistics_KeyDown(object sender, KeyEventArgs e)
        {
            if((e.KeyCode == Keys.F5)){
                if (updateData(out getData))
                {
                    show();
                }
                else
                {
                    MessageBox.Show("Chưa có dữ liệu đơn hàng để thống kê !!!", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
            }
        }

        private void chartBtn_Click(object sender, EventArgs e)
        {
            chart.Visible = true;
            myChart.Series["Pie"].IsValueShownAsLabel = true;
            for (int i = 0; i < getData.Count(); i++)
            {
                myChart.Series["Pie"].Points.AddXY($"{getData[i].productName}", $"{getData[i].quantity}");   
            }
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            chart.Visible = false;
        }
    }
}
