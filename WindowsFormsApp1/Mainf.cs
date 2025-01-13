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
    public partial class Mainf : Form
    {
       
        public Mainf()
        {
            InitializeComponent();
            
            Home home = new Home();
            loadform(home);
            
        }

        public void loadform(object Form)
        {
            if(this.panelContainer.Controls.Count > 0)
            {
                this.panelContainer.Controls.RemoveAt(0);
            }
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panelContainer.Controls.Add(f);
            this.panelContainer.Tag = f;
            f.Show();
        }
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            loadform(new Admin());
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            loadform(new ViewPassenger());
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            loadform(new AddPassenger());
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            loadform(new FlightTbl());
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            loadform(new ViewFlights());
        }

        
        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            loadform(new Ticket());
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            loadform(new CancellationTbl());
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            loadform(home);
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Login viewpass = new Login();
            viewpass.Show();
            this.Hide();
        }

        private void Mainf_Load(object sender, EventArgs e)
        {
            adnamelabel.Text = Login.username;
        }
    }
}
