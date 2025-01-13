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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AddPassenger viewpass = new AddPassenger();
            viewpass.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
             FlightTbl viewpass= new FlightTbl();
            viewpass.Show();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            ViewFlights viewpass = new ViewFlights();
            viewpass.Show();
            this.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Ticket viewpass = new Ticket();
            viewpass.Show();
            this.Hide();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            CancellationTbl viewpass = new CancellationTbl();
            viewpass.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            ViewPassenger viewpass = new ViewPassenger();
            viewpass.Show();
            this.Hide();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            Admin viewpass = new Admin();
            viewpass.Show();
            this.Hide();
        }

    }
}
