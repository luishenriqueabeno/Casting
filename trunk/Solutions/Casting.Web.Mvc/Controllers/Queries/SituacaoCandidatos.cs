using System.Collections.Generic;
using Casting.Domain;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate;

namespace Casting.Web.Mvc.Controllers.Queries
{
    public class SituacaoCandidatos : NHibernateQuery, ISituacaoCandidatos
    {
        
        private readonly IRepository<SituacaoCandidato> _situacao;

        public SituacaoCandidatos(IRepository<SituacaoCandidato> situacao)
        {
            _situacao = situacao;
        }

        public void SaveSituacao(SituacaoCandidato situacaoCandidato)
        {
            _situacao.SaveOrUpdate(situacaoCandidato);
            _situacao.DbContext.CommitChanges();
        }

        public IList<SituacaoCandidato> ListaSituacaoCandidato()
        {
            return Session.QueryOver<SituacaoCandidato>().List();
        }

        public SituacaoCandidato GetSituacao(int id)
        {
            return Session.QueryOver<SituacaoCandidato>()
                .Where(x => x.Id == id)
                .SingleOrDefault();
        }
    }
}