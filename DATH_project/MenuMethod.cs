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
    public partial class MenuMethod : Form
    {
        public MenuMethod()
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
        private void MenuBtn_Click_1(object sender, EventArgs e)
        {
            OrderForm od = new OrderForm();
            openChildForm(od);
        }

        private void QrBtn_Click_1(object sender, EventArgs e)
        {
            openChildForm(new QrForm());
        }
    }
}
