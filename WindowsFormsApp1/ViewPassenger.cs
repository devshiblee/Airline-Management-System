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
    public partial class ViewPassenger : Form
    {
        public ViewPassenger()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\USER\Documents\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            con.Open();
            string query = "select * from PassengerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query,con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PassengerDVG.DataSource = ds.Tables[0];
            con.Close();
        }

        public void TextboxFilter()
        {
            con.Open();
            string query = "select * from PassengerTbl where Phone = '" + PPhoneTb.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(query,con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            PassengerDVG.DataSource = ds.Tables[0];
            con.Close();
        }

        public void GenderFilter()
        {
            con.Open();
            string query = "select * from PassengerTbl where PassGend = '" + PGenderCb.SelectedItem.ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            PassengerDVG.DataSource = ds.Tables[0];
            con.Close();
        }
        private void ViewPassenger_Load(object sender, EventArgs e)
        {
         populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddPassenger addpas = new AddPassenger();
            addpas.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(PidTb.Text == "")
            {
                MessageBox.Show("Enter the Passenger Id To Delete");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "delete from PassengerTbl where PassId=" + PidTb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query,con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Passenger Deleted Successfuly");
                    con.Close();
                    populate();
                }
                catch(Exception Ex) 
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void PassengerDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PidTb.Text = PassengerDVG.SelectedRows[0].Cells[0].Value.ToString();
            PnameTb.Text = PassengerDVG.SelectedRows[0].Cells[1].Value.ToString();
            PassAd.Text = PassengerDVG.SelectedRows[0].Cells[2].Value.ToString();
            PpassTb.Text = PassengerDVG.SelectedRows[0].Cells[3].Value.ToString();
            PhoneTb.Text = PassengerDVG.SelectedRows[0].Cells[6].Value.ToString();
            natcb.SelectedItem = PassengerDVG.SelectedRows[0].Cells[4].Value.ToString();
            GenderCb.SelectedItem = PassengerDVG.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            PidTb.Text = "";
            PnameTb.Text = "";
            PassAd.Text = "";
            PpassTb.Text = "";
            PhoneTb.Text = "";
            natcb.SelectedItem = "";
            GenderCb.SelectedItem = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(PidTb.Text == "" || PnameTb.Text == "" || PassAd.Text == "" || PpassTb.Text == "" || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    String query = "update PassengerTbl set PassName='"+PnameTb.Text+"',Passport='"+PpassTb.Text+"',PassAd='"+PassAd.Text+"',Phone='"+PhoneTb.Text+"',PassNat='"+natcb.SelectedItem.ToString()+"',PassGend='"+GenderCb.SelectedItem.ToString()+"'where PassId="+PidTb.Text+"";
                     SqlCommand cmd = new SqlCommand(query,con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Passenger Updated Sucessfully");
                    con.Close();
                    populate();
                }
                catch(Exception Ex)
                {
                    MessageBox.Show("Missing Information");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Home viewpass = new Home();
            viewpass.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            TextboxFilter();
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            populate();
            PPhoneTb.Text = "";
            PGenderCb.Text = "";
        }

        private void PGenderCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenderFilter();
        }
    }
}
