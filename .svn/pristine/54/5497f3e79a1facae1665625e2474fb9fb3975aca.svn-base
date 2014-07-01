using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Casting.Domain;

namespace Casting.Web.Mvc.Controllers.ViewModels
{
    public class PesquisaVagaViewModel
    {
        public IList<Cliente> Clientes { get; set; }

        public List<EntrevistasDaVaga> Entrevistas { get; set; }
        public List<PesquisaCandidatoViewModel> PessoasNaVaga { get; set; }

        public String TituloDaVagaParaDetalhes  { get; set; }
        public String NomeDoClienteParaDetalhes { get; set; }
    }
    
    public class EntrevistasDaVaga
    {
        public int IdCandidato { get; set; }
        public string Nome { get; set; }
        public string Idade { get; set; }
        public string Escolaridade { get; set; }
        public string DataEntrevista { get; set; }
        public string HoraEntrevista { get; set; }
        public string Situacao { get; set; }
        public string Foto { get; set; }
    }

    public class VagasPesquisadasViewModel
    {
        public string Id { get; set; }
        public string TituloVaga { get; set; }
        public string Cliente { get; set; }
        public int TotalVagas { get; set; }
        public int TotalVagasNaoPreenchidas { get; set; }   

    }
}