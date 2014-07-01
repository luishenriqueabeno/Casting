using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Casting.Domain;
using Casting.Domain.enums;

namespace Casting.Web.Mvc.Controllers.ViewModels
{
    public class AvaliacaoViewModel
    {
        public int IdAvaliacao { get; set; }
        public int IdCandidato { get; set; }
        public string NomeCandidato { get; set; }
        public CabeloComprimento CabeloComprimento { get; set; }
        public CabeloTipo CabeloTipo { get; set; }
        public CabeloCor CabeloCor { get; set; }
        public TipoFisico TipoFisico { get; set; }
        public AvaliacaoDentesCor AvaliacaoDentesCor { get; set; }
        public AvaliacaoDentesSorriso AvaliacaoDentesSorriso { get; set; }
        public bool PossuiAparelho { get; set; }
        public string RetiradaAparelho { get; set; }
        public TipoDePele TipoDePele { get; set; }

        public string DataEntrevista { get; set; }
        public string HoraEntrevista { get; set; }
        public Cliente Cliente { get; set; }
        public int IdVaga { get; set; }

        public bool PossuiCicatriz { get; set; }
        public bool PossuiPiercing { get; set; }
        public bool PossuiTatuagem { get; set; }
        public string LocalPiercing { get; set; }
        public string LocalTatuagem { get; set; }
        public string LocalCicatriz { get; set; }

        public PerfilComportalmental PerfilComportalmental { get; set; }
        public PerfilProfissional PerfilProfissional { get; set; }
        public ConhecimentoMerchandising ConhecimentoMerchandising { get; set; }
        public string AnaliseFinal { get; set; }
        public virtual string OutroPerfilComportalmental { get; set; }
        public virtual AvaliacaoConhecimentoMerchandising AvaliacaoConhecimentoMerchandising { get; set; }
        public virtual PerfilPadrao PerfilPadrao { get; set; }

        public IList<CabeloComprimento> CabeloComprimentos { get; set; }
        public IList<CabeloTipo> CabeloTipos { get; set; }
        public IList<CabeloCor> CabeloCores { get; set; }
        public IList<TipoFisico> TipoFisicos { get; set; }
        public IList<AvaliacaoDentesCor> AvaliacaoDentesCores { get; set; }
        public IList<AvaliacaoDentesSorriso> AvaliacaoDentesSorrisos { get; set; }
        public IList<TipoDePele> TiposDePele { get; set; }
        public IList<Cliente> Clientes { get; set; }

        public IList<PerfilComportalmental> PerfilComportalmentals { get; set; }
        public IList<PerfilProfissional> PerfilProfissionals { get; set; }
        public IList<ConhecimentoMerchandising> ConhecimentoMerchandisings { get; set; }
    }
}