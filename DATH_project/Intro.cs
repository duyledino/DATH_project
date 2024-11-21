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
    public partial class Intro : Form
    {
        int num = 0,rgb = 0;
        bool down = false;
        public Intro()
        {
            InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (rgb < 250 && down == false)
            {
                label1.ForeColor = Color.FromArgb(rgb, rgb, rgb);
                num += 10;
                rgb += 10;
            }
            else if (rgb ==0  && down == true)
            {
                // Stop the timer
                timer1.Stop();

                // Create a new instance of Form1
                this.Hide();
                new LoginForm().Show();
            }
            else if (num<500)
            {
                    num += 10;
                    down = true;
            }else if(num==500 && rgb > 0)
            {
                label1.ForeColor = Color.FromArgb(rgb, rgb, rgb);
                rgb -= 10;
            }
        }


        private void Intro_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
