using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    
    public partial class Login : Form
    {
        public static string username;
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\USER\Documents\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AduserTb.Text = "";
            AdPassTb.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from AdminTbl where AdUsername='"+AduserTb.Text+"' and AdPassword='"+AdPassTb.Text+"'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                username= AduserTb.Text;
                Mainf main = new Mainf();
                main.ShowDialog();
                this.Hide();
                con.Close();
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password");
            }
            con.Close();

            
            
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                AdPassTb.UseSystemPasswordChar = true;
            }
            else
            {
                AdPassTb.UseSystemPasswordChar = false;
            }
        }
    }
}
