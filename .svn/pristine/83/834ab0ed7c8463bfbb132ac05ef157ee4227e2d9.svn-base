using System.Collections.Generic;
using Casting.Domain;

namespace Casting.Web.Mvc.Controllers.Queries
{
    public interface IAvaliacoes
    {
        IList<Avaliacao> ListaAvaliacoes();
        IList<CabeloTipo> ListaTiposCabelo();
        IList<CabeloCor> ListaCoresCabelo();
        IList<CabeloComprimento> ListaComprimentosCabelo();
        IList<TipoFisico> ListaTipoFisicos();
        IList<AvaliacaoDentesSorriso> ListaAvaliacaoDentesSorriso();
        IList<AvaliacaoDentesCor> ListaAvaliacaoDentesCor();
        IList<TipoDePele> ListaTiposDePele();
        IList<PerfilComportalmental> ListaPerfilComportalmental();
        IList<PerfilProfissional> ListaPerfilProfissional();
        IList<ConhecimentoMerchandising> ListaConhecimentoMerchandising();
        IList<Avaliacao> ListaAvaliacoesPorCandidato(int candidatoId);
        PerfilComportalmental GetPerfilComp(int id);
        PerfilProfissional GetPerfilProf(int id);
        ConhecimentoMerchandising GetConhecimentoMerchan(int id);
        void SaveAvaliacao(Avaliacao avaliacao);
        Avaliacao GetAvaliacao(int id);
        Avaliacao GetUltimaAvaliacao(int candidatoId);
        IList<ProjetosParticipados> ListaProjetosParticipados();

    }
}