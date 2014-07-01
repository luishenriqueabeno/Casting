using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Casting.Domain;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate;

namespace Casting.Web.Mvc.Controllers.Queries
{
    public class Avaliacoes : NHibernateQuery, IAvaliacoes
    {
        private readonly IRepository<Avaliacao> _avaliacao;

        public Avaliacoes(IRepository<Avaliacao> avaliacao)
        {
            _avaliacao = avaliacao;
        }

        public Avaliacao GetAvaliacao(int id)
        {
            return Session.QueryOver<Avaliacao>()
                .Where(x => x.Id == id)
                .SingleOrDefault();
        }

        public Avaliacao GetUltimaAvaliacao(int candidatoId)
        {
            return Session.QueryOver<Avaliacao>()
                .Where(x => x.Candidato.Id == candidatoId)
                .OrderBy(x => x.DataEntrevista).Desc
                .List().First();
        }
        public IList<Avaliacao> ListaAvaliacoes()
        {
            return Session.QueryOver<Avaliacao>().List();
        }

        public IList<CabeloTipo> ListaTiposCabelo()
        {
            return Session.QueryOver<CabeloTipo>().List();
        }

        public IList<CabeloCor> ListaCoresCabelo()
        {
            return Session.QueryOver<CabeloCor>().List();
        }

        public IList<CabeloComprimento> ListaComprimentosCabelo()
        {
            return Session.QueryOver<CabeloComprimento>().List();
        }

        public IList<TipoFisico> ListaTipoFisicos()
        {
            return Session.QueryOver<TipoFisico>().List();
        }

        public IList<AvaliacaoDentesSorriso> ListaAvaliacaoDentesSorriso()
        {
            return Session.QueryOver<AvaliacaoDentesSorriso>().List();
        }

        public IList<AvaliacaoDentesCor> ListaAvaliacaoDentesCor()
        {
            return Session.QueryOver<AvaliacaoDentesCor>().List();
        }

        public IList<TipoDePele> ListaTiposDePele()
        {
            return Session.QueryOver<TipoDePele>().List();
        }

        public IList<PerfilComportalmental> ListaPerfilComportalmental()
        {
            return Session.QueryOver<PerfilComportalmental>().List();
        }

        public IList<PerfilProfissional> ListaPerfilProfissional()
        {
            return Session.QueryOver<PerfilProfissional>().List();
        }

        public IList<ConhecimentoMerchandising> ListaConhecimentoMerchandising()
        {
            return Session.QueryOver<ConhecimentoMerchandising>().List();
        }

        public IList<Avaliacao> ListaAvaliacoesPorCandidato(int candidatoId)
        {
            return Session.QueryOver<Avaliacao>()
                .Where(x => x.Candidato.Id == candidatoId)
                .List();
        }

        public IList<ProjetosParticipados> ListaProjetosParticipados()
        {
            var query = "select V.Id, C.Nome,V.Titulo from Vagas as V ";
            query += "inner join Clientes as C on C.Id = V.Cliente_Id ";
            return Session
             .CreateSQLQuery(query)
             .AddEntity(typeof(ProjetosParticipados))
             .List<ProjetosParticipados>();

        }


        public PerfilComportalmental GetPerfilComp(int id)
        {
            return Session.QueryOver<PerfilComportalmental>()
                .Where(x => x.Id == id)
                .SingleOrDefault();
        }

        public PerfilProfissional GetPerfilProf(int id)
        {
            return Session.QueryOver<PerfilProfissional>()
                .Where(x => x.Id == id)
                .SingleOrDefault();
        }

        public ConhecimentoMerchandising GetConhecimentoMerchan(int id)
        {
            return Session.QueryOver<ConhecimentoMerchandising>()
                .Where(x => x.Id == id)
                .SingleOrDefault();
        }

        public void SaveAvaliacao(Avaliacao avaliacao)
        {
            _avaliacao.SaveOrUpdate(avaliacao);
            _avaliacao.DbContext.CommitChanges();
        }
    }
}