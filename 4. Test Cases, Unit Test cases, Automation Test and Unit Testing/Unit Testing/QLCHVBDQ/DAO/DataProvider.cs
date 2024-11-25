using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLCHVBDQ.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get
            {
                if (instance == null) instance = new DataProvider(); return DataProvider.instance;
            }

            private set { DataProvider.instance = value; }
        }

        private DataProvider() { }

        private string connectionSTR = "Data Source=DESKTOP-M09ROU4;Initial Catalog=QLVBDQ2;Integrated Security=True";

        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }

                connection.Close();
            }
            return data;
        }

        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                try
                {
                    data = command.ExecuteNonQuery();
                }
                catch (Exception exp)
                {
                    data = -1;
                    MessageBox.Show(exp.Message);
                }

                connection.Close();
            }
            return data;
        }

        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                try
                {
                    data = command.ExecuteScalar();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }

                connection.Close();
            }
            return data;
        }
    }
}
