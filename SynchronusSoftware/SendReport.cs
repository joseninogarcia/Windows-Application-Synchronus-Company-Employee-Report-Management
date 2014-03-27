using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using SychronusClass;

namespace SynchronusSoftware
{
   
    public partial class SendReport : Form
    {
        public SendReport()
        {
            InitializeComponent();
        }

        private void SendReport_Load(object sender, EventArgs e)
        {
            Connection g = new Connection();
            SqlDataReader dr;
            SqlConnection con = new SqlConnection(g.connectionClass());
            SqlCommand cmd = new SqlCommand("SELECT SynchEmp_No, SynchEmp_FirstName, SynchEmp_LastName FROM Synch_EmpInfo WHERE SynchEmp_Level = 'Manager'", con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteReader();
            var items = new List<string>();
            while(dr.Read())
            {
                items.Add(dr["SynchEmp_No"].ToString());
            }

            comboBox1.DataSource = items;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("No Title, please try again.");
            }
            else if (richTextBox1.Text == "")
            {
                MessageBox.Show("No Content, please provide it to your report.");
            }
            else
            {
                SendReportNew();
            }
        }

        private void SendReportNew()
        { 
            Connection g = new Connection();
            LoginDetails l = new LoginDetails();
            using (SqlConnection connection = new SqlConnection(g.connectionClass()))
            {
                connection.Open();
                SqlCommand addSite = new SqlCommand("INSERT INTO Synch_Report (Report_UserAccount ,Report_Title, Report_DateTime, Report_Recipient, Report_Content)" +
                                     "VALUES (@report_account,@report_title, @report_date, @report_recipient, @report_content)", connection);
                addSite.Parameters.AddWithValue("@report_account", l.GetUserID());
                addSite.Parameters.AddWithValue("@report_title", textBox1.Text);
                addSite.Parameters.AddWithValue("@report_date", DateTime.Now.ToString());
                addSite.Parameters.AddWithValue("@report_recipient", comboBox1.SelectedValue.ToString());
                addSite.Parameters.AddWithValue("@report_content", richTextBox1.Text);

                addSite.ExecuteNonQuery();
                MessageBox.Show("Your Report has been submitted to your Manager.");
                txtManager.Text = "";
                textBox1.Text = "";
                richTextBox1.Text = "";
                connection.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Inside f = new Inside();
            txtManager.Text = f.GetMyName(comboBox1.SelectedValue.ToString());
        }
    }

    public class ComboBoxItem
    {
        public string Value;
        public string Text;

        public ComboBoxItem(string val, string text)
        {
            Value = val;
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
