using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Casting.Domain.enums;

namespace Casting.Web.Mvc.Controllers.ViewModels
{
    public class AuxiliarDePesquisaViewModel
    {
        public string TipoPesquisa { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public string Regiao { get; set; }
        public List<string> Funcoes { get; set; }
        public List<string> Segmentos { get; set; }
        public List<string> Projetos { get; set; }
        public string ExperienciaProf { get; set; }
        public string Periodo1 { get; set; }
        public string Periodo2 { get; set; }

        public string IdadeMinima { get; set; }
        public string IdadeMaxima { get; set; }
        public List<string> EstadoCivil { get; set; }
        public List<string> Escolaridade { get; set; }
        public List<string> Informatica { get; set; }
        public List<string> Idiomas { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public List<string> Uf { get; set; }
        public List<string> Regioes { get; set; }
        public List<string> CategoriaCNH { get; set; }
        public List<string> Bancos { get; set; }
        public List<string> TipoConta { get; set; }
        public List<string> Veiculo { get; set; }

        public string ManequimMinimo { get; set; }
        public string ManequimMaximo { get; set; }
        public string AlturaMinima { get; set; }
        public string AlturaMaxima { get; set; }
        public string PesoMinimo { get; set; }
        public string PesoMaximo { get; set; }
        public string CalcadoMinimo { get; set; }
        public string CalcadoMaximo { get; set; }
        public List<string> CorDosOlhos { get; set; }
        public List<string> CorDoCabelo { get; set; }
        public List<string> CorDaPele { get; set; }
        public List<string> AvaliacaoCorpo { get; set; }
        public List<string> AvaliacaoCabelo { get; set; }
        public List<string> AvaliacaoDentes { get; set; }
        public List<string> AvaliacaoPele { get; set; }
        public List<string> PerfilComportamental { get; set; }
        public List<string> ConhecimentoMerchandising{ get; set; }
        public List<string> PerfilProfissional { get; set; }
        public List<string> Padrao { get; set; }
        public List<string> Peculiaridades { get; set; }

        public string ConstroiConsulta()
        {
            //return ConstroiConsultaNormal();
            return ConstroiConsultaAvancada();
        }

        public string ConstroiConsultaNormal()
        {
            
            StringBuilder strCondicao = new StringBuilder();
            
            if(!string.IsNullOrEmpty(Nome))
            {
                strCondicao.AppendFormat(" c.Nome like '%{0}%' ", Nome);

            }

            if(Sexo != "0")
            {
                strCondicao.AppendFormat(" {0} c.Sexo = {1} ", (strCondicao.ToString() != "") ? " and " : "", Sexo);
            }

            if (Regiao != "0")
            {
                strCondicao.AppendFormat(" {0} c.Regiao = {1} ", (strCondicao.ToString() != "") ? " and " : "", Regiao);
            }

            
            if (!string.IsNullOrEmpty(Periodo1))
            {
                strCondicao.AppendFormat(" {0} e.Periodo1 = '{1}' ", (strCondicao.ToString() != "") ? " and " : "", Periodo1);
            }

            if (!string.IsNullOrEmpty(Periodo2))
            {
                strCondicao.AppendFormat(" {0} e.Periodo2 = '{1}' ", (strCondicao.ToString() != "") ? " and " : "", Periodo2);
            }

            if (!string.IsNullOrEmpty(ExperienciaProf))
            {
                var experiencias = ExperienciaProf.Split(';');

                if (experiencias.Any())
                {
                    var experiencia = new StringBuilder();

                    foreach (var exp in experiencias)
                    {
                        experiencia.AppendFormat(" {0} e.Nome like '%{1}%' ", (experiencia.ToString() != "") ? " or " : " (", exp);
                    }
                    experiencia.Append(") ");
                    strCondicao.AppendFormat("{0} {1}", (strCondicao.ToString() != "") ? " and " : "", experiencia);
                }
            }

            if (Funcoes != null)
            {
                var funcao = new StringBuilder();
                
                foreach (var f in Funcoes)
                {
                    funcao.AppendFormat(" {0} f.Id = {1} ", (funcao.ToString() != "") ? " or " : " (", f);
                }
                funcao.Append(") ");
                strCondicao.AppendFormat("{0} {1}", (strCondicao.ToString() != "") ? " and " : "", funcao);
            }


            if (Segmentos != null)
            {
                var segmento = new StringBuilder();
                
                foreach (var seg in Segmentos)
                {
                    segmento.AppendFormat(" {0} s.Id = {1} ", (segmento.ToString() != "") ? " or " : " (", seg);
                }
                segmento.Append(") ");
                strCondicao.AppendFormat("{0} {1}", (strCondicao.ToString() != "") ? " and " : "", segmento);
            }
         

            return strCondicao.ToString();
        }

        public string ConstroiConsultaAvancada()
        {
            var stringBuilder = new StringBuilder();

            if(!string.IsNullOrEmpty(Nome))
            {
                stringBuilder.AppendFormat(" c.Nome like '%{0}%' ", Nome);
            }

            if (!string.IsNullOrEmpty(IdadeMinima))
            {
                stringBuilder.AppendFormat(" {0} (DATEDIFF(DAY, DataNascimento, GETDATE()) / 365) >= {1} ", (stringBuilder.ToString() != "") ? " and " : "", IdadeMinima);
            }

            if (!string.IsNullOrEmpty(IdadeMaxima))
            {
                stringBuilder.AppendFormat(" {0} (DATEDIFF(DAY, DataNascimento, GETDATE()) / 365) <= {1} ", (stringBuilder.ToString() != "") ? " and " : "", IdadeMaxima);
            }

            if (Sexo != "0")
            {
                stringBuilder.AppendFormat(" {0} c.Sexo = {1} ", (stringBuilder.ToString() != "") ? " and " : "", Sexo);
            }

            if (Escolaridade != null)
            {
                var escolaridade = new StringBuilder();

                foreach (var esc in Escolaridade)
                {
                    escolaridade.AppendFormat(" {0} c.Escolaridade = {1} ", (escolaridade.ToString() != "") ? " or " : " (", esc);
                }
                escolaridade.Append(") ");
                stringBuilder.AppendFormat("{0} {1}",(stringBuilder.ToString() != "") ? " and ":"", escolaridade);
            }

            if (EstadoCivil != null)
            {
                var estCivil = new StringBuilder();

                foreach (var civ in EstadoCivil)
                {
                    estCivil.AppendFormat(" {0} c.EstadoCivil = {1} ", (estCivil.ToString() != "") ? " or " : " (", civ);
                }
                estCivil.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", estCivil);
            }

            if (Informatica != null)
            {
                var info = new StringBuilder();
                foreach (var civ in Informatica)
                {
                    var infoStr = ConstroiConhecimentoInformatica(civ);

                    if (infoStr != "")
                    {
                        info.AppendFormat(" {0} {1} ", (info.ToString() != "") ? " or " : " (", infoStr);    
                    }
                }

                info.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", info);
            }

            if (Idiomas != null)
            {
                var idioma = new StringBuilder();
                foreach (var idi in Idiomas)
                {
                    var idiomaStr = ConstroiIdiomas(idi);

                    if (idiomaStr != "")
                    {
                        idioma.AppendFormat(" {0} {1} ", (idioma.ToString() != "") ? " or " : " (", idiomaStr);
                    }
                }

                idioma.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", idioma);
            }

            if (!string.IsNullOrEmpty(Bairro))
            {
                stringBuilder.AppendFormat("{0} c.Bairro like '%{1}%' ", (stringBuilder.ToString() != "") ? " and " : "", Bairro);

            }

            if (!string.IsNullOrEmpty(Cidade))
            {
                stringBuilder.AppendFormat("{0} c.Cidade like '%{1}%' ", (stringBuilder.ToString() != "") ? " and " : "", Cidade);

            }

            if (!string.IsNullOrEmpty(Cep))
            {
                stringBuilder.AppendFormat("{0} c.Cep = '{1}' ", (stringBuilder.ToString() != "") ? " and " : "", Cep);

            }
        
            if (Uf != null)
            {
                var estado = new StringBuilder();

                foreach (var est in Uf)
                {
                    estado.AppendFormat(" {0} c.Estado = '{1}' ", (estado.ToString() != "") ? " or " : " (", est);
                }
                estado.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", estado);
            }

            if (Regioes != null)
            {
                var regioes = new StringBuilder();

                foreach (var reg in Regioes)
                {
                    regioes.AppendFormat(" {0} c.Regiao = {1} ", (regioes.ToString() != "") ? " or " : " (", reg);
                }
                regioes.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", regioes);
            }

            if (CategoriaCNH != null)
            {
                var catCnh = new StringBuilder();

                foreach (var cnh in CategoriaCNH)
                {
                    catCnh.AppendFormat(" {0} c.HabilitacaoCategoria = '{1}' ", (catCnh.ToString() != "") ? " or " : " (", cnh);
                }
                catCnh.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", catCnh);
            }

            if (Veiculo != null)
            {
                var veiculo = new StringBuilder();
                foreach (var v in Veiculo)
                {
                    var veiculoStr = ConstroiCondicaoVeiculo(v);

                    if (veiculoStr != "")
                    {
                        veiculo.AppendFormat(" {0} {1} ", (veiculo.ToString() != "") ? " or " : " (", veiculoStr);
                    }
                }

                veiculo.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", veiculo);
            }

            if (Bancos != null)
            {
                var banco = new StringBuilder();

                foreach (var b in Bancos)
                {
                    banco.AppendFormat(" {0} c.Banco_Id = {1} ", (banco.ToString() != "") ? " or " : " (", b);
                }
                banco.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", banco);
            }

            if (Funcoes != null)
            {
                var funcao = new StringBuilder();

                foreach (var f in Funcoes)
                {
                    funcao.AppendFormat(" {0} f.Id = {1} ", (funcao.ToString() != "") ? " or " : " (", f);
                }
                funcao.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", funcao);
            }

            if (Segmentos != null)
            {
                var segmento = new StringBuilder();

                foreach (var seg in Segmentos)
                {
                    segmento.AppendFormat(" {0} s.Id = {1} ", (segmento.ToString() != "") ? " or " : " (", seg);
                }
                segmento.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", segmento);
            }
            if (Projetos != null)
            {
                var projeto = new StringBuilder();

                foreach (var proj in Projetos)
                {
                    projeto.AppendFormat(" {0} rec.Vaga_Id = {1} ", (projeto.ToString() != "") ? " or " : " (", proj);
                }
                projeto.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", projeto);
            }
            if (TipoConta != null)
            {
                var tipoConta = new StringBuilder();

                foreach (var conta in TipoConta)
                {
                    tipoConta.AppendFormat(" {0} c.TipoConta = {1} ", (tipoConta.ToString() != "") ? " or " : " (", conta);
                }
                tipoConta.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", tipoConta);
            }
            
            if (!string.IsNullOrEmpty(ExperienciaProf))
            {
                var experiencias = ExperienciaProf.Split(';');

                if (experiencias.Any())
                {
                    var experiencia = new StringBuilder();

                    foreach (var exp in experiencias)
                    {
                        experiencia.AppendFormat(" {0} e.Nome like '%{1}%' ", (experiencia.ToString() != "") ? " or " : " (", exp);
                    }
                    experiencia.Append(") ");
                    stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", experiencia);
                }
            }

            if (!string.IsNullOrEmpty(ManequimMinimo))
            {
                stringBuilder.AppendFormat(" {0} c.Manequim >= {1} ", (stringBuilder.ToString() != "") ? " and " : "", ManequimMinimo);
            }

            if (!string.IsNullOrEmpty(ManequimMaximo))
            {
                stringBuilder.AppendFormat(" {0} c.Manequim <= {1} ", (stringBuilder.ToString() != "") ? " and " : "", ManequimMaximo);
            }

            if (!string.IsNullOrEmpty(AlturaMinima))
            {
                stringBuilder.AppendFormat(" {0} c.Altura >='{1}'", (stringBuilder.ToString() != "") ? " and " : "", AlturaMinima);
            }

            if (!string.IsNullOrEmpty(AlturaMaxima))
            {
                stringBuilder.AppendFormat(" {0} c.Altura <= '{1}'", (stringBuilder.ToString() != "") ? " and " : "", AlturaMaxima);
            }

            if (!string.IsNullOrEmpty(PesoMinimo))
            {
                stringBuilder.AppendFormat(" {0} c.Peso >= '{1}' ", (stringBuilder.ToString() != "") ? " and " : "", PesoMinimo);
            }

            if (!string.IsNullOrEmpty(PesoMaximo))
            {
                stringBuilder.AppendFormat(" {0} c.Peso <= '{1}' ", (stringBuilder.ToString() != "") ? " and " : "", PesoMaximo);
            }

            if (!string.IsNullOrEmpty(CalcadoMinimo))
            {
                stringBuilder.AppendFormat(" {0} c.Calcado >= '{1}' ", (stringBuilder.ToString() != "") ? " and " : "", CalcadoMinimo);
            }

            if (!string.IsNullOrEmpty(CalcadoMaximo))
            {
                stringBuilder.AppendFormat(" {0} c.Calcado <= '{1}' ", (stringBuilder.ToString() != "") ? " and " : "", CalcadoMaximo);
            }

            if (CorDosOlhos != null)
            {
                var corOlhosStrBuilder = new StringBuilder();

                foreach (var co in CorDosOlhos)
                {
                    corOlhosStrBuilder.AppendFormat(" {0} c.CorDosOlhos = {1} ", (corOlhosStrBuilder.ToString() != "") ? " or " : " (", co);
                }
                corOlhosStrBuilder.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", corOlhosStrBuilder);
            }

            if (CorDaPele != null)
            {
                var corPeleStringBuilder = new StringBuilder();

                foreach (var cp in CorDaPele)
                {
                    corPeleStringBuilder.AppendFormat(" {0} c.CorDaPele = {1} ", (corPeleStringBuilder.ToString() != "") ? " or " : " (", cp);
                }
                corPeleStringBuilder.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", corPeleStringBuilder);
            }

            if (CorDoCabelo != null)
            {
                var corCabeloStringBuilder = new StringBuilder();

                foreach (var cc in CorDoCabelo)
                {
                    corCabeloStringBuilder.AppendFormat(" {0} c.CorDoCabelo = {1} ", (corCabeloStringBuilder.ToString() != "") ? " or " : " (", cc);
                }
                corCabeloStringBuilder.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", corCabeloStringBuilder);
            }

            if (AvaliacaoCabelo  != null)
            {
                var avalCabeloStringBuilder = new StringBuilder();

                foreach (var avalCabelo in AvaliacaoCabelo)
                {
                    avalCabeloStringBuilder.AppendFormat(" {0} a.CabeloTipo_Id = {1} ", (avalCabeloStringBuilder.ToString() != "") ? " or " : " (", avalCabelo);
                }
                avalCabeloStringBuilder.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", avalCabeloStringBuilder);
            }

            if (AvaliacaoCorpo != null)
            {
                var avalCorpoStringBuilder = new StringBuilder();

                foreach (var avalCorpo in AvaliacaoCorpo)
                {
                    avalCorpoStringBuilder.AppendFormat(" {0} a.TipoFisico_Id = {1} ", (avalCorpoStringBuilder.ToString() != "") ? " or " : " (", avalCorpo);
                }
                avalCorpoStringBuilder.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", avalCorpoStringBuilder);
            }

            if (AvaliacaoDentes != null)
            {
                var avalDentesStringBuilder = new StringBuilder();

                foreach (var avalDentes in AvaliacaoDentes)
                {
                    avalDentesStringBuilder.AppendFormat(" {0} a.AvaliacaoDentesSorriso_Id = {1} ", (avalDentesStringBuilder.ToString() != "") ? " or " : " (", avalDentes);
                }
                avalDentesStringBuilder.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", avalDentesStringBuilder);
            }

            if (AvaliacaoPele != null)
            {
                var avalPeleStringBuilder = new StringBuilder();

                foreach (var avalPele in AvaliacaoPele)
                {
                    avalPeleStringBuilder.AppendFormat(" {0} a.TipoDePele_Id = {1} ", (avalPeleStringBuilder.ToString() != "") ? " or " : " (", avalPele);
                }
                avalPeleStringBuilder.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", avalPeleStringBuilder);
            }

            if (PerfilProfissional != null)
            {
                var perfilProfStringBuilder = new StringBuilder();

                foreach (var pp in PerfilProfissional)
                {
                    perfilProfStringBuilder.AppendFormat(" {0} pp.Id = {1} ", (perfilProfStringBuilder.ToString() != "") ? " or " : " (", pp);
                }
                perfilProfStringBuilder.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", perfilProfStringBuilder);
            }

            if (PerfilComportamental != null)
            {
                var perfilCompStringBuilder = new StringBuilder();

                foreach (var pc in PerfilComportamental)
                {
                    perfilCompStringBuilder.AppendFormat(" {0} pc.Id = {1} ", (perfilCompStringBuilder.ToString() != "") ? " or " : " (", pc);
                }
                perfilCompStringBuilder.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", perfilCompStringBuilder);
            }

            if (ConhecimentoMerchandising != null)
            {
                var conMerchanStringBuilder = new StringBuilder();

                foreach (var cm in ConhecimentoMerchandising)
                {
                    conMerchanStringBuilder.AppendFormat(" {0} cm.Id = {1} ", (conMerchanStringBuilder.ToString() != "") ? " or " : " (", cm);
                }
                conMerchanStringBuilder.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", conMerchanStringBuilder);
            }

            if (Padrao != null)
            {
                var padrao = new StringBuilder();

                foreach (var pd in Padrao)
                {
                    padrao.AppendFormat(" {0} a.PerfilPadrao = {1} ", (padrao.ToString() != "") ? " or " : " (", pd);
                }
                padrao.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", padrao);
            }

            if (Peculiaridades != null)
            {
                var peculiariedades = new StringBuilder();
                foreach (var pddes in Peculiaridades)
                {
                    var peculiariedadeStr = ConstroiCondicaoPeculiariedade(pddes);

                    if (peculiariedadeStr != "")
                    {
                        peculiariedades.AppendFormat(" {0} {1} ", (peculiariedades.ToString() != "") ? " or " : " (", peculiariedadeStr);
                    }
                }

                peculiariedades.Append(") ");
                stringBuilder.AppendFormat("{0} {1}", (stringBuilder.ToString() != "") ? " and " : "", peculiariedades);
            }
            
            return stringBuilder.ToString();
        }

        private string ConstroiIdiomas(string strIdioma)
        {
            switch (strIdioma)
            {
                case "Alemao":
                    return "(Idioma2Nome like 'Alem%' or Idioma3Nome like 'Alem%')";
                case "Frances":
                    return "(Idioma2Nome like 'Franc%' or Idioma3Nome like 'Franc%')";
                case "EspanholBasico":
                    return "((Idioma2Nome = 'Espanhol' and Idioma2NivelConhecimento = 1) or (Idioma3Nome = 'Espanhol' and Idioma3NivelConhecimento = 1))";
                case "EspanholIntermediario":
                    return "((Idioma2Nome = 'Espanhol' and Idioma2NivelConhecimento = 2) or (Idioma3Nome = 'Espanhol' and Idioma3NivelConhecimento = 2))";
                case "EspanholAvancado":
                    return "((Idioma2Nome = 'Espanhol' and Idioma2NivelConhecimento = 3) or (Idioma3Nome = 'Espanhol' and Idioma3NivelConhecimento = 3))";
                case "InglesBasico":
                    return "(PossuiIngles = 1 and InglesNivelConhecimento = 1)";
                case "InglesIntermediario":
                    return "(PossuiIngles = 1 and InglesNivelConhecimento = 2)";
                case "InglesAvancado":
                    return "(PossuiIngles = 1 and InglesNivelConhecimento = 3)";
            }
            return "";
        }
        
        private string ConstroiConhecimentoInformatica(string strConhecimento)
        {
            switch (strConhecimento)
            {
                case "AccessBasico":
                    return "(ConhecimentoAccess = 1 and AccessNivelConhecimento = 1)";
                case "AccessIntermediario":
                    return "(ConhecimentoAccess = 1 and AccessNivelConhecimento = 2)";
                case "AccessAvancado":
                    return "(ConhecimentoAccess = 1 and AccessNivelConhecimento = 3)";
                case "ExcelBasico":
                    return "(ConhecimentoExcel = 1 and ExcelNivelConhecimento = 1)";
                case "ExcelIntermediario":
                    return "(ConhecimentoExcel = 1 and ExcelNivelConhecimento = 2)";
                case "ExcelAvancado":
                    return "(ConhecimentoExcel = 1 and ExcelNivelConhecimento = 3)";
                case "InternetBasico":
                    return "(ConhecimentoInternet = 1 and InternetNivelConhecimento = 1)";
                case "InternetIntermediario":
                    return "(ConhecimentoInternet = 1 and InternetNivelConhecimento = 2)";
                case "InternetAvancado":
                    return "(ConhecimentoInternet = 1 and InternetNivelConhecimento = 3)";
                case "OutlookBasico":
                    return "(ConhecimentoOutlook = 1 and OutlookNivelConhecimento = 1)";
                case "OutlookIntermediario":
                    return "(ConhecimentoOutlook = 1 and OutlookNivelConhecimento = 2)";
                case "OutlookAvancado":
                    return "(ConhecimentoOutlook = 1 and OutlookNivelConhecimento = 3)";
                case "PowerPointBasico":
                    return "(ConhecimentoPowerPoint = 1 and PowerPointNivelConhecimento = 1)";
                case "PowerPointIntermediario":
                    return "(ConhecimentoPowerPoint = 1 and PowerPointNivelConhecimento = 2)";
                case "PowerPointAvancado":
                    return "(ConhecimentoPowerPoint = 1 and PowerPointNivelConhecimento = 3)";
                case "WordBasico":
                    return "(ConhecimentoWord = 1 and WordNivelConhecimento = 1)";
                case "WordIntermediario":
                    return "(ConhecimentoWord = 1 and WordNivelConhecimento = 2)";
                case "WordAvancado":
                    return "(ConhecimentoWord = 1 and WordNivelConhecimento = 3)";
            }

            return "";
        }

        private string ConstroiCondicaoVeiculo(string strCondicao)
        {

            switch (strCondicao)
            {
                case "carro":
                    return "(PossuiVeiculo = 1 and TipoVeiculo = 1)";
                case "moto":
                    return "(PossuiVeiculo = 1 and TipoVeiculo = 2)";
                case "van":
                    return "(PossuiVeiculo = 1 and TipoVeiculo = 3)";
                case "naoinformou":
                    return "(PossuiVeiculo = 1 and TipoVeiculo = 0)";
                case "nenhum":
                    return "(PossuiVeiculo = 0)";
            }

            return "";
        }

        private string ConstroiCondicaoPeculiariedade(string strCondicao)
        {

            switch (strCondicao)
            {
                case "aparelho":
                    return "(PossuiAparelho = 1)";
                case "piercing":
                    return "(PossuiPiercing = 1)";
                case "cicatriz":
                    return "(PossuiCicatriz = 1)";
                case "tatuagem":
                    return "(PossuiTatuagem = 1)";
            }

            return "";
        }
    }
}