using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Casting.Domain;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate;

namespace Casting.Web.Mvc.Controllers.Queries
{
    public class Clientes : NHibernateQuery, IClientes
    {
        private readonly IRepository<Cliente> _cliente;

        public Clientes(IRepository<Cliente> cliente)
        {
            _cliente = cliente;
        }

        public void SaveCliente(Cliente cliente)
        {
            _cliente.SaveOrUpdate(cliente);
            _cliente.DbContext.CommitChanges();
        }

        public IList<Cliente> ListaClientes()
        {
            var clientes = Session.QueryOver<Cliente>().List();
            return clientes;
        }
    }
}