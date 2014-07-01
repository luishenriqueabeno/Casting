using System.Collections.Generic;
using Casting.Domain;

namespace Casting.Web.Mvc.Controllers.Queries
{
    public interface IEntrevistas
    {
        void SaveEntrevista(Entrevista entrevista);
        int TotalEntrevistasEmAberto();
        List<Entrevista> ListaEntrevistasEmAberto();
        List<Entrevista> EntrevistasEmAbertoDaVaga(int vagaId);
        Entrevista EntrevistaMarcadaParaCandidato(int candidatoId);
    }
}