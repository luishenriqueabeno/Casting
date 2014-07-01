using System.Collections.Generic;
using Casting.Domain;

namespace Casting.Web.Mvc.Controllers.Queries
{
    public interface IRecrutamentos
    {
        void SaveRecrutamento(Recrutamento recrutamento);
        Recrutamento GetRecrutamento(Vaga vaga, Candidato candidato);
        Recrutamento GetRecrutamentoAtualDoCandidato(Candidato candidato);
        int TotalRecrutamentosUltimoMes();
        int TotalRecrutamentosMesAtual();
        List<Recrutamento> RecrutamentosDaVaga(int vagaId);

        IList<Recrutamento> ListaRecrutamentosPorCandidato(int candidatoId);
    }
}