using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SychronusClass
{
    public class LoginDetails : Connection
    {
        public static string EmpNo;

        private string _synch_empno;
        private string _synch_emppass;

        public string Synch_EmpNo
        {
            get { return _synch_empno; }
            set { _synch_empno = value; }
        }

        public string Synch_EmpPass
        {
            get { return _synch_emppass; }
            set { _synch_emppass = value; }
        }

        /// <summary>
        /// Checks the employee from the database if it is existing. 
        /// If the not then the result should be false.
        /// </summary>
        /// <returns></returns>
        private bool CheckAccountNumberIfExists()
        {
            //Declaration of variables
            bool checkResults = true;
            bool temp;

            using (SqlConnection connect = new SqlConnection(connectionClass()))
            {
                try
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand("CheckUserID", connect);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@username", Synch_EmpNo);

                    temp = Convert.ToBoolean(command.ExecuteScalar().ToString());

                    if (temp){
                        checkResults = true;
                        LoginDetails.EmpNo = Synch_EmpNo;
                    }
                    else{
                        checkResults = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("LoginDetails Error: " + ex.Message);
                }
                finally
                {
                    connect.Close();
                    connect.Dispose();
                }
            }
            return checkResults;
        }

        /// <summary>
        /// Checks the password if it is existing on the database
        /// </summary>
        /// <returns></returns>
        private bool CheckPasswordIfExists()
        {
            //Declaration of variables
            bool checkResults = true;
            bool temp;

            using (SqlConnection connect = new SqlConnection(connectionClass()))
            {
                try
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand("CheckIfPasswordExists", connect);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@username", Synch_EmpNo);
                    command.Parameters.AddWithValue("@password", Synch_EmpPass);

                    temp = Convert.ToBoolean(command.ExecuteScalar().ToString());

                    if (temp)
                    {
                        checkResults = true;
                    }
                    else
                    {
                        checkResults = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("LoginDetails Error: " + ex.Message);
                }
                finally
                {
                    connect.Close();
                    connect.Dispose();
                }
            }
            return checkResults;
        }

        /// <summary>
        /// Gets the result of the validation.
        /// </summary>
        /// <returns></returns>
        public int GetValidationResults()
        {
            string VarAccountID = CheckAccountNumberIfExists().ToString();
            string VarAccountPass = CheckPasswordIfExists().ToString();
            int VarTheResult = 0;

            if (VarAccountID == "True")
            {
                if (VarAccountPass == "True")
                {
                    VarTheResult = 1;
                }
                else
                {
                    VarTheResult = 0;
                }
            }
            else
            {
                VarTheResult = 0;
            }

            return VarTheResult;
        }

        public string GetUserID()
        {
            return LoginDetails.EmpNo;
        }
    }
}
