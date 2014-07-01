using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Casting.Domain;

namespace Casting.Web.Mvc.Controllers.ViewModels
{
    public class PesquisaAdminViewModel
    {
        public List<CandidatoPesquisado> CandidatoPesquisados;

        public IList<Funcao> Funcaos { get; set; }
        public IList<Segmento> Segmentos { get; set; }
        public IList<ExperienciaProfissional> ExperienciaProfissionals { get; set; }
    }

    public class CandidatoPesquisado
    {
        public int IdCandidato { get; set; }
        public string Imagem { get; set; }
        public string Nome { get; set; }
        public string Idade { get; set; }
        public string Escolaridade { get; set; }
        public string Altura { get; set; }
        public string Peso { get; set; }
        public string CorDosOlhos { get; set; }
        public string Situacao { get; set; }
        public string Foto { get; set; }
    }
}