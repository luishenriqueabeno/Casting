using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Casting.Domain;
using NHibernate.Criterion;
using NHibernate.Linq;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate;

namespace Casting.Web.Mvc.Controllers.Queries
{
    public class Vagas : NHibernateQuery, IVagas
    {
        private readonly IRepository<Vaga> _vaga;

        public Vagas(IRepository<Vaga> vaga)
        {
            _vaga = vaga;
        }

        public IList<Vaga> ListaVagas()
        {
            var vaga = Session.QueryOver<Vaga>().List();
            return vaga;
        }

        public IList<Vaga> ListaVagasPorCliente(int idCliente)
        {
            return Session.QueryOver<Vaga>()
                .Where(x =>x.Cliente.Id == idCliente)
                .List();
        }

        public IList<Vaga> PesquisarVagas(string titulo, int clienteId, int pagina, 
            int numeroDeRegistros, string orderCol = "", string orderDir = "")
        {
            var primeiroResultado = pagina > 1 ? (pagina - 1) * numeroDeRegistros : 0;

            return Session.QueryOver<Vaga>()
                .Where(new LikeExpression("Titulo", "%" + titulo + "%"))
                .Where(x => x.Cliente.Id == clienteId || clienteId == 0)
                .Skip(primeiroResultado)
                .Take(numeroDeRegistros)
                .List();
        }

        public int TotalDeVagasPesquisadas(string titulo, int clienteId)
        {
            return Session.QueryOver<Vaga>()
                .Where(new LikeExpression("Titulo", "%" + titulo + "%"))
                .Where(x => x.Cliente.Id == clienteId || clienteId == 0)
                .List().Count;
        }

        public Vaga PesquisarVaga(int idVaga)
        {
            return Session.QueryOver<Vaga>()
                .Where(x => x.Id == idVaga || idVaga == 0)
                .SingleOrDefault();
        }

        public int PesquisaVagasEmAberto()
        {
            if (Session.QueryOver<Vaga>().List().Any())
            {
                return (from vale in Session.Query<Vaga>()
                        select vale.TotalVagasDisponiveis).Sum() - (from vale in Session.Query<Vaga>()
                                                                    select vale.TotalVagasPreenchidas).Sum();
            }

            return 0;
        }

        public int PesquisaVagasPreenchidas()
        {
            if (Session.QueryOver<Vaga>().List().Any())
            {
                return (from vale in Session.Query<Vaga>()
                            select vale.TotalVagasPreenchidas).Sum();
            }
            return 0;
        }
     
        public void SaveVaga(Vaga vaga)
        {
            _vaga.SaveOrUpdate(vaga);
            _vaga.DbContext.CommitChanges();
        }
    }
}