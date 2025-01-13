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
using System.Web.UI.WebControls.WebParts;

namespace WindowsFormsApp1
{
    public partial class CancellationTbl : Form
    {
        public CancellationTbl()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\USER\Documents\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void fillFlightCode()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select TNo from TicketTbl", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TNo", typeof(string));
            dt.Load(rdr);
            TNoCb.ValueMember = "TNo";
            TNoCb.DataSource = dt;
            con.Close();
        }

        private void fetchFlightCode()
        {
            con.Open();
            string query = "select * from TicketTbl where TNo=" + TNoCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Fcode.Text = dr["Fcode"].ToString();
                
            }
            con.Close();
        }

        private void populate()
        {
            con.Open();
            string query = "select * from CancelTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TicketCancDVG.DataSource = ds.Tables[0];
            con.Close();
        }

        private void CancellationTbl_Load(object sender, EventArgs e)
        {
            fillFlightCode();
            populate();
        }

        private void CancellationTbl_Load_1(object sender, EventArgs e)
        {
            fillFlightCode();
            populate();

        }

        private void TNoCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchFlightCode();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CancId.Text = "";
            Fcode.Text = "";
        }

        private void deleteTicket()
        {
           
                try
                {
                    con.Open();
                    string query = "delete from TicketTbl where TNo=" + TNoCb.SelectedValue.ToString() + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                   // MessageBox.Show("Ticket Cancelled Sucessfully");
                    con.Close();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (CancId.Text == "" || Fcode.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into CancelTbl values(" + CancId.Text + "," + TNoCb.SelectedValue.ToString() + ",'" + Fcode.Text + "','" + CancDate.Value.Date + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ticket Cancelled Sucessfully");
                    con.Close();
                    populate();
                    deleteTicket();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Ticket viewpass = new Ticket();
            viewpass.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home viewpass = new Home();
            viewpass.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
