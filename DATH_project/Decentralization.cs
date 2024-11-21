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
    public partial class Decentralization : Form
    {
        private EFunc func = new EFunc();
        private List<Employee> dsnv = new List<Employee>();
        public Decentralization()
        {
            InitializeComponent();
        }

        private void Decentralization_Load(object sender, EventArgs e)
        {
            func.readFile("Employee.dat", out dsnv);
            if(dsnv == null) dsnv = new List<Employee>();
            else
            {
                show();
            }
        }
        private void show()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dsnv;
            dataGridView1.DataSource = bs;
        }
        public Employee find(string id)
        {
            if (dsnv == null) return null;
            foreach (Employee e in dsnv)
            {
                if (e.Id == id)
                {
                    return e;
                }
            }
            return null;
        }
        public bool Add(Employee employee)
        {
            if(dsnv == null) dsnv = new List<Employee>();
            if (find(employee.Id) != null) return false;
            dsnv.Add(employee);
            return true;
        }
        public bool Del(string id)
        {
            Employee temp = find(id);
            if (temp == null) return false;
            dsnv.Remove(temp);
            return true;
        }
        public bool Adjust(Employee employee)
        {
            Employee temp = find(employee.Id);
            if (temp == null) return false;
            temp.Id = employee.Id;
            temp.Name = employee.Name;
            temp.Position = employee.Position;
            temp.Key = employee.Key;
            return true;
        }
        private void addBtn_Click(object sender, EventArgs e)
        {
            Employee temp = new Employee();
            temp.Id = IdE.Text.ToString();
            temp.Name = NameE.Text.ToString();
            if (admin.Checked == true) temp.Position = pos.Admin;
            else if (manager.Checked == true) temp.Position = pos.Manager;
            else temp.Position = pos.Employee;
            temp.Key = KeyE.Text.ToString();
            if (Add(temp))
            {
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                show();
                func.writeFile("Employee.dat",dsnv);
            }
            else
            {
                MessageBox.Show("Đã có nhân viên này", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
        private void AdjustBtn_Click(object sender, EventArgs e)
        {
            Employee temp = new Employee();
            temp.Id = IdE.Text.ToString();
            temp.Name = NameE.Text.ToString();
            if (admin.Checked == true) temp.Position = pos.Admin;
            else if (manager.Checked == true) temp.Position = pos.Manager;
            else temp.Position = pos.Employee;
            temp.Key = KeyE.Text.ToString();
            if (Adjust(temp))
            {
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                show();
                func.writeFile("Employee.dat", dsnv);

            }
            else
            {
                MessageBox.Show("Nhân viên không tồn tại", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (Del(IdE.Text.ToString()))
            {
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                show();
                func.writeFile("Employee.dat", dsnv);
            }
            else
            {
                MessageBox.Show("Xóa thất bại", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string temp = row.Cells[0].Value.ToString();
                Employee emp = find(temp);
                if(emp != null)
                {
                    IdE.Text = emp.Id;
                    NameE.Text = emp.Name;
                    switch (emp.Position)
                    {
                        case pos.Admin:
                            admin.Checked = true; break;
                        case pos.Manager: manager.Checked = true; break;
                        case pos.Employee: employee.Checked = true; break;
                    }
                    KeyE.Text = emp.Key;
                    break;
                }
            }
        }
    }
}
