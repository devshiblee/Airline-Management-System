using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    public partial class ViewFlights : Form
    {
        public ViewFlights()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\USER\Documents\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            con.Open();
            string query = "select * from FlightTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            FlightDVG.DataSource = ds.Tables[0];
            con.Close();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
         
        }

        public void FlightFilter()
        {
            con.Open();
            string query = "select * from FlightTbl where  Fsrc = '" + SrcCb1.SelectedItem.ToString() + "' AND FDest = '"+DesCb1.SelectedItem.ToString()+"' ";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            FlightDVG.DataSource = ds.Tables[0];
            con.Close();
        }

        private void ViewFlights_Load(object sender, EventArgs e)
        {
            populate(); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FlightTbl Addfl = new FlightTbl();
            Addfl.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FcodeTb.Text = "";
            Seatnum.Text = "";
            SrcCb.SelectedItem = "";
            DstCb.SelectedItem = "";
            
            
        }

        private void FlightDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            FcodeTb.Text = FlightDVG.SelectedRows[0].Cells[0].Value.ToString();
            SrcCb.SelectedItem = FlightDVG.SelectedRows[0].Cells[1].Value.ToString();
            DstCb.SelectedItem = FlightDVG.SelectedRows[0].Cells[2].Value.ToString();
            Seatnum.Text = FlightDVG.SelectedRows[0].Cells[4].Value.ToString();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FcodeTb.Text == "")
            {
                MessageBox.Show("Enter the Flight Code To Delete");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "delete from FlightTbl where Fcode='" + FcodeTb.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Flight Deleted Successfuly");
                    con.Close();
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
            if (FcodeTb.Text == "" || Seatnum.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    String query = "update FlightTbl set Fsrc='" + SrcCb.SelectedItem.ToString() + "',FDest='" + DstCb.SelectedItem.ToString() + "',FDate='" + FDate.Value.Date.ToString() + "',FCap=" + Seatnum.Text + " where Fcode='" + FcodeTb.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Flight Updated Sucessfully");
                    con.Close();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Missing Information");
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            FlightFilter();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();
            SrcCb1.Text = "";
            DesCb1.Text = "";
        }
    }
}
