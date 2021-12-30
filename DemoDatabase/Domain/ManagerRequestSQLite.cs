using DemoDatabase.Data;
using DemoDatabase.Infrastructure.Abstracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace DemoDatabase.Domain
{
    public class ManagerRequestSQLite : ManagerRequest
    {
        private readonly string _path;
        private readonly int _version;
        private readonly string _password;

        public ManagerRequestSQLite(string dataSource, int version, string password = null)
            :base(dataSource, version, password)
        {
            _path = dataSource;
            _version = version;
            _password = password;
        }

        public override IEnumerable<Dto.Cliente> Execute(Select querys, string table)
        {
            var queryAll = querys.All(table);

            var connectionString = new Connector(_path, 3).ConnectionString;

            using (var connection = new SQLiteConnection(connectionString))
            {
                var command = new SQLiteCommand(queryAll, connection);

                try
                {
                    connection.Open();

                    var clientes = new List<Dto.Cliente>();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var cliente = new Dto.Cliente
                        {
                            Id = reader["ID"] != null ? Convert.ToInt32(reader["ID"]) : new Int32(),
                            Name = reader["NAME"] != null ? Convert.ToString(reader["NAME"]) : string.Empty,
                            Address = reader["ADDRESS"] != null ? Convert.ToString(reader["ADDRESS"]) : string.Empty,
                            Movil = reader["MOVIL"] != null ? Convert.ToInt32(reader["MOVIL"]) : new Int32()
                        };

                        clientes.Add(cliente);
                    }

                    reader.Close();

                    return clientes;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public override bool Execute(Update querys, Dto.Cliente cliente)
        {
            var update = querys.Command(cliente.Id, cliente.Name, cliente.Address);

            var connectionString = new Connector(_path, 3).ConnectionString;

            using (var connection = new SQLiteConnection(connectionString))
            {
                var command = new SQLiteCommand(update, connection);

                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery() == 1;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public override bool Execute(Delete querys, string table, string key, int id)
        {
            var update = querys.Command(table, key, id);

            var connectionString = new Connector(_path, 3).ConnectionString;

            using (var connection = new SQLiteConnection(connectionString))
            {
                var command = new SQLiteCommand(update, connection);

                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery() == 1;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public override bool Execute(Insert querys, Dto.Cliente cliente)
        {
            var insert = querys.Command(cliente.Name, cliente.Address, cliente.Movil);

            var connectionString = new Connector(_path, 3).ConnectionString;

            using (var connection = new SQLiteConnection(connectionString))
            {
                var command = new SQLiteCommand(insert, connection);

                try
                {
                    connection.Open();

                    return command.ExecuteNonQuery() == 1;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public override bool Execute(Insert querys, string table, IEnumerable<Dto.Cliente> clientes)
        {
            var insert = querys.Command();

            var connectionString = new Connector(_path, _version).ConnectionString;

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    using (var command = new SQLiteCommand())
                    {
                        command.Transaction = transaction;
                        command.CommandText = insert;
                        command.Parameters.Add("@NAME", DbType.String);
                        command.Parameters.Add("@ADDRESS", DbType.String);
                        command.Parameters.Add("@MOVIL", DbType.Int32);

                        try
                        {
                            foreach (var cliente in clientes)
                            {
                                command.Parameters["@NAME"].Value = cliente.Name;
                                command.Parameters["@ADDRESS"].Value = cliente.Address;
                                command.Parameters["@MOVIL"].Value = cliente.Movil;

                                command.ExecuteNonQuery();
                            }

                            transaction.Commit();

                            return true;
                        }
                        catch (Exception exception)
                        {
                            transaction.Rollback();

                            throw exception;
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }
    }
}