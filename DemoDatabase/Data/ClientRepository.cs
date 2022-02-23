using System;
using System.Data;
using System.Data.SqlClient;

namespace DemoDatabase.Data
{
    public class ClientRepository
    {
        private string SqlConnectionString { get; set; }
        private readonly string UMLAUT = "¨"; //Hex = 00A8; //Dec = 0168; //Entidad = &uml; //Carácter = diéresis

        public void LoginInSqlServer(string dataSource, string dataBase, string user, string password)
        {
            string conString = $"Data Source={dataSource}; Initial Catalog={dataBase}; User Id ={user}; Password={password}";

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(conString);

                con.Open();

                SqlConnectionString = conString;
            }
            catch(SqlException exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                if(con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public string[] GetAll(string sqlCommand)
        {
            string[] result = { " " };

            Array.Resize(ref result, 1);

            if(SqlConnectionString == string.Empty)
            {
                throw new Exception("No ha hecho login");
            }

            var sqlConnection = new SqlConnection();

            try
            {
                sqlConnection.ConnectionString = SqlConnectionString;

                sqlConnection.Open();

                var cmd = new SqlCommand(sqlCommand, sqlConnection);

                var records = cmd.ExecuteReader();

                while(records.Read())
                {

                    var temporaryRecord = string.Empty;

                    for(int i = 0; i < records.FieldCount; i++)
                    {
                        temporaryRecord = temporaryRecord + records.GetValue(i) + UMLAUT;
                    }
                    result[result.Length - 1] = temporaryRecord;

                    Array.Resize(ref result, result.Length + 1);
                }

                records.Close();

                Array.Resize(ref result, result.Length - 1);

                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
