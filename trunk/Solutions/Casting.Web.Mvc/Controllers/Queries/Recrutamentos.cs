using System;
using System.Collections.Generic;
using System.Linq;
using Casting.Domain;
using Casting.Domain.enums;
using NHibernate.Linq;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate;

namespace Casting.Web.Mvc.Controllers.Queries
{
    public class Recrutamentos : NHibernateQuery, IRecrutamentos
    {
        private readonly IRepository<Recrutamento> _recrutamento;

        public Recrutamentos(IRepository<Recrutamento> recrutamento)
        {
            _recrutamento = recrutamento;
        }

        public void SaveRecrutamento(Recrutamento recrutamento)
        {
            _recrutamento.SaveOrUpdate(recrutamento);
            _recrutamento.DbContext.CommitChanges();
        }

        public Recrutamento GetRecrutamento(Vaga vaga, Candidato candidato)
        {
            return Session.QueryOver<Recrutamento>()
                .Where(x => x.Candidato == candidato)
                .Where(x => x.Vaga == vaga)
                .SingleOrDefault();
        }

        public Recrutamento GetRecrutamentoAtualDoCandidato(Candidato candidato)
        {
            return Session.QueryOver<Recrutamento>()
                .Where(x => x.Candidato == candidato)
                .Where(x => x.StatusCandidato == StatusCandidato.Aprovado || x.StatusCandidato == StatusCandidato.StandyBy)
                .SingleOrDefault();
        }

        public int TotalRecrutamentosUltimoMes()
        {
            return (Session.Query<Recrutamento>().Where(x => x.DataInicio.Value.Month == DateTime.Today.Month - 1 && x.StatusCandidato == StatusCandidato.Aprovado).Select(
                rec => rec.Id)).Count();
        }

        public int TotalRecrutamentosMesAtual()
        {
            return (Session.Query<Recrutamento>().Where(x => x.DataInicio.Value.Month == DateTime.Today.Month && x.StatusCandidato == StatusCandidato.Aprovado).Select(
                rec => rec.Id)).Count();
        }

        public List<Recrutamento> RecrutamentosDaVaga(int vagaId)
        {
            return (List<Recrutamento>)Session.QueryOver<Recrutamento>()
                                .Where(x => x.StatusCandidato == StatusCandidato.Aprovado)
                                .Where(x => x.Vaga.Id == vagaId).List();
        }

        public IList<Recrutamento> ListaRecrutamentosPorCandidato(int candidatoId)
        {
                return Session.QueryOver<Recrutamento>()
                .Where(x => x.Candidato.Id == candidatoId)
                .List();



        }
    }
}