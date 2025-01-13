using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace WindowsFormsApp1
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\USER\Documents\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30");
       
        private void Reset()
        {
            AduserTb.Text = "";
            AdPassTb.Text = "";
            key = 0;
        }

        private void populate()
        {
            con.Open();
            string query = "select * from AdminTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            AdDVG.DataSource = ds.Tables[0];
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
         if(AduserTb.Text == ""|| AdPassTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
         else
            {
                try
                {
                    String query = "insert into AdminTbl values('" + AduserTb.Text + "','" + AdPassTb.Text + "','" + AdNameTb.Text + "','" + AdAgeTb.Text + "','" + AdGenderCb.SelectedItem.ToString() + "','" + AdMailTb.Text + "')";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Admin Successfully Saved");
                    con.Close();
                    Reset();
                    populate();
                }
                catch (Exception Ex)
                {
                  MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        int key = 0;
        private void AdDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         AduserTb.Text = AdDVG.SelectedRows[0].Cells[1].Value.ToString();
         AdPassTb.Text= AdDVG.SelectedRows[0].Cells[2].Value.ToString();
         AdNameTb.Text= AdDVG.SelectedRows[0].Cells[3].Value.ToString();
         AdAgeTb.Text= AdDVG.SelectedRows[0].Cells[4].Value.ToString();
         AdGenderCb.SelectedItem= AdDVG.SelectedRows[0].Cells[5].Value.ToString();
         AdMailTb.Text= AdDVG.SelectedRows[0].Cells[6].Value.ToString();


            if (AduserTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(AdDVG.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(key == 0)
            {
                MessageBox.Show("Select The Admin to Delete");
            }
            else
            {
                try
                {
                    String query = "Delete from AdminTbl where AdminId=" + key +";";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Admin Successfully Deleted");
                    con.Close();
                    Reset();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }



            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (AduserTb.Text == "" || AdPassTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    String query = "update AdminTbl set AdUsername='" + AduserTb.Text + "',AdPassword= '" + AdPassTb.Text + "',AdName='"+AdNameTb.Text+"',AdAge='"+AdAgeTb.Text+"',AdGend='"+AdGenderCb.SelectedItem.ToString()+"',AdEmail='"+AdMailTb.Text+"' where AdminId= " +key+";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Admin Updated Sucessfully");
                    con.Close();
                    Reset();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home viewpass = new Home();
            viewpass.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
