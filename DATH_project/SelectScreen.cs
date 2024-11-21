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
    public partial class SelectScreen : System.Windows.Forms.Form
    {
        public SelectScreen()
        {
            InitializeComponent();
        }
        private Form currentForm;
        private void openChildForm(Form child)
        {
            currentForm = child;
            this.Hide();
            child.FormClosed += (sender, e) => this.Show();
            child.Show();
        }
        private void AdminBtn_Click_1(object sender, EventArgs e)
        {
            panelAdmin.Visible = true;
        }

        private void OrderBtn_Click_1(object sender, EventArgs e)
        {
            openChildForm(new MenuMethod());
            KeyBox.Focus();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            EFunc eFunc = new EFunc();
            List<Employee> temp = new List<Employee>();
            eFunc.readFile("Employee.dat", out temp);
            bool check = false;
            foreach (Employee emp in temp)
            {
                if(emp.Key == KeyBox.Text.ToString() && KeyBox.Text != "" && emp.Key != "")
                {
                    KeyBox.Text = "";
                    this.openChildForm(new ManageInterface(emp.Key));
                    check = true;
                    break;
                }
            }
            if(check == false)
            {
                MessageBox.Show("Key không đúng","Thông báo",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
                KeyBox.Text = "";
                KeyBox.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KeyBox.Text = "";
            panelAdmin.Visible = false;
        }
    }
}
