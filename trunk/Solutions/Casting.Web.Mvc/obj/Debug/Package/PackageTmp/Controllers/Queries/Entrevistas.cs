using System;
using System.Collections.Generic;
using System.Linq;
using Casting.Domain;
using NHibernate.Linq;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate;

namespace Casting.Web.Mvc.Controllers.Queries
{
    public class Entrevistas : NHibernateQuery, IEntrevistas
    {
        private readonly IRepository<Entrevista> _entrevista;

        public Entrevistas(IRepository<Entrevista> entrevista)
        {
            _entrevista = entrevista;
        }

        public void SaveEntrevista(Entrevista entrevista)
        {
            _entrevista.SaveOrUpdate(entrevista);
            _entrevista.DbContext.CommitChanges();
        }

        public int TotalEntrevistasEmAberto()
        {
            return (Session.Query<Entrevista>().Where(x => x.DataEntrevista >= DateTime.Now.AddDays(-1)).Select(
                entrevista => entrevista.Id)).Count();
        }

        public List<Entrevista> ListaEntrevistasEmAberto()
        {
            return (List<Entrevista>) Session.QueryOver<Entrevista>()
                .Where(x => x.DataEntrevista > DateTime.Now.AddDays(-1)).List();
        }

        public List<Entrevista> EntrevistasEmAbertoDaVaga(int vagaId)
        {
            return (List<Entrevista>)Session.QueryOver<Entrevista>()
                    .Where(x => x.DataEntrevista >= DateTime.Now.AddDays(-1))
                    .Where(x => x.Vaga.Id == vagaId).List();
        }

        public Entrevista EntrevistaMarcadaParaCandidato(int candidatoId)
        {
            var entrevista = Session.QueryOver<Entrevista>()
                .Where(x => x.DataEntrevista > DateTime.Now.AddDays(-1))
                .Where(x => x.Candidato.Id == candidatoId)
                .SingleOrDefault();

            return entrevista;
        }
    }
}