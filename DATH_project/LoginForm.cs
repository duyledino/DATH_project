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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        bool hide = true;
        private void loginBtn_Click(object sender, EventArgs e)
        {
            checkLogin();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            userName.Focus();
            Pass.PasswordChar = '*';
        }
        private void checkLogin()
        {
            if (userName.Text == "trasuamenau123" && Pass.Text == "123456")
            {
                Pass.Text = "";
                this.Hide();
                MessageBox.Show("Đăng nhập thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                new SelectScreen().Show();
            }
            else
            {
                MessageBox.Show("Thông tin đăng nhập không đúng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                userName.Text = "";
                Pass.Text = "";
                userName.Focus();
            }
        }
        private void clearBtn_Click(object sender, EventArgs e)
        {
            userName.Text = "";
            Pass.Text = "";
            userName.Focus();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn thoát chứ ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if(result == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        private void myEye_Click_1(object sender, EventArgs e)
        {
            if(hide == true)
            {
                Pass.PasswordChar = '\0';
                myEye.Image = Image.FromFile("..\\..\\..\\..\\login\\open.png");
                hide = false;
            }
            else
            {
                Pass.PasswordChar = '*';
                myEye.Image = Image.FromFile("..\\..\\..\\..\\login\\close.png");
                hide = true;
            }
        }
        
        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                checkLogin();
            }
        }

        private void Pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                checkLogin();
            }
        }
    }
}
