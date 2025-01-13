using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Ticket_Genarate : Form
    {
        public string PName, Fsrc, Fdest, Fcode, Seatno, Sclass, PName2, Fsrc2, Fdest2, Fcode2, Seatno2, Sclass2;

        private void pictureBoxprint_Click(object sender, EventArgs e)
        {
            Print(this.panelprint);
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(memoryimg, (pagearea.Width / 2) - (this.panelprint.Width / 2), this.panelprint.Location.Y);
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void Ticket_Genarate_Load(object sender, EventArgs e)
        {
            labelPname.Text = PName;
            labelFsrc.Text = Fsrc;
            labelFdest.Text = Fdest;
            labelFcode.Text = Fcode;
            labelSeatno.Text = Seatno;
            labelSeatclass.Text = Sclass;
            labelPname2.Text = PName2;
            labelFsrc2.Text = Fsrc2;
            labelFdest2.Text = Fdest2;
            labelFcode2.Text = Fcode2;
            labelSeatno2.Text = Seatno2;
            labelSeatclass2.Text = Sclass2;
        }

        public Ticket_Genarate()
        {
            InitializeComponent();
        }
        
        private void Print(Panel pnl)
        {
            PrinterSettings ps = new PrinterSettings();
            panelprint = pnl;
            getprintarea(pnl);
            printPreviewDialog1.Document = printDocument1;
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printPreviewDialog1.ShowDialog();
        }

        private Bitmap memoryimg;
        private void getprintarea(Panel pnl)
        {
            memoryimg = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(memoryimg , new Rectangle (0,0,pnl.Width,pnl.Height));
        }
        private void pictureBoxprint_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBoxprint,"Print");
        }
    }
}
