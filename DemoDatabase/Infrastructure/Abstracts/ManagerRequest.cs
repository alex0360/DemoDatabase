using DemoDatabase.Data;
using System.Collections.Generic;

namespace DemoDatabase.Infrastructure.Abstracts
{
    public abstract class ManagerRequest
    {

        public ManagerRequest(string dataSource, int version, string password = null)
        {

        }

        public ManagerRequest(string dataSource, string dataBase, string user, string password)
        {

        }

        public virtual IEnumerable<Dto.Cliente> Execute(Select querys, string table)
        {
            return null;
        }

        public virtual bool Execute(Update querys, Dto.Cliente cliente)
        {
            return true;
        }

        public virtual bool Execute(Delete querys, string table, string key, int id)
        {
            return true;
        }

        public virtual bool Execute(Insert querys, Dto.Cliente cliente)
        {
            return true;
        }

        public virtual bool Execute(Insert querys, string table, IEnumerable<Dto.Cliente> clientes) 
        {
            return true;
        }
    }
}
