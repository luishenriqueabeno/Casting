using System.Collections.Generic;
using Casting.Domain;
using SharpArch.Domain.PersistenceSupport;

namespace Casting.Web.Mvc.Controllers.Queries
{
    public interface IClientes
    {
        void SaveCliente(Cliente cliente);
        IList<Cliente> ListaClientes();
    }
}