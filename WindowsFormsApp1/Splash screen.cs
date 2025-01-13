using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Splash_screen : Form
    {
        public Splash_screen()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            gProgressBar1.Increment(2);
            if(gProgressBar1.Value == 100 )
            {
             timer1.Enabled=false;
                Login login = new Login();
                login.Show();
                this.Hide();
            }
        }
    }
}
