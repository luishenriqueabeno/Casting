using System.Collections;
using System.Collections.Generic;
using Casting.Domain;
using Casting.Web.Mvc.Controllers.ViewModels;
using FluentNHibernate.Testing.Values;
using SharpArch.Domain.PersistenceSupport;

namespace Casting.Web.Mvc.Controllers.Queries
{
    public interface ICandidatos
    {
        Candidato GetCandidato(int id);
        Candidato GetCandidatoByUsuarioId(int usuario);
        void SaveCandidato(Candidato candidato);
        void RemoverFuncoesDoCandidato(int id);
        void RemoverSegmentosDoCandidato(int id);
        List<Candidato> PesquisaCandidatos(string cpf, string nome, string email, string telefoneCelular);

        List<Candidato> PesquisaCandidatos(AuxiliarDePesquisaViewModel auxiliar, int pagina,
                                   int numeroDeRegistros, string orderCol = "", string orderDir = "");

        List<Candidato> PesquisaCandidatosAvancada(AuxiliarDePesquisaViewModel auxiliar, int pagina,
                                   int numeroDeRegistros, string orderCol = "", string orderDir = "");

        int PesquisaCandidatosTotal(AuxiliarDePesquisaViewModel auxiliar);
        int PesquisaCandidatosAvancadaTotal(AuxiliarDePesquisaViewModel auxiliar);

        int TotalCandidatos();
        int TotalCandidatosAnoAtual();
        int TotalCandidatosMesAtual();
        IList TotalCandidatosPorOrigem();
        bool VerificaSeCandidatoJaExiste(string Cpf);
    }
}