using DemoDatabase.Data;
using DemoDatabase.Domain;
using DemoDatabase.Local;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoDatabase
{
    public class Program
    {
        private static readonly string path = @"C:\Users\edelossantos\source\repos\DemoDatabase\DemoDatabase\Resources\pruebaDB.db";
        private static readonly int version = 3;

        static void Main(string[] args)
        {
            //Delete();
            //Show(ConsultLinq("ma"));

            CreateBatchSuccessful();

            Console.ReadKey();
        }

        static void Show(IEnumerable<Dto.Cliente> enumerable)
        {
            try
            {
                foreach (var cliente in enumerable)
                {
                    var json = new
                    {
                        cliente.Id,
                        cliente.Name,
                        cliente.Address,
                        cliente.Movil
                    };

                    Console.WriteLine(json.ToString());
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        static IEnumerable<Dto.Cliente> ConsultNoLinq()
        {
            Select querys = new Select();

            return new SQLite(path, version).Command(querys, "Cliente");
        }

        static IEnumerable<Dto.Cliente> ConsultLinq(string subQuery = null, int currentPage = 1, int pageSize = 10)
        {
            if (subQuery != null) subQuery = subQuery.ToLower();

            Select querys = new Select();

            return new SQLite(path, version).Command(querys, "Cliente")
                .Where(w => (subQuery == null) ||
                                           w.Id.ToString().Contains(subQuery) ||
                                           w.Name.ToString().Contains(subQuery) ||
                                           w.Address.ToString().Contains(subQuery) ||
                                           w.Movil.ToString().Contains(subQuery)
                         ).Skip((currentPage - 1) * pageSize)
                         .Take((currentPage * pageSize) - ((currentPage - 1) * pageSize));
        }

        static void Create()
        {
            try
            {
                var querys = new Insert();

                var isExecute = new SQLite(path, version).Command(querys, "Emmanuel", "C:/2, #321", 8932312);

                Console.WriteLine($"Cliente insertado: {isExecute}");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        static void CreateBatchfailed()
        {
            try
            {
                var querys = new Insert();

                var clientes = new List<Dto.Cliente>();

                clientes.Add(new Dto.Cliente() { Name = "Teodoro", Address = "Martin #23", Movil = new Distributor().Movil });
                clientes.Add(new Dto.Cliente() { Name = "Jose", Address = "FR #15", Movil = new Distributor().Movil });
                clientes.Add(new Dto.Cliente() { Name = "Miguel", Address = "Naco #541", Movil = new Distributor().Movil });
                clientes.Add(new Dto.Cliente() { Name = "Hector", Address = "J. Duarte #1", Movil = new Distributor().Movil });
                clientes.Add(new Dto.Cliente() { Name = "Roman", Address = "Medina #3", Movil = new Distributor().Movil });
                clientes.Add(new Dto.Cliente() { Name = "Maria", Address = "Don Gregorio #68", Movil = new Distributor().Movil });
                clientes.Add(new Dto.Cliente() { Name = "Catia", Address = "Juan D. #26", Movil = new Distributor().Movil });
                clientes.Add(new Dto.Cliente() { Name = "Miguelina", Address = "Nature #98", Movil = new Distributor().Movil });
                clientes.Add(new Dto.Cliente() { Name = "Andreina", Address = "Martin #23", Movil = new Distributor().Movil });

                var isExecute = new SQLite(path, version).Command(querys, "Cliente", clientes);

                Console.WriteLine($"Clientes insertados: {isExecute}");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        static void CreateBatchSuccessful()
        {
            try
            {
                var querys = new Insert();

                var clientes = new List<Dto.Cliente>();

                clientes.Add(new Dto.Cliente() { Name = "Teodoro", Address = "Martin #23", Movil = new Distributor().Movil + 2});
                clientes.Add(new Dto.Cliente() { Name = "Jose", Address = "FR #15", Movil = new Distributor().Movil + 4});
                clientes.Add(new Dto.Cliente() { Name = "Miguel", Address = "Naco #541", Movil = new Distributor().Movil + 9});
                clientes.Add(new Dto.Cliente() { Name = "Hector", Address = "J. Duarte #1", Movil = new Distributor().Movil + 10});
                clientes.Add(new Dto.Cliente() { Name = "Roman", Address = "Medina #3", Movil = new Distributor().Movil });
                clientes.Add(new Dto.Cliente() { Name = "Maria", Address = "Don Gregorio #68", Movil = new Distributor().Movil + 15});
                clientes.Add(new Dto.Cliente() { Name = "Catia", Address = "Juan D. #26", Movil = new Distributor().Movil + 110});
                clientes.Add(new Dto.Cliente() { Name = "Miguelina", Address = "Nature #98", Movil = new Distributor().Movil + 12});
                clientes.Add(new Dto.Cliente() { Name = "Andreina", Address = "Martin #23", Movil = new Distributor().Movil + 26});

                var isExecute = new SQLite(path, version).Command(querys, "Cliente", clientes);

                Console.WriteLine($"Clientes insertados: {isExecute}");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        static void Update()
        {
            try
            {
                var querys = new Update();

                var isExecute = new SQLite(path, version).Command(querys, 1, "Emmanuel", "C:/2-3b, #321");

                Console.WriteLine($"Cliente Actualizado: {isExecute}");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        static void Delete()
        {
            try
            {
                var querys = new Delete();

                var isExecute = new SQLite(path, version).Command(querys, "Cliente", "NAME", 3);

                Console.WriteLine($"Cliente Eliminado: {isExecute}");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}