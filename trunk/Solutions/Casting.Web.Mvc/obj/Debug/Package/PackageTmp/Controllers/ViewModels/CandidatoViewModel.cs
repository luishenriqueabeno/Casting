using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Casting.Domain;
using Casting.Domain.enums;

namespace Casting.Web.Mvc.Controllers.ViewModels
{
    public class CandidatoViewModel
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string DataExpedicaoRg { get; set; }
        public string UfExpedicaoRg { get; set; }
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
        public string Idade { get; set; }
        public string NomeDoPai { get; set; }
        public string NomeDaMae { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public virtual string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
        public Regiao Regiao { get; set; }
        public string TelefoneResidencial { get; set; }
        public string TelefoneRecado { get; set; }
        public string TelefoneCelular { get; set; }
        public string TelefoneComercial { get; set; }
        public string Email { get; set; }
        public string CarteiraTrabNumero { get; set; }
        public string CarteiraTrabSerie { get; set; }
        public string CarteiraTrabUf { get; set; }
        public string PisNumero { get; set; }
        public string PisDataEmissao { get; set; }
        public string HabilitacaoNumero { get; set; }
        public string HabilitacaoCategoria { get; set; }
        public bool PossuiVeiculo { get; set; }
        public TipoVeiculo TipoVeiculo { get; set; }
        public Sexo Sexo { get; set; }
        public bool PossuiFilhos { get; set; }
        public int QtdeFilhos { get; set; }
        public EstadoCivil EstadoCivil { get; set; }
        public string NaturalidadeCidade { get; set; }
        public string NaturalidadeUf { get; set; }
        public string Nacionalidade { get; set; }
        public string CargoPretendido { get; set; }
        public ComoFicouSabendoDaVaga ComoFicouSabendoDaVaga { get; set; }
        public float UltimoSalario { get; set; }
        public float PretensaoSalarial { get; set; }
        public int Manequim { get; set; }
        public string Altura { get; set; }
        public string Peso { get; set; }
        public string Calcado { get; set; }
        public CorDosOlhos CorDosOlhos { get; set; }
        public CorDoCabelo CorDoCabelo { get; set; }
        public CorDaPele CorDaPele { get; set; }
        public string TitularContaBancaria { get; set; }
        public Banco Banco { get; set; }
        public TipoConta TipoConta { get; set; }
        public string Agencia { get; set; }
        public string ContaCorrente { get; set; }
        public Escolaridade Escolaridade { get; set; }
        public int AnoConclusao { get; set; }
        public string NomeDoCursoSup { get; set; }
        public string NomeDoCursoSupIncompleto { get; set; }
        public string NomeDoCursoTecnico { get; set; }
        public bool ConhecimentoWord { get; set; }
        public bool ConhecimentoExcel { get; set; }
        public bool ConhecimentoPowerPoint { get; set; }
        public bool ConhecimentoAccess { get; set; }
        public bool ConhecimentoInternet { get; set; }
        public bool ConhecimentoOutlook { get; set; }
        public NivelConhecimento WordNivelConhecimento { get; set; }
        public NivelConhecimento ExcelNivelConhecimento { get; set; }
        public NivelConhecimento PowerPointNivelConhecimento { get; set; }
        public NivelConhecimento AccessNivelConhecimento { get; set; }
        public NivelConhecimento InternetNivelConhecimento { get; set; }
        public NivelConhecimento OutlookNivelConhecimento { get; set; }
        public string OutrosConhecimentos { get; set; }
        public bool PossuiIngles { get; set; }
        public NivelConhecimento InglesNivelConhecimento { get; set; }
        public string Idioma2Nome { get; set; }
        public NivelConhecimento Idioma2NivelConhecimento { get; set; }
        public string Idioma3Nome { get; set; }
        public NivelConhecimento Idioma3NivelConhecimento { get; set; }
        public string DataAlteracao { get; set; }
        public string DataCadastro { get; set; }

        public IList<Funcao> Funcoes { get; set; }
        public string OutrasFuncoes { get; set; }

        public IList<Segmento> Segmentos { get; set; }
        public string OutrosSegmentos { get; set; }

        public IList<ExperienciaProfissional> ExperienciasProfissionais { get; set; }

        public string FotoRosto { get; set; }
        public string FotoCabelo { get; set; }
        public string FotoCorpoFrente { get; set; }
        public string FotoOlhos { get; set; }
        public string FotoCorpoLado { get; set; }

        public HttpPostedFileBase FileFotoRosto { get; set; }
        public HttpPostedFileBase FileFotoCabelo { get; set; }
        public HttpPostedFileBase FileFotoCorpoFrente { get; set; }
        public HttpPostedFileBase FileFotoOlhos { get; set; }
        public HttpPostedFileBase FileFotoCorpoLado { get; set; }

        public string TipoDeOperacao { get; set; } //Avançar ou Upload de Fotos

        public List<SegmentosDoCandidato> SegmentoList { get; set; }
        public List<FuncoesDoCandidato> FuncoesList { get; set; }

        public IList<Banco> BancoList { get; set; }

        public IList<Cliente> ClienteList { get; set; }
        public IList<HistoricoDeAvaliacao> HistoricoDeAvaliacao { get; set; }
        public IList<HistoricoDeRecrutamento> Recrutamentos { get; set; }
        public IList<SituacaoCandidato> SituacaoCandidatoList { get; set; }
        public SituacaoCandidato SituacaoCandidato { get; set; }
        public Recrutamento RecrutamentoAtual { get; set; }

        public AvaliacaoViewModel AvaliacaoDetalhada { get; set; }
        public Entrevista EntrevistaAgendada { get; set; }
    }

    public class HistoricoDeAvaliacao
    {
        public int IdAvaliacao { get; set; }
        public string DataAvaliacao { get; set; }
        public string TituloVaga { get; set; }
        public string Cliente { get; set; }
        public SituacaoCandidato Situacao { get; set; }
        public string Status { get; set; }
    }

    public class HistoricoDeRecrutamento
    {
        public int IdRecrutamento { get; set; }
        public string TituloVaga { get; set; }
        public string Cliente { get; set; }
        public SituacaoCandidato Situacao { get; set; }
        public string Status { get;set;}
    }

    public class SegmentosDoCandidato
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool UsuarioPossuiSegmento { get; set; }
    }

    public class FuncoesDoCandidato
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool UsuarioPossuiFuncao { get; set; }
    }
}