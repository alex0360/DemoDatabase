using System.Collections.Generic;
using System.Data;

namespace DemoDatabase.Data
{
    public class Insert
    {
        public string Command(string name, string address, int movil) =>
            $"INSERT INTO Cliente(NAME, ADDRESS, MOVIL) Values ('{name}', '{address}', {movil})";

        public string Command() =>
            $"INSERT INTO Cliente(NAME, ADDRESS, MOVIL) Values (@NAME, @ADDRESS, @MOVIL);";

        public DataTable Command(IEnumerable<Dto.Cliente> clientes)
        {
            // Tabla Temporal
            var table = new DataTable();

            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("NAME", typeof(string));
            table.Columns.Add("ADDRESS", typeof(int));
            table.Columns.Add("MOVIL", typeof(float));

            foreach (var cliente in clientes)
            {
                table.Rows.Add(new object[]
                    {
                        cliente.Id,
                        cliente.Name,
                        cliente.Address,
                        cliente.Movil
                });
            }

            return table;
        }
    }
}