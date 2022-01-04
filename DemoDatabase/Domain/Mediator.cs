using DemoDatabase.Data;
using System.Collections.Generic;
using static DemoDatabase.Local.Settings;

namespace DemoDatabase.Domain
{
    public class SQLite
    {
        private readonly string _path = Parameterize.Path;
        private readonly int _version = Parameterize.Version;
        private readonly string _password = Parameterize.PasswordLite;

        public SQLite() { }

        public SQLite(string dataSource, int version, string password)
        {
            _path = dataSource;
            _version = version;
            _password = password;
        }

        public IEnumerable<Dto.Cliente> Command(Select querys, string table)
        {
            return new ManagerRequestSQLite(_path, _version, _password).Execute(querys, table);
        }

        public bool Command(Insert querys, string name, string address, int movil)
        {
            var cliente = new Dto.Cliente()
            {
                Name = name,
                Address = address,
                Movil = movil
            };

            return new ManagerRequestSQLite(_path, _version, _password).Execute(querys, cliente);
        }

        public bool Command(Update querys, int id, string name, string address)
        {
            var cliente = new Dto.Cliente()
            {
                Id = id,
                Name = name,
                Address = address
            };

            return new ManagerRequestSQLite(_path, _version, _password).Execute(querys, cliente);
        }

        public bool Command(Delete querys, string table, string key, int id)
        {
            return new ManagerRequestSQLite(_path, _version, _password).Execute(querys, table, key, id);
        }

        public bool Command(Insert querys, string table, IEnumerable<Dto.Cliente> clientes)
        {
            return new ManagerRequestSQLite(_path, _version, _password).Execute(querys, table, clientes);
        }
    }

    public class SqlServer
    {
        private readonly string _dataSource = Parameterize.DataSource;
        private readonly string _dataBase = Parameterize.DataBase;
        private readonly string _user = Parameterize.UserId;
        private readonly string _password = Parameterize.PasswordServer;

        public SqlServer() { }

        public SqlServer(string dataSource, string dataBase, string user, string password)
        {
            _dataSource = dataSource;
            _dataBase = dataBase;
            _user = user;
            _password = password;
        }

        public IEnumerable<Dto.Cliente> Command(Select querys, string table)
        {
            return new ManagerRequestSqlServer(_dataSource, _dataBase, _user, _password).Execute(querys, table);
        }

        public bool Command(Insert querys, string name, string address, int movil)
        {
            var cliente = new Dto.Cliente()
            {
                Name = name,
                Address = address,
                Movil = movil
            };

            return new ManagerRequestSqlServer(_dataSource, _dataBase, _user, _password).Execute(querys, cliente);
        }

        public bool Command(Update querys, int id, string name, string address)
        {
            var cliente = new Dto.Cliente()
            {
                Id = id,
                Name = name,
                Address = address
            };

            return new ManagerRequestSqlServer(_dataSource, _dataBase, _user, _password).Execute(querys, cliente);
        }

        public bool Command(Delete querys, string table, string key, int id)
        {
            return new ManagerRequestSqlServer(_dataSource, _dataBase, _user, _password).Execute(querys, table, key, id);
        }

        public bool Command(Insert querys, string table, IEnumerable<Dto.Cliente> clientes)
        {
            return new ManagerRequestSqlServer(_dataSource, _dataBase, _user, _password).Execute(querys, table, clientes);
        }

        public bool CommandBulk(Insert querys, string table, IEnumerable<Dto.Cliente> clientes)
        {
            return new ManagerRequestSqlServer(_dataSource, _dataBase, _user, _password).ExecuteBulk(querys, table, clientes);
        }
    }
}