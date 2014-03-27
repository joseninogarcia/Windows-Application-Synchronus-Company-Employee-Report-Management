using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SychronusClass;

namespace SynchronusSoftware
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void employeeReportManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutFrm = new About();
            aboutFrm.Show();
            aboutFrm.BringToFront();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.synch_AccountsTableAdapter.Fill(this.synchDataDataSet.Synch_Accounts);
            LoginDetails k = new LoginDetails();
            Inside l = new Inside();
            label4.Text = "Welcome," + l.GetMyName(k.GetUserID()) + "!";
            timer1.Enabled = true;
            timer1.Interval = 1000;

            Connection g = new Connection();

            using (SqlConnection con = new SqlConnection(g.connectionClass()))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Report_Title, Report_DateTime, Report_Recipient, Report_Content FROM Synch_Report WHERE Report_UserAccount=" + k.GetUserID(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);   
                            gridview.DataSource = dt;
                            gridview.AutoGenerateColumns = false;

                            gridview.Columns["Report_Title"].HeaderText = "Title";
                            gridview.Columns["Report_DateTime"].HeaderText = "Date Time Created";
                            gridview.Columns["Report_DateTime"].HeaderText = "Recipient";
                            gridview.Columns["Report_Content"].HeaderText = "Content";

                        }
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label5.Text = "Date and Time: " + DateTime.Now.ToString();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            var login = new frmLogin();
            login.Show();
        }

        private void gridview_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void gridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = gridview.SelectedCells[0].RowIndex;
            textBox1.Text = gridview.Rows[i].Cells[0].Value.ToString();
            textBox2.Text = gridview.Rows[i].Cells[1].Value.ToString();
            label6.Text = gridview.Rows[i].Cells[3].Value.ToString();

        }

        private void sendReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formG = new SendReport();
            formG.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoginDetails k = new LoginDetails();
            Connection g = new Connection();

            using (SqlConnection con = new SqlConnection(g.connectionClass()))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Report_Title, Report_DateTime, Report_Recipient, Report_Content FROM Synch_Report WHERE Report_UserAccount=" + k.GetUserID(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gridview.DataSource = dt;
                            gridview.AutoGenerateColumns = false;

                            gridview.Columns["Report_Title"].HeaderText = "Title";
                            gridview.Columns["Report_DateTime"].HeaderText = "Date Time Created";
                            gridview.Columns["Report_DateTime"].HeaderText = "Recipient";
                            gridview.Columns["Report_Content"].HeaderText = "Content";

                        }
                    }
                }
            }
        }
    }
}
