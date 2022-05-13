using DemoDatabase.Data;
using DemoDatabase.Domain;
using DemoDatabase.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using static DemoDatabase.Local.Settings;

namespace DemoDatabase.Controllers
{
    public class Client
    {
        private static readonly Logger _logger = Logger.GetInstance();

        public void Show(IEnumerable<Dto.Cliente> clientes)
        {
            try
            {
                _logger.Insert.Debug(JsonSerializer.Serialize(clientes));
            }
            catch (Exception exception)
            {
                _logger.Insert.Error(exception, exception.Message);
            }
        }

        public IEnumerable<Dto.Cliente> ConsultNoLinq()
        {
            Select querys = new Select();

            return new SQLite().Command(querys, "Cliente");
        }

        public IEnumerable<Dto.Cliente> ConsultLinq(string subQuery = null, int numberPage = 1, int sizePage = 10)
        {
            if (subQuery != null) subQuery = subQuery.ToLower();

            Select querys = new Select();

            return new SQLite().Command(querys, "Cliente")
                .Where(w => (subQuery == null) ||
                                           w.Id.ToString().Contains(subQuery) ||
                                           w.Name.ToString().Contains(subQuery) ||
                                           w.Address.ToString().Contains(subQuery) ||
                                           w.Movil.ToString().Contains(subQuery)
                         ).Skip((numberPage - 1) * sizePage)
                         .Take((numberPage * sizePage) - ((numberPage - 1) * sizePage));
        }

        public void ConsultClient()
        {
            var clientRepository = new ClientRepository();

            try
            {
                clientRepository.LoginInSqlServer("localhost,1433", "BD_VENTAS_020", "SA", "xxxxxx");

                var sqlCommand = "select ID_CLIENTE, NOMB_CLIENTE, DIRECCION from CLIENTES WHERE ID_CLIENTE <='100'";

                var rows = clientRepository.GetAll(sqlCommand);

                if (rows.Length < 1) return;

                foreach (var row in rows)
                {
                    Console.WriteLine(row.ToString());

                    _logger.Insert.Debug(JsonSerializer.Serialize(row));
                }

            }
            catch (Exception exception)
            {
                _logger.Insert.Error(exception, exception.Message);
            }
        }

        public void Create()
        {
            try
            {
                var querys = new Insert();

                var isExecute = new SQLite().Command(querys, "Emmanuel", "C:/2, #321", 8932312);

                _logger.Insert.Debug($"Cliente insertado: {isExecute}");
            }
            catch (Exception exception)
            {
                _logger.Insert.Error(exception, exception.Message);
            }
        }

        public void CreateBatchFailed()
        {
            try
            {
                _logger.Insert.Debug("Iniciando Method CreateBathFailed");

                var querys = new Insert();

                _logger.Insert.Debug("Iniciando Lista de Objetos Cliente");

                var clientes = new List<Dto.Cliente>
                {
                    new Dto.Cliente() { Name = "Teodoro", Address = "Martin #23", Movil = new Distributor().Movil },
                    new Dto.Cliente() { Name = "Jose", Address = "FR #15", Movil = new Distributor().Movil + 2 },
                    new Dto.Cliente() { Name = "Miguel", Address = "Naco #541", Movil = new Distributor().Movil + 3 },
                    new Dto.Cliente() { Name = "Hector", Address = "J. Duarte #1", Movil = new Distributor().Movil + 4},
                    new Dto.Cliente() { Name = "Roman", Address = "Medina #3", Movil = new Distributor().Movil + 5},
                    new Dto.Cliente() { Name = "Maria", Address = "Don Gregorio #68", Movil = new Distributor().Movil + 6 },
                    new Dto.Cliente() { Name = "Catia", Address = "Juan D. #26", Movil = new Distributor().Movil + 7 },
                    new Dto.Cliente() { Name = "Miguelina", Address = "Nature #98", Movil = new Distributor().Movil + 8 },
                    new Dto.Cliente() { Name = "Andreina", Address = "Martin #23", Movil = new Distributor().Movil }
                };

                _logger.Insert.Information($"El Objeto cliente: { JsonSerializer.Serialize(clientes) }");

                var isExecute = new SQLite().Command(querys, "Cliente", clientes);

                _logger.Insert.Debug($"Clientes insertados: {isExecute}");
            }
            catch (Exception exception)
            {
                _logger.Insert.Error(exception, exception.Message);
            }
        }

        public void CreateBatchSuccessful()
        {
            try
            {
                _logger.Insert.Debug("Iniciando Method CreateBathFailed");

                var querys = new Insert();

                _logger.Insert.Debug("Iniciando Lista de Objetos Cliente");

                var clientes = new List<Dto.Cliente>
                {
                    new Dto.Cliente() { Name = "Teodoro", Address = "Martin #23", Movil = new Distributor().Movil + 2 },
                    new Dto.Cliente() { Name = "Jose", Address = "FR #15", Movil = new Distributor().Movil + 4 },
                    new Dto.Cliente() { Name = "Miguel", Address = "Naco #541", Movil = new Distributor().Movil + 9 },
                    new Dto.Cliente() { Name = "Hector", Address = "J. Duarte #1", Movil = new Distributor().Movil + 10 },
                    new Dto.Cliente() { Name = "Roman", Address = "Medina #3", Movil = new Distributor().Movil },
                    new Dto.Cliente() { Name = "Maria", Address = "Don Gregorio #68", Movil = new Distributor().Movil + 15 },
                    new Dto.Cliente() { Name = "Catia", Address = "Juan D. #26", Movil = new Distributor().Movil + 110 },
                    new Dto.Cliente() { Name = "Miguelina", Address = "Nature #98", Movil = new Distributor().Movil + 12 },
                    new Dto.Cliente() { Name = "Andreina", Address = "Martin #23", Movil = new Distributor().Movil + 26 }
                };

                _logger.Insert.Debug($"El Objeto cliente: { JsonSerializer.Serialize(clientes) }");

                var isExecute = new SQLite().Command(querys, "Cliente", clientes);

                _logger.Insert.Debug($"Clientes insertados: {isExecute}");
            }
            catch (Exception exception)
            {
                _logger.Insert.Error(exception, exception.Message);
            }
        }

        public void Update()
        {
            try
            {
                var querys = new Update();

                var isExecute = new SQLite().Command(querys, 1, "Emmanuel", "C:/2-3b, #321");

                _logger.Insert.Debug($"Cliente Actualizado: {isExecute}");
            }
            catch (Exception exception)
            {
                _logger.Insert.Error(exception, exception.Message);
            }
        }

        public void Delete()
        {
            try
            {
                var querys = new Delete();

                var isExecute = new SQLite().Command(querys, "Cliente", "NAME", 3);

                _logger.Insert.Debug($"Cliente Eliminado: {isExecute}");
            }
            catch (Exception exception)
            {
                _logger.Insert.Error(exception, exception.Message);
            }
        }
    }
}