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
    public partial class ManageInterface : Form
    {
        List<Employee> ds = new List<Employee>();
        string temp = "";
        public ManageInterface(string key)
        {
            EFunc eFunc = new EFunc();
            eFunc.readFile("Employee.dat", out ds);
            foreach (Employee e in ds) {
                if (key == e.Key)
                {
                    temp = e.Name;
                }
            }
            InitializeComponent();
        }
        private Form current;
        private void openForm(Form childForm)
        {
            if (current!=null)
            {
                current.Close();
            }
            current = childForm;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.TopLevel = false;
            childForm.Dock = DockStyle.Fill;
            panelBody.Controls.Add(childForm);
            panelBody.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openForm(new ManageForm());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openForm(new Statistics());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openForm(new Decentralization());
        }

        private void ManageInterface_Load(object sender, EventArgs e)
        {
            showName.Text = $"Xin chào Sếp: {temp}";
        }
    }
}
