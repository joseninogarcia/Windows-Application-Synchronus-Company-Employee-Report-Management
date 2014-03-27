using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SychronusClass
{
    public class Inside : Connection
    {
        public string GetMyName(string id)
        {
            StringBuilder str = new StringBuilder();

            using (SqlConnection connect = new SqlConnection(connectionClass()))
            {
                try
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM Synch_EmpInfo WHERE SynchEmp_No = @username", connect);
                    command.Parameters.AddWithValue("@username", id);

                    SqlDataReader reader;

                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        str.Append(reader["SynchEmp_FirstName"].ToString() + " " + reader["SynchEmp_LastName"].ToString());
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
            return str.ToString();
        }
    }
}
