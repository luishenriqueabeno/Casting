using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Casting.Domain.enums;
using SharpArch.Domain.DomainModel;

namespace Casting.Domain
{
    public class Candidato : Entity
    {

        public virtual Usuario Usuario { get; set; }
        public virtual string Cpf { get; set; }
        public virtual string Rg { get; set; }
        public virtual DateTime? DataExpedicaoRg { get; set; }
        public virtual string UfExpedicaoRg { get; set; }
        public virtual string Nome { get; set; }
        public virtual DateTime? DataNascimento { get; set; }
        public virtual string NomeDoPai { get; set; }
        public virtual string NomeDaMae { get; set; }
        public virtual string Endereco { get; set; }
        public virtual string Numero { get; set; }
        public virtual string Complemento { get; set; }
        public virtual string Bairro { get; set; }
        public virtual string Cidade { get; set; }
        public virtual string Estado { get; set; }
        public virtual string CEP { get; set; }
        public virtual Regiao Regiao { get; set; }
        public virtual string TelefoneResidencial { get; set; }
        public virtual string TelefoneRecado { get; set; }
        public virtual string TelefoneCelular { get; set; }
        public virtual string TelefoneComercial { get; set; }
        public virtual string Email { get; set; }
        public virtual string CarteiraTrabNumero { get; set; }
        public virtual string CarteiraTrabSerie { get; set; }
        public virtual string CarteiraTrabUf { get; set; }
        public virtual string PisNumero { get; set; }
        public virtual DateTime? PisDataEmissao { get; set; }
        public virtual string HabilitacaoNumero { get; set; }
        public virtual string HabilitacaoCategoria { get; set; }
        public virtual bool PossuiVeiculo { get; set; }
        public virtual TipoVeiculo TipoVeiculo { get; set; }
        public virtual Sexo Sexo { get; set; }
        public virtual bool PossuiFilhos { get; set; }
        public virtual int QtdeFilhos { get; set; }
        public virtual EstadoCivil EstadoCivil { get; set; }
        public virtual string NaturalidadeCidade { get; set; }
        public virtual string NaturalidadeUf { get; set; }
        public virtual string Nacionalidade { get; set; }
        public virtual string CargoPretendido { get; set; }
        public virtual ComoFicouSabendoDaVaga ComoFicouSabendoDaVaga { get; set; }
        public virtual float UltimoSalario { get; set; }
        public virtual float PretensaoSalarial { get; set; }
        public virtual int Manequim { get; set; }
        public virtual string Altura { get; set; }
        public virtual string Peso { get; set; }
        public virtual string Calcado { get; set; }
        public virtual CorDosOlhos CorDosOlhos { get; set; }
        public virtual CorDoCabelo CorDoCabelo  { get; set; }
        public virtual CorDaPele CorDaPele { get; set; }
        public virtual string FotoRosto { get; set; }
        public virtual string FotoCabelo { get; set; }
        public virtual string FotoCorpoFrente { get; set; }
        public virtual string FotoOlhos { get; set; }
        public virtual string FotocorpoLado { get; set; }
        public virtual string TitularContaBancaria { get; set; }
        public virtual Banco Banco { get; set; }
        public virtual TipoConta TipoConta { get; set; }
        public virtual string Agencia { get; set; }
        public virtual string ContaCorrente { get; set; }
        public virtual Escolaridade Escolaridade { get; set; }
        public virtual int AnoConclusao { get; set; }
        public virtual string NomeDoCursoSup { get; set; }
        public virtual string NomeDoCursoSupIncompleto { get; set; }
        public virtual string NomeDoCursoTecnico { get; set; }
        public virtual bool ConhecimentoWord { get; set; }
        public virtual bool ConhecimentoExcel { get; set; }
        public virtual bool ConhecimentoPowerPoint { get; set; }
        public virtual bool ConhecimentoAccess { get; set; }
        public virtual bool ConhecimentoInternet { get; set; }
        public virtual bool ConhecimentoOutlook { get; set; }
        public virtual NivelConhecimento WordNivelConhecimento { get; set; }
        public virtual NivelConhecimento ExcelNivelConhecimento { get; set; }
        public virtual NivelConhecimento PowerPointNivelConhecimento { get; set; }
        public virtual NivelConhecimento AccessNivelConhecimento { get; set; }
        public virtual NivelConhecimento InternetNivelConhecimento { get; set; }
        public virtual NivelConhecimento OutlookNivelConhecimento { get; set; }
        public virtual string OutrosConhecimentos { get; set; }
        public virtual bool PossuiIngles { get; set; }
        public virtual NivelConhecimento InglesNivelConhecimento { get; set; }
        public virtual string Idioma2Nome { get; set; }
        public virtual NivelConhecimento Idioma2NivelConhecimento { get; set; }
        public virtual string Idioma3Nome { get; set; }
        public virtual NivelConhecimento Idioma3NivelConhecimento { get; set; }
        public virtual SituacaoCandidato SituacaoCandidato { get; set; }

        public virtual DateTime? DataCadastro { get; set; }
        public virtual DateTime? DataAlteracao { get; set; }

        public virtual IEnumerable<Funcao> Funcoes { get { return _funcoes; } }
        private IList<Funcao> _funcoes;        
        public virtual string OutrasFuncoes { get; set; }
        
        public virtual IEnumerable<Segmento> Segmentos { get { return _segmentos; } }
        private IList<Segmento> _segmentos;
        public virtual string OutrosSegmentos { get; set; }

        public virtual IEnumerable<ExperienciaProfissional> ExperienciasProfissionais { get { return _experienciasProfissionais; } }
        private IList<ExperienciaProfissional> _experienciasProfissionais;

        public virtual void IncluirFuncao(Funcao funcao)
        {
            if (_funcoes == null)
                _funcoes = new List<Funcao>();

            if (!_funcoes.Contains(funcao))
                _funcoes.Add(funcao);

        }

        public virtual void IncluirSegmento(Segmento segmento)
        {
            if (_segmentos == null)
                _segmentos = new List<Segmento>();

            if (!_segmentos.Contains(segmento))
                _segmentos.Add(segmento);
        }

        public virtual void IncluirExperienciaProfissional(ExperienciaProfissional experiencia)
        {
            if (_experienciasProfissionais == null)
                _experienciasProfissionais = new List<ExperienciaProfissional>();

            if (!_experienciasProfissionais.Contains(experiencia))
                _experienciasProfissionais.Add(experiencia);
        }

        public virtual void RemoverFuncao(Funcao funcao)
        {
            _funcoes.Remove(funcao);
        }

        public virtual void RemoverSegmento(Segmento segmento)
        {
            _segmentos.Remove(segmento);
        }
    }
}
