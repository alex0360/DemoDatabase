using System;
using System.Data;
using System.Data.SqlClient;

namespace DemoDatabase.Data
{
    public class ClientRepository
    {
        private readonly string UMLAUT = "¨"; //Hex = 00A8; //Dec = 0168; //Entidad = &uml; //Carácter = diéresis

        private SqlConnection sqlConnection = null;

        public void LoginInSqlServer(string dataSource, string dataBase, string user, string password)
        {
            try
            {
                sqlConnection = new SqlConnection($"Data Source={dataSource}; Initial Catalog={dataBase}; User Id ={user}; Password={password}");

                sqlConnection.Open();
            }
            catch(SqlException exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                if(sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        public string[] GetAll(string sqlCommand)
        {
            string[] resultOfQuery = { " " };

            Array.Resize(ref resultOfQuery, 1);

            if(sqlConnection.ConnectionString == string.Empty)
            {
                throw new Exception("No ha hecho login");
            }

            try
            {
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
                    resultOfQuery[resultOfQuery.Length - 1] = temporaryRecord;

                    Array.Resize(ref resultOfQuery, resultOfQuery.Length + 1);
                }

                records.Close();

                Array.Resize(ref resultOfQuery, resultOfQuery.Length - 1);

                return resultOfQuery;
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