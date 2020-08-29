using Nikesh.Subscriber.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Nikesh.Subscriber.Services
{
    public class DataService : IDataService
    {
        
        private static string connectionString
        {
            get { return "Data Source=NIKESH-LAPTOP;Initial Catalog=Information;User id=sa; Password=admin; MultipleActiveResultSets=true"; }
        }
        private static SqlConnection con = new SqlConnection(connectionString);
        public void InsertData(InformationViewModel viewModel)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_Insert_Information", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", viewModel.FirstName);
                cmd.Parameters.AddWithValue("@LastName", viewModel.LastName);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Console.WriteLine("Data Inserted");
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                Console.WriteLine("Opps Something went wrong!",ex);
            }
        }
    }
}
