using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Casting.Domain;
using Casting.Domain.enums;
using Casting.Web.Mvc.Controllers.ViewModels;
using NHibernate.Criterion;
using NHibernate.Linq;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate;


namespace Casting.Web.Mvc.Controllers.Queries
{
    public class Candidatos : NHibernateQuery, ICandidatos
    {
        private readonly IRepository<Candidato> _candidato;

        public Candidatos(IRepository<Candidato> candidato)
        {
            _candidato = candidato;
        }

        public Candidato GetCandidato(int id)
        {
            return Session.QueryOver<Candidato>()
                .Where(x => x.Id == id)
                .SingleOrDefault();
        }

        public void SaveCandidato(Candidato candidato)
        {
            _candidato.SaveOrUpdate(candidato);
            _candidato.DbContext.CommitChanges();
        }

        public void RemoverFuncoesDoCandidato(int id)
        {
            var candidato = GetCandidato(id);
            var funcaoCount = candidato.Funcoes.Count();

            for (var i = 0; i < funcaoCount; i++ )
            {
                candidato.RemoverFuncao(candidato.Funcoes.First());
            }
            
        }

        public void RemoverSegmentosDoCandidato(int id)
        {
            var candidato = GetCandidato(id);
            var segmentoCount = candidato.Segmentos.Count();

            for (var i = 0; i < segmentoCount; i++)
            {
                candidato.RemoverSegmento(candidato.Segmentos.First());
            }

        }

        public Candidato GetCandidatoByUsuarioId(int usuarioId)
        {
            return Session.QueryOver<Candidato>()
                .Where(x => x.Usuario.Id == usuarioId)
                .SingleOrDefault();
        }

        public List<Candidato> PesquisaCandidatos(string cpf, string nome, string email, string telefoneCelular)
        {
            return (List<Candidato>) Session.QueryOver<Candidato>()
                .Where(candidato => candidato.Cpf == cpf || cpf == "")
                .Where(new LikeExpression("Nome", "%" + nome + "%"))
                .Where(new LikeExpression("Email", "%" + email + "%"))
                .Where(candidato => candidato.TelefoneCelular == telefoneCelular || telefoneCelular == "")
                .List();
        }

        public List<Candidato> PesquisaCandidatos(AuxiliarDePesquisaViewModel auxiliar, int pagina,
                                   int numeroDeRegistros, string orderCol = "", string orderDir = "")
        {
            var condicao = auxiliar.ConstroiConsultaNormal();
            var primeiroResultado = pagina > 1 ? (pagina - 1) * numeroDeRegistros : 0;

            var listaDeCandidatos = 
                Session.CreateSQLQuery("Select distinct(c.Id) from Candidatos c " + 
                                            "left join ExperienciaProfissionalToCandidato ec on c.id = ec.Candidato_id " +
                                            "left join ExperienciaProfissionals e on e.id = ec.ExperienciaProfissional_id " +
                                            "left join FuncaoToCandidato fc on c.id = fc.Candidato_id " +
                                            "left join Funcaos f on f.id = fc.Funcao_id " +
                                            "left join CandidatosToSegmentos sc on sc.Candidato_id = c.id " +
                                            "left join Segmentos s on s.id = sc.Segmento_id " +
                                            string.Format("{0} {1} ", (condicao != "") ? "where " : "", condicao)).List();
            
            return (List<Candidato>) Session.QueryOver<Candidato>()
                .Where(x => x.Id.IsIn(listaDeCandidatos))
                .Skip(primeiroResultado)
                .Take(numeroDeRegistros)
                .List();
        }

        public List<Candidato> PesquisaCandidatosAvancada(AuxiliarDePesquisaViewModel auxiliar, int pagina,
                                   int numeroDeRegistros, string orderCol = "", string orderDir = "")
        {
            var condicao = auxiliar.ConstroiConsultaAvancada();
            var primeiroResultado = pagina > 1 ? (pagina - 1) * numeroDeRegistros : 0;

            var listaDeCandidatos =
                Session.CreateSQLQuery("Select distinct(c.Id) from Candidatos c " +
                                            "left join ExperienciaProfissionalToCandidato ec on c.id = ec.Candidato_id " +
                                            "left join ExperienciaProfissionals e on e.id = ec.ExperienciaProfissional_id " +
                                            "left join FuncaoToCandidato fc on c.id = fc.Candidato_id " +
                                            "left join Funcaos f on f.id = fc.Funcao_id " +
                                            "left join CandidatosToSegmentos sc on sc.Candidato_id = c.id " +
                                            "left join Segmentos s on s.id = sc.Segmento_id " +
                                            "left join Avaliacaos a on a.Candidato_Id = c.Id " +
                                            "left join AvaliacaosToConhecimentoMerchandisings acm on a.Id = acm.Avaliacao_Id " +
                                            "left join ConhecimentoMerchandisings cm on cm.Id = acm.ConhecimentoMerchandising_Id " +
                                            "left join AvaliacaosToPerfilProfissionals app on a.Id = app.Avaliacao_Id " +
                                            "left join PerfilProfissionals pp on pp.Id = app.PerfilProfissional_Id " +
                                            "left join PerfilComportalmentalToAvaliacao apc on a.Id = apc.Avaliacao_Id " +
                                            "left join PerfilComportalmentals pc on pc.Id = apc.PerfilComportalmental_Id " +
                                            "left join Recrutamentos rec on rec.Candidato_Id = c.Id " +
                                            string.Format("{0} {1} ", (condicao != "") ? "where " : "", condicao)).List();

            return (List<Candidato>)Session.QueryOver<Candidato>()
                              .Where(x => x.Id.IsIn(listaDeCandidatos))
                              .Skip(primeiroResultado)
                              .Take(numeroDeRegistros)
                              .List();

        }

        public int PesquisaCandidatosTotal(AuxiliarDePesquisaViewModel auxiliar)
        {
            var condicao = auxiliar.ConstroiConsultaNormal();
            
            var listaDeCandidatos =
                Session.CreateSQLQuery("Select distinct(c.Id) from Candidatos c " +
                                            "left join ExperienciaProfissionalToCandidato ec on c.id = ec.Candidato_id " +
                                            "left join ExperienciaProfissionals e on e.id = ec.ExperienciaProfissional_id " +
                                            "left join FuncaoToCandidato fc on c.id = fc.Candidato_id " +
                                            "left join Funcaos f on f.id = fc.Funcao_id " +
                                            "left join CandidatosToSegmentos sc on sc.Candidato_id = c.id " +
                                            "left join Segmentos s on s.id = sc.Segmento_id " +
                                            string.Format("{0} {1} ", (condicao != "") ? "where " : "", condicao)).List();

            return Session.QueryOver<Candidato>().Where(x => x.Id.IsIn(listaDeCandidatos)).List().Count;
        }

        public int PesquisaCandidatosAvancadaTotal(AuxiliarDePesquisaViewModel auxiliar)
        {
            var condicao = auxiliar.ConstroiConsultaAvancada();

            var listaDeCandidatos =
                Session.CreateSQLQuery("Select distinct(c.Id) from Candidatos c " +
                                            "left join ExperienciaProfissionalToCandidato ec on c.id = ec.Candidato_id " +
                                            "left join ExperienciaProfissionals e on e.id = ec.ExperienciaProfissional_id " +
                                            "left join FuncaoToCandidato fc on c.id = fc.Candidato_id " +
                                            "left join Funcaos f on f.id = fc.Funcao_id " +
                                            "left join CandidatosToSegmentos sc on sc.Candidato_id = c.id " +
                                            "left join Segmentos s on s.id = sc.Segmento_id " +
                                            "left join Avaliacaos a on a.Candidato_Id = c.Id " +
                                            "left join AvaliacaosToConhecimentoMerchandisings acm on a.Id = acm.Avaliacao_Id " +
                                            "left join ConhecimentoMerchandisings cm on cm.Id = acm.ConhecimentoMerchandising_Id " +
                                            "left join AvaliacaosToPerfilProfissionals app on a.Id = app.Avaliacao_Id " +
                                            "left join PerfilProfissionals pp on pp.Id = app.PerfilProfissional_Id " +
                                            "left join PerfilComportalmentalToAvaliacao apc on a.Id = apc.Avaliacao_Id " +
                                            "left join PerfilComportalmentals pc on pc.Id = apc.PerfilComportalmental_Id " +
                                            "left join Recrutamentos rec on rec.Candidato_Id = c.Id " +
                                            string.Format("{0} {1} ", (condicao != "") ? "where " : "", condicao)).List();

            return Session.QueryOver<Candidato>().Where(x => x.Id.IsIn(listaDeCandidatos)).List().Count;
        }

        public int TotalCandidatos()
        {
            return (from candidato in Session.Query<Candidato>()
                    select candidato.Id).Count();
        }

        public int TotalCandidatosAnoAtual()
        {
            return (Session.Query<Candidato>().Where(x => x.DataCadastro.Value.Year == DateTime.Today.Year).Select(
                candidato => candidato.Id)).Count();
        }

        public int TotalCandidatosMesAtual()
        {
            return (Session.Query<Candidato>().Where(x => x.DataCadastro.Value.Month == DateTime.Today.Month).Select(
                candidato => candidato.Id)).Count();
        }

        public IList TotalCandidatosPorOrigem()
        {
            return Session.CreateCriteria(typeof (Candidato))
                   .SetProjection(Projections.ProjectionList()
                                   .Add(Projections.Property("ComoFicouSabendoDaVaga"), "Origem")
                                   .Add(Projections.Count("Id"), "Total")
                                   .Add(Projections.GroupProperty("ComoFicouSabendoDaVaga"))).List();
        }

        public bool VerificaSeCandidatoJaExiste(string Cpf)
        {
            var candidato = Session.QueryOver<Candidato>()
                .Where(x => x.Cpf == Cpf).List();

            return candidato.Any();
        }

        public static string GerarSenhaCriptografada(string senha)
        {
            var md5 = new MD5CryptoServiceProvider();
            var arrayDeBytesDaString = Encoding.UTF8.GetBytes(senha);
            arrayDeBytesDaString = md5.ComputeHash(arrayDeBytesDaString);
            var senhaCriptografada = new StringBuilder();

            foreach (var b in arrayDeBytesDaString)
            {
                senhaCriptografada.Append(b.ToString("x2").ToLower());
            }
            return senhaCriptografada.ToString();
        }
        
    }
}