using DemoDatabase.Data;
using DemoDatabase.Infrastructure.Abstracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DemoDatabase.Domain
{
    public class ManagerRequestSqlServer : ManagerRequest
    {
        private readonly string _dataSource;
        private readonly string _dataBase;
        private readonly string _user;
        private readonly string _password;

        public ManagerRequestSqlServer(string dataSource, string dataBase, string user, string password)
            : base(dataSource, dataBase, user, password)
        {
            _dataSource = dataSource;
            _dataBase = dataBase;
            _user = user;
            _password = password;
        }

        public override IEnumerable<Dto.Cliente> Execute(Select querys, string table)
        {
            var queryAll = querys.All(table);

            var connectionString = new Connector(_dataSource, _dataBase, _user, _password).ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(queryAll, connection);

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

            var connectionString = new Connector(_dataSource, _dataBase, _user, _password).ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(update, connection);

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
            var delete = querys.Command(table, key, id);

            var connectionString = new Connector(_dataSource, _dataBase, _user, _password).ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(delete, connection);

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

            var connectionString = new Connector(_dataSource, _dataBase, _user, _password).ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(insert, connection);

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

        /// <summary>
        /// Insert < 1 MM of register
        /// </summary>
        /// <param name="querys"></param>
        /// <param name="table"></param>
        /// <param name="clientes"></param>
        public override bool Execute(Insert querys, string table, IEnumerable<Dto.Cliente> clientes)
        {
            var insert = querys.Command();

            var connectionString = new Connector(_dataSource, _dataBase, _user, _password).ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (var command = new SqlCommand())
                    {
                        command.Transaction = transaction;
                        command.CommandType = CommandType.Text;
                        command.CommandText = insert;
                        command.Parameters.Add("@NAME", SqlDbType.Text);
                        command.Parameters.Add("@ADDRESS", SqlDbType.Text);
                        command.Parameters.Add("@MOVIL", SqlDbType.Int);

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

        /// <summary>
        /// Insert > 1MM of register
        /// </summary>
        /// <param name="querys"></param>
        /// <param name="table"></param>
        /// <param name="clientes"></param>
        public bool ExecuteBulk(Insert querys, string table, IEnumerable<Dto.Cliente> clientes)
        {
            // Tabla Temporal
            var tableTemp = querys.Command(clientes);

            var connectionString = new Connector(_dataSource, _dataBase, _user, _password).ConnectionString;

            //insert to db
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                    {
                        try
                        {
                            bulkCopy.DestinationTableName = table;
                            bulkCopy.WriteToServer(tableTemp);

                            transaction.Commit();

                            return true;
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();

                            throw;
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