using System;
using System.Collections.Generic;
using Casting.Domain.enums;
using SharpArch.Domain.DomainModel;

namespace Casting.Domain
{
    public class Avaliacao : Entity
    {
        public virtual Vaga Vaga { get; set; }
        public virtual Candidato Candidato { get; set; }
        public virtual DateTime DataEntrevista { get; set; }
        public virtual string HorarioEntrevista { get; set; }
        public virtual CabeloComprimento CabeloComprimento  { get; set; }
        public virtual CabeloCor CabeloCor { get; set; }
        public virtual CabeloTipo CabeloTipo { get; set; }
        public virtual TipoFisico TipoFisico { get; set; }
        public virtual AvaliacaoDentesCor AvaliacaoDentesCor { get; set; }
        public virtual AvaliacaoDentesSorriso AvaliacaoDentesSorriso { get; set; }
        public virtual Boolean PossuiAparelho { get; set; }
        public virtual string PrevisaoRetiradaAparelho { get; set; }
        public virtual TipoDePele TipoDePele { get; set; }
        public virtual Boolean PossuiCicatriz { get; set; }
        public virtual string LocalCicatriz { get; set; }
        public virtual Boolean PossuiPiercing { get; set; }
        public virtual string LocalPiercing { get; set; }
        public virtual Boolean PossuiTatuagem { get; set; }
        public virtual string LocalTatuagem { get; set; }
        public virtual string OutroPerfilComportalmental { get; set; }
        public virtual AvaliacaoConhecimentoMerchandising AvaliacaoConhecimentoMerchandising { get; set; }
        public virtual string AnaliseFinal { get; set; }
        public virtual PerfilPadrao PerfilPadrao { get; set; }
        public virtual SituacaoCandidato SituacaoCandidato { get; set; }

        public virtual IEnumerable<PerfilComportalmental> PerfilComportalmentals { get { return _perfilComportalmentals; } }
        private IList<PerfilComportalmental> _perfilComportalmentals;
        
        public virtual IEnumerable<ConhecimentoMerchandising> ConhecimentoMerchandisings { get { return _conhecimentoMerchandisings; } }
        private IList<ConhecimentoMerchandising> _conhecimentoMerchandisings;

        public virtual IEnumerable<PerfilProfissional> PerfilProfissionals { get { return _perfilProfissionals; } }
        private IList<PerfilProfissional> _perfilProfissionals;

        public virtual void IncluirPerfilComportamental(PerfilComportalmental perfilComportalmental)
        {
            if (_perfilComportalmentals == null)
                _perfilComportalmentals = new List<PerfilComportalmental>();

            if (!_perfilComportalmentals.Contains(perfilComportalmental))
                _perfilComportalmentals.Add(perfilComportalmental);

        }

        public virtual void IncluirConhecimentoMerchandising(ConhecimentoMerchandising conhecimentoMerchandising)
        {
            if (_conhecimentoMerchandisings == null)
                _conhecimentoMerchandisings = new List<ConhecimentoMerchandising>();

            if (!_conhecimentoMerchandisings.Contains(conhecimentoMerchandising))
                _conhecimentoMerchandisings.Add(conhecimentoMerchandising);

        }

        public virtual void IncluirPerfilProfissional(PerfilProfissional perfilProfissional)
        {
            if (_perfilProfissionals == null)
                _perfilProfissionals = new List<PerfilProfissional>();

            if (!_perfilProfissionals.Contains(perfilProfissional))
                _perfilProfissionals.Add(perfilProfissional);

        }
                
    }
}
