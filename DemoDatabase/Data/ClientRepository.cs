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

            //AQUI DEBEMOS COLOCAR LA INSTRUCCION PARA DIMENSIONAR EL ARREGLO EN CERO (0)
            Array.Resize(ref result, 1);

            //VALIDO A VER SI EL STRING CONNECTION (SC) TIENE DATOS
            if(SqlConnectionString == string.Empty)
            {
                throw new Exception("No ha hecho login");
            }

            //EN CASO DE TENER.. DEFINO LA VARIABLE CON LA QUE ME VOY A COMUNICAR CON LA BD
            var sqlConnection = new SqlConnection();

            try
            {
                //ASIGNO EL SC A LA VARIABLE DE CONEXIÓN
                sqlConnection.ConnectionString = SqlConnectionString;
                //APERTURO LA CONEXIÓN
                sqlConnection.Open();
                //SI NO HUBO ERROR.. DEFINO LA VARIABLE CON LA QUE EJECUTARÉ EL COMANDO REIBIDO COMO PARAMETRO
                var cmd = new SqlCommand(sqlCommand, sqlConnection);
                //EJECUTO LA INSTRUCCIÓN Y EL RESULTADO LO GUARDO EN LA VARIABLE "REGISTRO"
                var records = cmd.ExecuteReader();

                //HAGO UN RECORRIDO REGISTRO X REGISTRO DE LA VARIABLE "REGISTRO"
                while(records.Read())
                {
                    //LIMPIAMOS LA VARIABLES QUE ACUMULARA LOS CAMPOS DEL REGISTRO
                    var temporaryRecord = string.Empty;

                    //POR CADA CAMPO QUE TENGA EL REGISTRO QUE SE ESTA LEYENDO EN ESTE PASE
                    for(int i = 0; i < records.FieldCount; i++)
                    {
                        //ACUMULO EN LA VARIABLE "strRegistro" EL VALOR DEL CAMPO EN LA POSICION "i" DEL REGISTRO
                        temporaryRecord = temporaryRecord + records.GetValue(i) + UMLAUT;
                    }
                    //AQUI GUARDO EN LA MAYOR POSICIÓN DEL ARREGLO (INICIALMENTE ES 0) EL CONTENIDO DE LA VARIABLE: strRegistro
                    result[result.Length - 1] = temporaryRecord;

                    //ACA INCREMENTO EN 1 EL TRAMAÑO DEL ARREGLO, PERO!!!!! "PRESERVANDO EL CONTENIDO" QUE HASTA ESE MOMENTO HAYA ACUMULADO
                    Array.Resize(ref result, result.Length + 1);
                }

                records.Close();

                //ACA DISMINUYO EN 1 EL TRAMAÑO DEL ARREGLO, YA QUE LA ULT. POSICIÓN ESTA EN BLANCO
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
