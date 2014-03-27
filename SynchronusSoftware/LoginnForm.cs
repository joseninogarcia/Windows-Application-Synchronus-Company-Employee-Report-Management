using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SychronusClass;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace SynchronusSoftware
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            LoginDetails lg = new LoginDetails();
            lg.Synch_EmpNo = txtEmpNo.Text;
            lg.Synch_EmpPass = txtPass.Text;

            if (lg.GetValidationResults() == 1)
            {
                this.Hide();
                var form2 = new MainForm();
                form2.Show();
            }
            else
            {
                MessageBox.Show("Sorry but Employee ID / Password does not exists!", "Error Login");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
