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
    public partial class Ticket : Form
    {
        public Ticket()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\USER\Documents\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            con.Open();
            string query = "select * from TicketTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TicketDVG.DataSource = ds.Tables[0];
            con.Close();
        }
        private void fillPassenger()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select PassId from PassengerTbl", con);
            SqlDataReader rdr;
            rdr= cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PassId",typeof(int));
            dt.Load(rdr);
            PIdCb.ValueMember = "PassId";
            PIdCb.DataSource = dt;
            con.Close();
        }
        private void fillFlightCode()
        {
            FCodeCb.Items.Clear();
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Fcode from FlightTbl";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            foreach(DataRow dr in dt.Rows)
            {
                FCodeCb.Items.Add(dr["Fcode"].ToString());
            }
            con.Close();
        }
        string pname, ppass, pnat;

        private void fetchPassenger()
        {
            con.Open();
            string query = "select * from PassengerTbl where PassId=" + PIdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                pname = dr["PassName"].ToString();
                ppass = dr["Passport"].ToString();
                pnat = dr["PassNat"].ToString();

                PNameTb.Text = pname;
                PPassTb.Text = ppass;
                PNatTb.Text = pnat;

            }
            con.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (TNo.Text == "" || PNameTb.Text == "" || FCodeCb.Text == "" || SeatTypeCb.Text == "" ||SeatNoCb.Text =="")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into TicketTbl values(" + TNo.Text + ",'" + FCodeCb.SelectedItem.ToString() + "'," + PIdCb.SelectedValue.ToString() + ",'" + PNameTb.Text + "','" + PPassTb.Text + "','" + PNatTb.Text + "','" + FSrcTb.Text + "','" + FDesTb.Text + "','"+SeatTypeCb.SelectedItem.ToString()+"','"+SeatNoCb.SelectedItem.ToString()+"'," + PAmt.Text + ")";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ticket Booked Sucessfully");
                    con.Close();
                    populate();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PNameTb.Text = "";
            PNatTb.Text = "";
            TNo.Text = "";
            PPassTb.Text = "";
            PAmt.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home viewpass = new Home();
            viewpass.Show();
            this.Hide();
        }

        private void FCodeCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM FlightTbl where Fcode='" + FCodeCb.Text + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string src = (string)dr["Fsrc"].ToString();
                FSrcTb.Text = src;
                string dest = (string)dr["FDest"].ToString();
                FDesTb.Text = dest;
            }

            con.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void SeatTypeCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM PricingTbl where Seat_Type='" + SeatTypeCb.Text + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string src = (string)dr["Price"].ToString();
                PAmt.Text = src;
                
            }

            con.Close();
        }

        private void TicketDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TNo.Text = TicketDVG.SelectedRows[0].Cells[0].Value.ToString();
            FCodeCb.SelectedItem = TicketDVG.SelectedRows[0].Cells[1].Value.ToString();
            PIdCb.SelectedItem = TicketDVG.SelectedRows[0].Cells[2].Value.ToString();
            PNameTb.Text = TicketDVG.SelectedRows[0].Cells[3].Value.ToString();
            PPassTb.Text = TicketDVG.SelectedRows[0].Cells[4].Value.ToString();
            PNatTb.Text = TicketDVG.SelectedRows[0].Cells[5].Value.ToString();
            FSrcTb.Text = TicketDVG.SelectedRows[0].Cells[6].Value.ToString();
            FDesTb.Text = TicketDVG.SelectedRows[0].Cells[7].Value.ToString();
            SeatTypeCb.SelectedItem = TicketDVG.SelectedRows[0].Cells[8].Value.ToString();
            SeatNoCb.SelectedItem = TicketDVG.SelectedRows[0].Cells[9].Value.ToString();
            PAmt.Text = TicketDVG.SelectedRows[0].Cells[10].Value.ToString();
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            Ticket_Genarate ticket_Genarate = new Ticket_Genarate();
            ticket_Genarate.PName = PNameTb.Text;
            ticket_Genarate.Fsrc = FSrcTb.Text;
            ticket_Genarate.Fdest = FDesTb.Text;
            ticket_Genarate.Fcode = FCodeCb.SelectedItem.ToString();
            ticket_Genarate.Seatno = SeatNoCb.SelectedItem.ToString();
            ticket_Genarate.Sclass = SeatTypeCb.SelectedItem.ToString();
            ticket_Genarate.PName2 = PNameTb.Text;
            ticket_Genarate.Fsrc2 = FSrcTb.Text;
            ticket_Genarate.Fdest2 = FDesTb.Text;
            ticket_Genarate.Fcode2 = FCodeCb.SelectedItem.ToString();
            ticket_Genarate.Seatno2 = SeatNoCb.SelectedItem.ToString();
            ticket_Genarate.Sclass2 = SeatTypeCb.SelectedItem.ToString();
            ticket_Genarate.ShowDialog();
        }

        private void PIdCb_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            fetchPassenger();
        }

        private void Ticket_Load(object sender, EventArgs e)
        {
            fillPassenger();
            fillFlightCode();
            populate();
        }

       
    }
}
