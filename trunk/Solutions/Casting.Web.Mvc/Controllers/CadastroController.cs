using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Casting.Domain;
using Casting.Domain.Contracts.Tasks;
using Casting.Domain.enums;
using Casting.Web.Mvc.Controllers.Queries;
using Casting.Web.Mvc.Controllers.ViewModels;

namespace Casting.Web.Mvc.Controllers
{
    public class CadastroController : Controller
    {
        private readonly IServicoDeAutenticacao _servicoDeAutenticacao;
        private readonly ICandidatos _candidato;
        private readonly ISegmentos _segmento;
        private readonly IFuncaos _funcao;
        private readonly IBancos _bancos;
        private readonly IExperienciaProfissionals _experienciaProfissionals;
        private readonly ISituacaoCandidatos _situacaoCandidatos;

        public CadastroController(ICandidatos candidato, ISegmentos segmento, IFuncaos funcao, IServicoDeAutenticacao servicoDeAutenticacao, IExperienciaProfissionals experienciaProfissionals, IBancos bancos, ISituacaoCandidatos situacaoCandidatos)
        {
            _candidato = candidato;
            _segmento = segmento;
            _funcao = funcao;
            _servicoDeAutenticacao = servicoDeAutenticacao;
            _experienciaProfissionals = experienciaProfissionals;
            _bancos = bancos;
            _situacaoCandidatos = situacaoCandidatos;
        }

        public ActionResult DadosPessoais(string idCandidato = null)
        {
            var usuario = _servicoDeAutenticacao.UsuarioAutenticado();

            CandidatoViewModel cv = usuario.Perfil == Perfil.Candidato ?
                                    GetCandidatoViewModel(usuario) : GetCandidatoViewModel(idCandidato);

            ViewData["usuario"] = usuario;

            return View(cv);
        }

        public ActionResult ViaGet(string cep, string callback)
        {
            var teste = new JsonpResult() { Data = new { TipoDeLogradouro = "Rua", Logradouro = "Tamoios", ComplementoDoLogradouro = "NC", CEP = "11360070", Estado = "São Paulo", Bairro = "Parque São Vicente", Cidade = "São Vicente", Regional = false } };

            return teste;
        }

        [HttpPost]
        public ActionResult GravaDadosPessoais(CandidatoViewModel cv)
        {
            var candidato = ViewModelParaModel(cv, EtapaCadastro.DadosPessoais);
            _candidato.SaveCandidato(candidato);

            return RedirectToAction("Documentos", new { idCandidato = candidato.Id });
        }

        public ActionResult Caracteristicas(string idCandidato = null)
        {
            var usuario = _servicoDeAutenticacao.UsuarioAutenticado();

            CandidatoViewModel cv = usuario.Perfil == Perfil.Candidato ?
                                    GetCandidatoViewModel(usuario) : GetCandidatoViewModel(idCandidato);

            ViewData["usuario"] = usuario;

            return View(cv);
        }

        [HttpPost]
        public RedirectToRouteResult GravaCaracteristicas(CandidatoViewModel cv)
        {
            if (cv.FileFotoCabelo != null)
                cv.FotoCabelo = SalvarArquivo(cv.FileFotoCabelo, "/Content/uploads/fotosCandidatos");

            if (cv.FileFotoCorpoFrente != null)
                cv.FotoCorpoFrente = SalvarArquivo(cv.FileFotoCorpoFrente, "/Content/uploads/fotosCandidatos");

            if (cv.FileFotoCorpoLado != null)
                cv.FotoCorpoLado = SalvarArquivo(cv.FileFotoCorpoLado, "/Content/uploads/fotosCandidatos");

            if (cv.FileFotoOlhos != null)
                cv.FotoOlhos = SalvarArquivo(cv.FileFotoOlhos, "/Content/uploads/fotosCandidatos");

            if (cv.FileFotoRosto != null)
                cv.FotoRosto = SalvarArquivo(cv.FileFotoRosto, "/Content/uploads/fotosCandidatos");

            var candidato = ViewModelParaModel(cv, EtapaCadastro.Caracteristicas);
            _candidato.SaveCandidato(candidato);

            if (cv.TipoDeOperacao == "uploadDeFotos")
            {
                return RedirectToAction("Caracteristicas", new { idCandidato = candidato.Id });
            }
            else
            {
                return RedirectToAction("DadosBancarios", new { idCandidato = candidato.Id });
            }

        }


        [HttpPost]
        public ActionResult GravaCaracteristicasFotoRosto(HttpPostedFileBase fileData, string id)
        {
            var postedFileBase = fileData;

            var cv = GetCandidatoViewModel(id);
            cv.Id = int.Parse(id);

            if (postedFileBase != null)
                cv.FotoRosto = SalvarArquivo(postedFileBase, "/Content/uploads/fotosCandidatos");

            var candidato = ViewModelParaModel(cv, EtapaCadastro.Caracteristicas);
            _candidato.SaveCandidato(candidato);

            var caminhoImagem = "/Content/uploads/fotosCandidatos/" + cv.FotoRosto;

            return Json(new {caminho = caminhoImagem.ToString()});
        }

        [HttpPost]
        public ActionResult DeleteFotoRosto(string id)
        {
           
            var cv = GetCandidatoViewModel(id);
            cv.Id = int.Parse(id);
           
            cv.FotoRosto = ""; 

            var candidato = ViewModelParaModel(cv, EtapaCadastro.Caracteristicas);
            _candidato.SaveCandidato(candidato);

            var caminhoImagem = " ";

            return Json(new { caminho = caminhoImagem.ToString() });
        }

        [HttpPost]
        public ActionResult GravaCaracteristicasFotoCorpoFrente(HttpPostedFileBase fileData, string id)
        {
            var postedFileBase = fileData;

            var cv = GetCandidatoViewModel(id);
            cv.Id = int.Parse(id);

            if (postedFileBase != null)
                cv.FotoCorpoFrente = SalvarArquivo(postedFileBase, "/Content/uploads/fotosCandidatos");

            var candidato = ViewModelParaModel(cv, EtapaCadastro.Caracteristicas);
            _candidato.SaveCandidato(candidato);

            var caminhoImagem = "/Content/uploads/fotosCandidatos/" + cv.FotoCorpoFrente;

            return Json(new { caminho = caminhoImagem.ToString() });
        }

        [HttpPost]
        public ActionResult DeleteFotoCorpoFrente(string id)
        {

            var cv = GetCandidatoViewModel(id);
            cv.Id = int.Parse(id);

            cv.FotoCorpoFrente = "";

            var candidato = ViewModelParaModel(cv, EtapaCadastro.Caracteristicas);
            _candidato.SaveCandidato(candidato);

            var caminhoImagem = " ";

            return Json(new { caminho = caminhoImagem.ToString() });
        }

        [HttpPost]
        public ActionResult GravaCaracteristicasFotoCorpoLado(HttpPostedFileBase fileData, string id)
        {
            var postedFileBase = fileData;

            var cv = GetCandidatoViewModel(id);
            cv.Id = int.Parse(id);

            if (postedFileBase != null)
                cv.FotoCorpoLado = SalvarArquivo(postedFileBase, "/Content/uploads/fotosCandidatos");

            var candidato = ViewModelParaModel(cv, EtapaCadastro.Caracteristicas);
            _candidato.SaveCandidato(candidato);

            var caminhoImagem = "/Content/uploads/fotosCandidatos/" + cv.FotoCorpoLado;

            return Json(new { caminho = caminhoImagem.ToString() });
        }
        [HttpPost]
        public ActionResult DeleteFotoCorpoLado(string id)
        {

            var cv = GetCandidatoViewModel(id);
            cv.Id = int.Parse(id);

            cv.FotoCorpoLado = "";

            var candidato = ViewModelParaModel(cv, EtapaCadastro.Caracteristicas);
            _candidato.SaveCandidato(candidato);

            var caminhoImagem = " ";

            return Json(new { caminho = caminhoImagem.ToString() });
        }



        [HttpPost]
        public ActionResult GravaCaracteristicasFotoCabelo(HttpPostedFileBase fileData, string id)
        {
            var postedFileBase = fileData;

            var cv = GetCandidatoViewModel(id);
            cv.Id = int.Parse(id);

            if (postedFileBase != null)
                cv.FotoCabelo= SalvarArquivo(postedFileBase, "/Content/uploads/fotosCandidatos");

            var candidato = ViewModelParaModel(cv, EtapaCadastro.Caracteristicas);
            _candidato.SaveCandidato(candidato);

            var caminhoImagem = "/Content/uploads/fotosCandidatos/" + cv.FotoCabelo;

            return Json(new { caminho = caminhoImagem.ToString() });
        }
        [HttpPost]
        public ActionResult DeleteFotoCabelo(string id)
        {

            var cv = GetCandidatoViewModel(id);
            cv.Id = int.Parse(id);

            cv.FotoCabelo = "";

            var candidato = ViewModelParaModel(cv, EtapaCadastro.Caracteristicas);
            _candidato.SaveCandidato(candidato);

            var caminhoImagem = " ";

            return Json(new { caminho = caminhoImagem.ToString() });
        }

        [HttpPost]
        public ActionResult GravaCaracteristicasFotoOlhos(HttpPostedFileBase fileData, string id)
        {
            var postedFileBase = fileData;

            var cv = GetCandidatoViewModel(id);
            cv.Id = int.Parse(id);

            if (postedFileBase != null)
                cv.FotoOlhos = SalvarArquivo(postedFileBase, "/Content/uploads/fotosCandidatos");

            var candidato = ViewModelParaModel(cv, EtapaCadastro.Caracteristicas);
            _candidato.SaveCandidato(candidato);

            var caminhoImagem = "/Content/uploads/fotosCandidatos/" + cv.FotoOlhos;

            return Json(new { caminho = caminhoImagem.ToString() });
        }
        [HttpPost]
        public ActionResult DeleteFotoOlhos(string id)
        {

            var cv = GetCandidatoViewModel(id);
            cv.Id = int.Parse(id);

            cv.FotoOlhos = "";

            var candidato = ViewModelParaModel(cv, EtapaCadastro.Caracteristicas);
            _candidato.SaveCandidato(candidato);

            var caminhoImagem = " ";

            return Json(new { caminho = caminhoImagem.ToString() });
        }


        public ActionResult DadosBancarios(string idCandidato = null)
        {
            var usuario = _servicoDeAutenticacao.UsuarioAutenticado();

            CandidatoViewModel cv = usuario.Perfil == Perfil.Candidato ?
                                    GetCandidatoViewModel(usuario) : GetCandidatoViewModel(idCandidato);

            ViewData["usuario"] = usuario;

            IList<Banco> bancos = _bancos.ListaBancos().OrderBy(x => x.Nome).ToList();
            cv.BancoList = bancos;
            return View(cv);
        }

        [HttpPost]
        public RedirectToRouteResult GravaDadosBancarios(CandidatoViewModel cv, string banco)
        {
            cv.Banco = _bancos.FindByNome(banco);
            var candidato = ViewModelParaModel(cv, EtapaCadastro.DadosBancarios);
            _candidato.SaveCandidato(candidato);

            return RedirectToAction("Escolaridade", new { idCandidato = candidato.Id });
        }

        public ActionResult Documentos(string idCandidato = null)
        {
            var usuario = _servicoDeAutenticacao.UsuarioAutenticado();

            CandidatoViewModel cv = usuario.Perfil == Perfil.Candidato ?
                                    GetCandidatoViewModel(usuario) : GetCandidatoViewModel(idCandidato);

            ViewData["usuario"] = usuario;

            return View(cv);
        }

        [HttpPost]
        public RedirectToRouteResult GravaDocumentos(CandidatoViewModel cv)
        {
            var candidato = ViewModelParaModel(cv, EtapaCadastro.Documentos);
            _candidato.SaveCandidato(candidato);

            return RedirectToAction("InformacoesGerais", new { idCandidato = candidato.Id });
        }

        public ActionResult Escolaridade(string idCandidato = null)
        {
            var usuario = _servicoDeAutenticacao.UsuarioAutenticado();

            CandidatoViewModel cv = usuario.Perfil == Perfil.Candidato ?
                                    GetCandidatoViewModel(usuario) : GetCandidatoViewModel(idCandidato);

            ViewData["usuario"] = usuario;

            return View(cv);
        }

        [HttpPost]
        public RedirectToRouteResult GravaEscolaridade(CandidatoViewModel cv)
        {
            var candidato = ViewModelParaModel(cv, EtapaCadastro.Escolaridade);
            _candidato.SaveCandidato(candidato);

            return RedirectToAction("Informatica", new { idCandidato = candidato.Id });
        }

        public ActionResult ExperienciaProfissional(string idCandidato = null)
        {
            var usuario = _servicoDeAutenticacao.UsuarioAutenticado();

            CandidatoViewModel cv = usuario.Perfil == Perfil.Candidato ?
                                    GetCandidatoViewModel(usuario) : GetCandidatoViewModel(idCandidato);

            ViewData["usuario"] = usuario;

            return View(cv);
        }

        [HttpPost]
        public RedirectToRouteResult GravaExperienciaProfissional(FormCollection formCollection)
        {
            var expId = formCollection["idExp"].Split(',');
            var empresa = formCollection["empresa"].Split(',');
            var produto = formCollection["produto"].Split(',');
            var periodo1 = formCollection["periodo1"].Split(',');
            var periodo2 = formCollection["periodo2"].Split(',');
            var tipoacao = formCollection["tipoacao"].Split(',');

            var usuario = _servicoDeAutenticacao.UsuarioAutenticado();

            CandidatoViewModel cv = usuario.Perfil == Perfil.Candidato ?
                                    GetCandidatoViewModel(usuario) : GetCandidatoViewModel(formCollection["Id"]);

            cv.ExperienciasProfissionais = new List<ExperienciaProfissional>();

            for (var i = 0; i < empresa.Count(); i++)
            {
                if (empresa[i] != "" || produto[i] != "" || periodo1[i] != "" || periodo2[i] != "" || tipoacao[i] != "")
                {
                    ExperienciaProfissional expProfissional = null;
                    if (expId[i] != "" && expId[i] != "0")
                    {
                        expProfissional = _experienciaProfissionals.GetExperienciaProfissional(Convert.ToInt32(expId[i]));
                    }
                    else
                    {
                        expProfissional = new ExperienciaProfissional();
                    }

                    expProfissional.Empresa = empresa[i];
                    expProfissional.Produto = produto[i];
                    expProfissional.Periodo1 = periodo1[i];
                    expProfissional.Periodo2 = periodo2[i];
                    expProfissional.TipoAcao = tipoacao[i];

                    cv.ExperienciasProfissionais.Add(expProfissional);

                }
            }
            var candidato = ViewModelParaModel(cv, EtapaCadastro.ExperienciaProfissional);
            _candidato.SaveCandidato(candidato);

            if (usuario.Perfil == Perfil.Candidato)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Detalhes", "Home", new { id = candidato.Id });
            }

        }

        public ActionResult Funcoes(string idCandidato = null)
        {
            var usuario = _servicoDeAutenticacao.UsuarioAutenticado();

            CandidatoViewModel cv = usuario.Perfil == Perfil.Candidato ?
                                    GetCandidatoViewModel(usuario) : GetCandidatoViewModel(idCandidato);

            ViewData["usuario"] = usuario;

            var funcoes = _funcao.ListaFuncoes();

            cv.FuncoesList = new List<FuncoesDoCandidato>();

            foreach (var funcao in funcoes)
            {
                cv.FuncoesList.Add(new FuncoesDoCandidato()
                {
                    Descricao = funcao.Nome,
                    UsuarioPossuiFuncao = (cv.Funcoes == null) ? false : cv.Funcoes.Any(x => x.Nome == funcao.Nome)
                });
            }

            return View(cv);
        }

        [HttpPost]
        public RedirectToRouteResult GravaFuncoes(FormCollection formCollection)
        {

            _candidato.RemoverFuncoesDoCandidato(Convert.ToInt32(formCollection["Id"]));

            var usuario = _servicoDeAutenticacao.UsuarioAutenticado();

            CandidatoViewModel cv = usuario.Perfil == Perfil.Candidato ?
                                    GetCandidatoViewModel(usuario) : GetCandidatoViewModel(formCollection["Id"]);

            cv.Funcoes = new List<Funcao>();
            cv.OutrasFuncoes = formCollection["OutrasFuncoes"];

            if (formCollection["funcoes"] != null)
            {
                var funcoesMarcadas = formCollection["funcoes"].Split(',');

                foreach (var f in funcoesMarcadas)
                {
                    var funcao = _funcao.FindByDescr(f);
                    cv.Funcoes.Add(funcao);
                }

            }

            var candidato = ViewModelParaModel(cv, EtapaCadastro.Funcoes);
            _candidato.SaveCandidato(candidato);

            return RedirectToAction("Segmentos", new { idCandidato = candidato.Id });
        }

        public ActionResult Idiomas(string idCandidato = null)
        {
            var usuario = _servicoDeAutenticacao.UsuarioAutenticado();

            CandidatoViewModel cv = usuario.Perfil == Perfil.Candidato ?
                                    GetCandidatoViewModel(usuario) : GetCandidatoViewModel(idCandidato);

            ViewData["usuario"] = usuario;

            return View(cv);
        }

        [HttpPost]
        public RedirectToRouteResult GravaIdiomas(CandidatoViewModel cv)
        {
            var c = _candidato.GetCandidato(cv.Id);

            var candidato = ViewModelParaModel(cv, EtapaCadastro.Idiomas);
            _candidato.SaveCandidato(candidato);

            return RedirectToAction("Funcoes", new { idCandidato = candidato.Id });
        }

        public ActionResult InformacoesGerais(string idCandidato = null)
        {
            var usuario = _servicoDeAutenticacao.UsuarioAutenticado();

            CandidatoViewModel cv = usuario.Perfil == Perfil.Candidato ?
                                    GetCandidatoViewModel(usuario) : GetCandidatoViewModel(idCandidato);

            ViewData["usuario"] = usuario;

            return View(cv);
        }

        [HttpPost]
        public RedirectToRouteResult GravaInformacoesGerais(CandidatoViewModel cv)
        {
            var candidato = ViewModelParaModel(cv, EtapaCadastro.InformacoesGerais);
            _candidato.SaveCandidato(candidato);

            return RedirectToAction("Caracteristicas", new { idCandidato = candidato.Id });
        }

        public ActionResult Informatica(string idCandidato = null)
        {
            var usuario = _servicoDeAutenticacao.UsuarioAutenticado();

            CandidatoViewModel cv = usuario.Perfil == Perfil.Candidato ?
                                    GetCandidatoViewModel(usuario) : GetCandidatoViewModel(idCandidato);

            ViewData["usuario"] = usuario;

            return View(cv);
        }

        [HttpPost]
        public RedirectToRouteResult GravaInformatica(CandidatoViewModel cv)
        {
            var candidato = ViewModelParaModel(cv, EtapaCadastro.Informatica);
            _candidato.SaveCandidato(candidato);

            return RedirectToAction("Idiomas", new { idCandidato = candidato.Id });
        }

        public ActionResult Segmentos(string idCandidato = null)
        {
            var usuario = _servicoDeAutenticacao.UsuarioAutenticado();

            CandidatoViewModel cv = usuario.Perfil == Perfil.Candidato ?
                                    GetCandidatoViewModel(usuario) : GetCandidatoViewModel(idCandidato);

            ViewData["usuario"] = usuario;

            var segmentos = _segmento.ListaSegmentos();

            cv.SegmentoList = new List<SegmentosDoCandidato>();
            foreach (var seg in segmentos)
            {
                cv.SegmentoList.Add(new SegmentosDoCandidato()
                                                              {
                                                                  Descricao = seg.Nome,
                                                                  UsuarioPossuiSegmento = (cv.Funcoes == null) ? false : cv.Segmentos.Any(x => x.Nome == seg.Nome)
                                                              });
            }

            return View(cv);
        }

        [HttpPost]
        public RedirectToRouteResult GravaSegmentos(FormCollection formCollection)
        {
            _candidato.RemoverSegmentosDoCandidato(Convert.ToInt32(formCollection["Id"]));

            var usuario = _servicoDeAutenticacao.UsuarioAutenticado();

            CandidatoViewModel cv = usuario.Perfil == Perfil.Candidato ?
                                    GetCandidatoViewModel(usuario) : GetCandidatoViewModel(formCollection["Id"]);

            cv.Segmentos = new List<Segmento>();
            cv.OutrosSegmentos = formCollection["OutrosSegmentos"];

            if (formCollection["segmentos"] != null)
            {
                var segmentosMarcados = formCollection["segmentos"].Split(',');

                foreach (var s in segmentosMarcados)
                {
                    var segmento = _segmento.FindByDescr(s);
                    cv.Segmentos.Add(segmento);
                }
            }

            var candidato = ViewModelParaModel(cv, EtapaCadastro.Segmentos);
            _candidato.SaveCandidato(candidato);

            return RedirectToAction("ExperienciaProfissional", new { idCandidato = candidato.Id });
        }

        private CandidatoViewModel ModelToViewModel(Candidato candidato)
        {
            var cv = new CandidatoViewModel()
                         {
                             Id = candidato.Id,
                             Cpf = candidato.Cpf,
                             Rg = candidato.Rg,
                             DataExpedicaoRg = ArrumarCampoData(candidato.DataExpedicaoRg),
                             UfExpedicaoRg = candidato.UfExpedicaoRg,
                             Nome = candidato.Nome,
                             DataNascimento = ArrumarCampoData(candidato.DataNascimento),
                             NomeDoPai = candidato.NomeDoPai,
                             NomeDaMae = candidato.NomeDaMae,
                             Endereco = candidato.Endereco,
                             Numero = candidato.Numero,
                             Complemento = candidato.Complemento,
                             Bairro = candidato.Bairro,
                             Cidade = candidato.Cidade,
                             Estado = candidato.Estado,
                             CEP = candidato.CEP,
                             Regiao = candidato.Regiao,
                             TelefoneCelular = candidato.TelefoneCelular,
                             TelefoneComercial = candidato.TelefoneComercial,
                             TelefoneRecado = candidato.TelefoneRecado,
                             TelefoneResidencial = candidato.TelefoneResidencial,
                             Email = candidato.Email,
                             CarteiraTrabNumero = candidato.CarteiraTrabNumero,
                             CarteiraTrabSerie = candidato.CarteiraTrabSerie,
                             CarteiraTrabUf = candidato.CarteiraTrabUf,
                             PisNumero = candidato.PisNumero,
                             PisDataEmissao = ArrumarCampoData(candidato.PisDataEmissao),
                             HabilitacaoNumero = candidato.HabilitacaoNumero,
                             HabilitacaoCategoria = candidato.HabilitacaoCategoria,
                             PossuiVeiculo = candidato.PossuiVeiculo,
                             TipoVeiculo = candidato.TipoVeiculo,
                             Sexo = candidato.Sexo,
                             PossuiFilhos = candidato.PossuiFilhos,
                             QtdeFilhos = candidato.QtdeFilhos,
                             EstadoCivil = candidato.EstadoCivil,
                             NaturalidadeCidade = candidato.NaturalidadeCidade,
                             NaturalidadeUf = candidato.NaturalidadeUf,
                             Nacionalidade = candidato.Nacionalidade,
                             CargoPretendido = candidato.CargoPretendido,
                             ComoFicouSabendoDaVaga = candidato.ComoFicouSabendoDaVaga,
                             UltimoSalario = candidato.UltimoSalario,
                             PretensaoSalarial = candidato.PretensaoSalarial,
                             Manequim = candidato.Manequim,
                             Altura = candidato.Altura,
                             Peso = candidato.Peso,
                             Calcado = candidato.Calcado,
                             CorDosOlhos = candidato.CorDosOlhos,
                             CorDoCabelo = candidato.CorDoCabelo,
                             CorDaPele = candidato.CorDaPele,
                             FotoRosto = candidato.FotoRosto,
                             FotoCabelo = candidato.FotoCabelo,
                             FotoCorpoFrente = candidato.FotoCorpoFrente,
                             FotoOlhos = candidato.FotoOlhos,
                             FotoCorpoLado = candidato.FotocorpoLado,
                             TitularContaBancaria = candidato.TitularContaBancaria,
                             Banco = candidato.Banco,
                             TipoConta = candidato.TipoConta,
                             Agencia = candidato.Agencia,
                             ContaCorrente = candidato.ContaCorrente,
                             Escolaridade = candidato.Escolaridade,
                             AnoConclusao = candidato.AnoConclusao,
                             NomeDoCursoSup = candidato.NomeDoCursoSup,
                             NomeDoCursoSupIncompleto = candidato.NomeDoCursoSupIncompleto,
                             NomeDoCursoTecnico = candidato.NomeDoCursoTecnico,
                             ConhecimentoAccess = candidato.ConhecimentoAccess,
                             ConhecimentoExcel = candidato.ConhecimentoExcel,
                             ConhecimentoInternet = candidato.ConhecimentoInternet,
                             ConhecimentoOutlook = candidato.ConhecimentoOutlook,
                             ConhecimentoPowerPoint = candidato.ConhecimentoPowerPoint,
                             ConhecimentoWord = candidato.ConhecimentoWord,
                             AccessNivelConhecimento = candidato.AccessNivelConhecimento,
                             ExcelNivelConhecimento = candidato.ExcelNivelConhecimento,
                             PowerPointNivelConhecimento = candidato.PowerPointNivelConhecimento,
                             InternetNivelConhecimento = candidato.InternetNivelConhecimento,
                             OutlookNivelConhecimento = candidato.OutlookNivelConhecimento,
                             WordNivelConhecimento = candidato.WordNivelConhecimento,
                             PossuiIngles = candidato.PossuiIngles,
                             InglesNivelConhecimento = candidato.InglesNivelConhecimento,
                             OutrosConhecimentos = candidato.OutrosConhecimentos,
                             Idioma2Nome = candidato.Idioma2Nome,
                             Idioma2NivelConhecimento = candidato.Idioma2NivelConhecimento,
                             Idioma3Nome = candidato.Idioma3Nome,
                             Idioma3NivelConhecimento = candidato.Idioma3NivelConhecimento,
                             Funcoes = (IList<Funcao>)candidato.Funcoes,
                             OutrasFuncoes = candidato.OutrasFuncoes,
                             Segmentos = (IList<Segmento>)candidato.Segmentos,
                             OutrosSegmentos = candidato.OutrosSegmentos,
                             ExperienciasProfissionais = (IList<ExperienciaProfissional>)candidato.ExperienciasProfissionais,
                             DataAlteracao = candidato.DataAlteracao.ToString()
                         };
            return cv;

        }

        private string ArrumarCampoData(DateTime? date)
        {
            if (date.ToString() == "01/01/0001 00:00:00" || date == null)
            {
                return "";
            }
            else
            {
                //O toString do candidato não permite essa formatação
                return Convert.ToDateTime(date).ToString("dd/MM/yyyy");
            }
        }

        private CandidatoViewModel GetCandidatoViewModel(string id)
        {
            var cv = new CandidatoViewModel();
            var candidato = _candidato.GetCandidato(Convert.ToInt32(id));
            if (candidato != null)
            {
                cv = ModelToViewModel(candidato);
            }

            return cv;
        }

        private CandidatoViewModel GetCandidatoViewModel(Usuario usuario)
        {
            var cv = new CandidatoViewModel();

            //var usuario = _servicoDeAutenticacao.UsuarioAutenticado();

            var candidato = _candidato.GetCandidatoByUsuarioId(usuario.Id);

            if (candidato != null)
            {
                cv = ModelToViewModel(candidato);
                cv.Nome = (string.IsNullOrEmpty(cv.Nome)) ? usuario.Nome : cv.Nome;
                cv.Cpf = (string.IsNullOrEmpty(cv.Cpf)) ? usuario.Cpf : cv.Cpf;
                cv.Email = (string.IsNullOrEmpty(cv.Email)) ? usuario.Email : cv.Email;

            }
            else
            {
                cv.Nome = usuario.Nome;
                cv.Cpf = usuario.Cpf;
                cv.Email = usuario.Email;
            }

            return cv;
        }

        private Candidato ViewModelParaModel(CandidatoViewModel cv, EtapaCadastro etapaCadastro)
        {
            var c = new Candidato();

            if (cv.Id > 0)
            {
                c = _candidato.GetCandidato(cv.Id);
            }

            if (c.Usuario == null)
            {
                var usuario = _servicoDeAutenticacao.UsuarioAutenticado();
                if (usuario.Perfil == Perfil.Candidato)
                {
                    c.Usuario = usuario;
                }
            }

            if (c.DataCadastro == null)
            {
                c.DataCadastro = DateTime.Now;
            }

            c.DataAlteracao = DateTime.Now;
            switch (etapaCadastro)
            {
                case EtapaCadastro.DadosPessoais:
                    c.Nome = cv.Nome;
                    c.DataNascimento = (string.IsNullOrEmpty(cv.DataNascimento)) ? (DateTime?)null : Convert.ToDateTime(String.Format(cv.DataNascimento, "dd/MM/yyyy"));
                    c.NomeDoPai = cv.NomeDoPai;
                    c.NomeDaMae = cv.NomeDaMae;
                    c.Endereco = cv.Endereco;
                    c.Numero = cv.Numero;
                    c.Complemento = cv.Complemento;
                    c.Bairro = cv.Bairro;
                    c.Cidade = cv.Cidade;
                    c.Estado = cv.Estado;
                    c.CEP = cv.CEP;
                    c.Regiao = cv.Regiao;
                    c.TelefoneCelular = cv.TelefoneCelular;
                    c.TelefoneComercial = cv.TelefoneComercial;
                    c.TelefoneRecado = cv.TelefoneRecado;
                    c.TelefoneResidencial = cv.TelefoneResidencial;
                    c.Email = cv.Email;
                    c.SituacaoCandidato = _situacaoCandidatos.GetSituacao(1);
                    break;
                case EtapaCadastro.Documentos:
                    c.Cpf = cv.Cpf;
                    c.Rg = cv.Rg;
                    c.DataExpedicaoRg = (string.IsNullOrEmpty(cv.DataExpedicaoRg)) ? (DateTime?)null : Convert.ToDateTime(String.Format(cv.DataExpedicaoRg, "dd/MM/yyyy"));
                    c.UfExpedicaoRg = cv.UfExpedicaoRg;
                    c.CarteiraTrabNumero = cv.CarteiraTrabNumero;
                    c.CarteiraTrabSerie = cv.CarteiraTrabSerie;
                    c.CarteiraTrabUf = cv.CarteiraTrabUf;
                    c.PisNumero = cv.PisNumero;
                    c.PisDataEmissao = (string.IsNullOrEmpty(cv.PisDataEmissao)) ? (DateTime?)null : Convert.ToDateTime(String.Format(cv.PisDataEmissao, "dd/MM/yyyy"));
                    c.HabilitacaoNumero = cv.HabilitacaoNumero;
                    c.HabilitacaoCategoria = cv.HabilitacaoCategoria;
                    c.PossuiVeiculo = cv.PossuiVeiculo;
                    c.TipoVeiculo = cv.TipoVeiculo;
                    break;
                case EtapaCadastro.InformacoesGerais:
                    c.Sexo = cv.Sexo;
                    c.PossuiFilhos = cv.PossuiFilhos;
                    c.QtdeFilhos = cv.QtdeFilhos;
                    c.EstadoCivil = cv.EstadoCivil;
                    c.NaturalidadeCidade = cv.NaturalidadeCidade;
                    c.NaturalidadeUf = cv.NaturalidadeUf;
                    c.Nacionalidade = cv.Nacionalidade;
                    c.CargoPretendido = cv.CargoPretendido;
                    c.ComoFicouSabendoDaVaga = cv.ComoFicouSabendoDaVaga;
                    c.UltimoSalario = cv.UltimoSalario;
                    c.PretensaoSalarial = cv.PretensaoSalarial;
                    break;
                case EtapaCadastro.Caracteristicas:
                    c.Manequim = cv.Manequim;
                    c.Altura = cv.Altura;
                    c.Peso = cv.Peso;
                    c.Calcado = cv.Calcado;
                    c.CorDosOlhos = cv.CorDosOlhos;
                    c.CorDoCabelo = cv.CorDoCabelo;
                    c.CorDaPele = cv.CorDaPele;
                    c.FotoRosto = cv.FotoRosto;
                    c.FotoCabelo = cv.FotoCabelo;
                    c.FotoCorpoFrente = cv.FotoCorpoFrente;
                    c.FotoOlhos = cv.FotoOlhos;
                    c.FotocorpoLado = cv.FotoCorpoLado;
                    break;
                case EtapaCadastro.DadosBancarios:
                    c.TitularContaBancaria = cv.TitularContaBancaria;
                    c.Banco = cv.Banco;
                    c.TipoConta = cv.TipoConta;
                    c.Agencia = cv.Agencia;
                    c.ContaCorrente = cv.ContaCorrente;
                    break;
                case EtapaCadastro.Escolaridade:
                    c.Escolaridade = cv.Escolaridade;
                    c.AnoConclusao = cv.AnoConclusao;
                    c.NomeDoCursoSup = cv.NomeDoCursoSup;
                    c.NomeDoCursoSupIncompleto = cv.NomeDoCursoSupIncompleto;
                    c.NomeDoCursoTecnico = cv.NomeDoCursoTecnico;
                    break;
                case EtapaCadastro.Informatica:
                    c.ConhecimentoAccess = cv.ConhecimentoAccess;
                    c.ConhecimentoExcel = cv.ConhecimentoExcel;
                    c.ConhecimentoInternet = cv.ConhecimentoInternet;
                    c.ConhecimentoOutlook = cv.ConhecimentoOutlook;
                    c.ConhecimentoPowerPoint = cv.ConhecimentoPowerPoint;
                    c.ConhecimentoWord = cv.ConhecimentoWord;
                    c.AccessNivelConhecimento = cv.AccessNivelConhecimento;
                    c.ExcelNivelConhecimento = cv.ExcelNivelConhecimento;
                    c.PowerPointNivelConhecimento = cv.PowerPointNivelConhecimento;
                    c.InternetNivelConhecimento = cv.InternetNivelConhecimento;
                    c.OutlookNivelConhecimento = cv.OutlookNivelConhecimento;
                    c.WordNivelConhecimento = cv.WordNivelConhecimento;
                    c.OutrosConhecimentos = cv.OutrosConhecimentos;
                    break;
                case EtapaCadastro.Idiomas:
                    c.PossuiIngles = cv.PossuiIngles;
                    c.InglesNivelConhecimento = cv.InglesNivelConhecimento;
                    c.Idioma2Nome = cv.Idioma2Nome;
                    c.Idioma2NivelConhecimento = cv.Idioma2NivelConhecimento;
                    c.Idioma3Nome = cv.Idioma3Nome;
                    c.Idioma3NivelConhecimento = cv.Idioma3NivelConhecimento;
                    break;
                case EtapaCadastro.Funcoes:
                    foreach (var funcao in cv.Funcoes)
                    {
                        c.IncluirFuncao(funcao);
                    }
                    c.OutrasFuncoes = cv.OutrasFuncoes;
                    break;
                case EtapaCadastro.Segmentos:
                    foreach (var segmento in cv.Segmentos)
                    {
                        c.IncluirSegmento(segmento);
                    }
                    c.OutrosSegmentos = cv.OutrosSegmentos;
                    break;
                case EtapaCadastro.ExperienciaProfissional:
                    foreach (var exp in cv.ExperienciasProfissionais)
                    {
                        c.IncluirExperienciaProfissional(exp);
                    }
                    break;

            }
            //Funcoes - Segmentos - EXP Prof.
            //c.Usuario = cv.Usuario;
            return c;
        }

        private string SalvarArquivo(HttpPostedFileBase arquivo, string path)
        {
            var extensao = "jpg";
            var nomeEncriptado = GerarMd5ComTimeStamp(arquivo.FileName);
            path = Path.Combine(Server.MapPath(path), string.Format("{0}.{1}", nomeEncriptado, extensao));
            arquivo.SaveAs(path);
            return string.Format("{0}.{1}", nomeEncriptado, extensao);
        }

        private string GerarMd5ComTimeStamp(string nome)
        {
            ViewData["classcampoid"] = "new";
            nome += nome + DateTime.Now;
            var md5 = new MD5CryptoServiceProvider();
            var arrayDeBytesDaString = Encoding.UTF8.GetBytes(nome);
            arrayDeBytesDaString = md5.ComputeHash(arrayDeBytesDaString);
            var nomeEncriptado = new StringBuilder();

            foreach (var b in arrayDeBytesDaString)
            {
                nomeEncriptado.Append(b.ToString("x2").ToLower());
            }
            return nomeEncriptado.ToString();
        }

        enum EtapaCadastro
        {
            DadosPessoais = 1,
            Documentos = 2,
            InformacoesGerais = 3,
            Caracteristicas = 4,
            DadosBancarios = 5,
            Escolaridade = 6,
            Informatica = 7,
            Idiomas = 8,
            Funcoes = 9,
            Segmentos = 10,
            ExperienciaProfissional = 11
        }
    }

    public class JsonpResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var callback = context.HttpContext.Request["callback"];

            if (string.IsNullOrWhiteSpace(callback))
                throw new ArgumentNullException("É necessário informar a função de callback");

            if (this.Data != null)
                context.HttpContext.Response.Write(
                    string.Format("{0}({1});",
                        callback,
                        new JavaScriptSerializer().Serialize(this.Data)));
        }
    }
}
