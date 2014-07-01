using System.Collections.Generic;
using Casting.Domain;
using SharpArch.Domain.PersistenceSupport;

namespace Casting.Web.Mvc.Controllers.Queries
{
    public interface IVagas
    {
        void SaveVaga(Vaga vaga);
        IList<Vaga> ListaVagas();
        IList<Vaga> ListaVagasPorCliente(int idCliente);

        IList<Vaga> PesquisarVagas(string titulo, int clienteId, int pagina,
                                   int numeroDeRegistros, string orderCol = "", string orderDir = "");

        int TotalDeVagasPesquisadas(string titulo, int clienteId);

        Vaga PesquisarVaga(int idVaga);
        int PesquisaVagasEmAberto();
        int PesquisaVagasPreenchidas();

    }
}