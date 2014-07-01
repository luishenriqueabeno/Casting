using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web.Routing;
using Casting.Domain;
using Casting.Domain.Contracts.Tasks;
using Casting.Domain.enums;
using Casting.Tasks;
using Casting.Web.Mvc.Controllers.Queries;
using Casting.Web.Mvc.Controllers.ViewModels;
using SharpArch.Domain.PersistenceSupport;

namespace Casting.Web.Mvc.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        private readonly ICandidatos _candidato;
        private readonly IRepository<Usuario> _usuario;
        private readonly IServicoDeAutenticacao _servicoDeAutenticacao;
        private readonly IVagas _vagas;
        private readonly IEntrevistas _entrevistas;
        private readonly IRecrutamentos _recrutamentos;

        public HomeController(ICandidatos candidato, IRepository<Usuario> usuario, IServicoDeAutenticacao servicoDeAutenticacao, IVagas vagas, IEntrevistas entrevistas, IRecrutamentos recrutamentos)
        {
            _candidato = candidato;
            _usuario = usuario;
            _servicoDeAutenticacao = servicoDeAutenticacao;
            _vagas = vagas;
            _entrevistas = entrevistas;
            _recrutamentos = recrutamentos;
        }

        [ControleDeAcesso]
        public ActionResult Index()
        {
            var usuario = _servicoDeAutenticacao.UsuarioAutenticado();

            dynamic viewModel = new ExpandoObject();

            viewModel.usuario = usuario;
            ViewData["usuario"] = usuario;

            switch (usuario.Perfil)
            {
                case Perfil.Digitador:
                    viewModel.candidatos = new List<PesquisaCandidatoViewModel>();
                    break;

                case Perfil.Candidato:
                    var candidato =
                        _candidato.GetCandidatoByUsuarioId(_servicoDeAutenticacao.CodigoDeUsuarioAutenticado());

                    if (candidato == null)
                    {
                        return RedirectToAction("DadosPessoais", "Cadastro");
                    }

                    viewModel.candidato = ModelToViewModel(candidato);
                    break;

                case Perfil.Administrador:
                    viewModel.estatisticas = CarregaAdminHome();
                    break;

            }
            
            return View(viewModel);
         
        }

        public ActionResult Detalhes(string id)
        {
            var usuario = _servicoDeAutenticacao.UsuarioAutenticado();

            if (usuario.Perfil == Perfil.Candidato)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["usuario"] = usuario;

            var candidato = _candidato.GetCandidato(Convert.ToInt32(id));
            var cv = ModelToViewModel(candidato);

            return View(cv);
        }

        private CandidatoViewModel ModelToViewModel(Candidato candidato)
        {
            var cv = new CandidatoViewModel()
            {
                Id = candidato.Id,
                Cpf = candidato.Cpf,
                Rg = candidato.Rg,
                Idade = (candidato.DataNascimento != null) ? CalculaIdade((DateTime)candidato.DataNascimento) : "",
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
                Usuario = candidato.Usuario,
                Funcoes = (IList<Funcao>) candidato.Funcoes,
                OutrasFuncoes = candidato.OutrasFuncoes,
                Segmentos = (IList<Segmento>) candidato.Segmentos,
                OutrosSegmentos = candidato.OutrosSegmentos,
                ExperienciasProfissionais = (IList<ExperienciaProfissional>) candidato.ExperienciasProfissionais,
                DataAlteracao = candidato.DataAlteracao.ToString()
            };
            return cv;

        }

        public ActionResult GravaUsuario(UsuarioViewModel usuarioViewModel)
        {
            if (_candidato.VerificaSeCandidatoJaExiste(usuarioViewModel.Cpf))
            {
                return RedirectToAction("Login", "Home", new { msgValidacao = "CPFExistente" });
            }
            
            var usuario = ViewModelToModel(usuarioViewModel);
            _usuario.SaveOrUpdate(usuario);
            


            if (_servicoDeAutenticacao.AutenticarPorId(usuario.Id))
            {
                //return RedirectToAction("DadosPessoais", "Cadastro", new { idCandidato = usuario.Id });    
                return RedirectToAction("DadosPessoais", "Cadastro");    
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            
            
        }
        
        public Usuario ViewModelToModel(UsuarioViewModel usuarioViewModel)
        {
            var usuario = new Usuario();

            usuario.Nome = usuarioViewModel.Nome;
            usuario.Email = usuarioViewModel.Email;
            usuario.Perfil = Perfil.Candidato;
            usuario.Cpf = usuarioViewModel.Cpf;
            usuario.Senha = usuarioViewModel.Senha; //GerarSenhaCriptografada(usuarioViewModel.Senha)

            return usuario;
        }

        public ActionResult Login(string msgValidacao = null)
        {
            ViewData["UsuarioInvalido"] = "";
            ViewData["ValidaCadastro"] = msgValidacao;
            return View();
        }

        public ActionResult Logout()
        {
            _servicoDeAutenticacao.Sair();
            return RedirectToAction("Login");
        }
  
        [HttpPost]
        public ActionResult Login(string cpf, string password)
        {
            if (_servicoDeAutenticacao.Autenticar(cpf, password))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["UsuarioInvalido"] = "Invalido";
            return View();
            
        }

        public ActionResult  PesquisaCandidato(string nome, string cpf, string email, string tel)
        {

            var candidatos = _candidato.PesquisaCandidatos(cpf, nome, email, tel);

            dynamic viewModel = new ExpandoObject();

            viewModel.candidatos = ModelToPesquisaViewModel(candidatos);

            return PartialView("PartialListaDeCandidatos", viewModel);
        }

        public AdminHomeViewModel CarregaAdminHome()
        {

            var viewModel = new AdminHomeViewModel
                                {
                                    ContratacoesEsseMes = _recrutamentos.TotalRecrutamentosMesAtual(),
                                    ContratacoesUltimoMes = _recrutamentos.TotalRecrutamentosUltimoMes(),
                                    CurriculosCadastradosAnoAtual = _candidato.TotalCandidatosAnoAtual(),
                                    CurriculosCadastradosMesAtual = _candidato.TotalCandidatosMesAtual(),
                                    CurriculosCadastradosTotal = _candidato.TotalCandidatos(),
                                    Entrevistas = _entrevistas.TotalEntrevistasEmAberto(),
                                    VagasPreenchidas = _vagas.PesquisaVagasPreenchidas(),
                                    VagasEmAberto = _vagas.PesquisaVagasEmAberto()
                                };

            return viewModel;
        }
        public List<PesquisaCandidatoViewModel> ModelToPesquisaViewModel(List<Candidato> candidatos)
        {
            var pesquisaViewModel = new List<PesquisaCandidatoViewModel>();
            foreach (var candidato in candidatos)
            {
                pesquisaViewModel.Add(new PesquisaCandidatoViewModel()
                                          {
                                              Altura = candidato.Altura,
                                              CorDosOlhos = candidato.CorDosOlhos.ToString(),
                                              Escolaridade = GetEscolaridade(candidato.Escolaridade),
                                              Idade = candidato.DataNascimento.ToString(),
                                              IdCandidato = candidato.Id,
                                              Nome = candidato.Nome,
                                              Peso = candidato.Peso,
                                              Situacao = ""
                                          });
            }

            return pesquisaViewModel;
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
        public ActionResult Teste()
        {
            var candidato = _candidato.GetCandidatoByUsuarioId(_servicoDeAutenticacao.CodigoDeUsuarioAutenticado());

            dynamic objetoDinamico = new ExpandoObject();
            objetoDinamico.candidato = candidato;
            objetoDinamico.usuario = _servicoDeAutenticacao.UsuarioAutenticado();
            return View(objetoDinamico);
        }

        private string CalculaIdade(DateTime dataNascimento)
        {

            var idade = DateTime.Today.Year - dataNascimento.Year;

            if (dataNascimento.Month < DateTime.Today.Month)
            {
                idade = idade - 1;
            }
            else if (dataNascimento.Month == DateTime.Today.Month)
            {
                if (dataNascimento.Day < DateTime.Today.Day)
                {
                    idade = idade - 1;
                }
            }

            idade = (idade < 0) ? 0 : idade;

            return idade.ToString();
        }

        public JsonResult ChartOrigemCandidato()
        {

            var result = GerarGrafico();

            return Json(new { result.chart, result.data }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CarregaEntrevistasMarcadas()
        {

            var result = GeraEntrevistasParaFullCalendar();

            //var teste = Json(new { result.header, result.events }, JsonRequestBehavior.AllowGet);
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private List<FullCalendarViewModel> GeraEntrevistasParaFullCalendar()
        {
            var entrevistas = _entrevistas.ListaEntrevistasEmAberto();

            var eventos = new List<FullCalendarViewModel>();
            
            foreach (var entrevista in entrevistas)
            {
                var calendario = new FullCalendarViewModel
                        {
                            id = entrevista.Id.ToString(),
                            date = entrevista.DataEntrevista.ToShortDateString(),
                            start = entrevista.DataEntrevista.Date.ToString("yyyy-MM-dd") + "T" + entrevista.HorarioEntrevista,
                            end = entrevista.DataEntrevista.Date.ToString("yyyy-MM-dd") + "T" + entrevista.HorarioEntrevista,
                            url = "Administracao/DetalhesCandidato/" + entrevista.Candidato.Id,
                            title = "Candidato: " + entrevista.Candidato.Nome + "\nVaga: " + entrevista.Vaga.Titulo,
                            allDay = false
                        };
                
                eventos.Add(calendario);
            }

            return eventos;
        }

        public ChartViewModel GerarGrafico()
        {
            var cores = new List<string>
                            {"429EAD", "AD42A2", "D4AC31", "AC3D41", "FFAAAA", "D34422", "095531", "42D1A8", "450210", "E2A272", "A300ff"};
            //Configuração
            var chart = new Chart() { caption = "", showlegend = "0", showpercentageinlabel = "0"};

            var candidatos = _candidato.TotalCandidatosPorOrigem();

            var totalCandidatos = _candidato.TotalCandidatos();
            
            //Categorias -> Itens da reta X
            var listCategories = new List<ChartCategories>();
            var i = 0;

            foreach (var candidato in candidatos)
            {
                var itemRelatorio = (object[])candidato;
                listCategories.Add(new ChartCategories() { 
                    label = itemRelatorio[0].ToString(),
                    value = itemRelatorio[1].ToString(),
                    color = cores[i]

                });
                i++;
            }
            
            return new ChartViewModel(){ chart = chart, data = listCategories};
        }

        private string GetEscolaridade(Escolaridade escolaridade)
        {
            switch (escolaridade)
            {
                case Escolaridade.EnsinoFundamentalIncompleto:
                    return "Ensino Fundamental Incompleto";
                case Escolaridade.EnsinoFundamentalCompleto:
                    return "Ensino Fundamental Completo";
                case Escolaridade.EnsinoMedioIncompleto:
                    return "Ensino Fundamental Incompleto";
                case Escolaridade.EnsinoMedioCompleto:
                    return "Ensino Médio Completo";
                case Escolaridade.EnsinoSuperiorIncompleto:
                    return "Ensino Superior Incompleto";
                case Escolaridade.EnsinoSuperiorCompleto:
                    return "Ensino Superior Completo";
                case Escolaridade.PosGraduaçãoIncompleta:
                    return "Pós Graduação Incompleta";
                case Escolaridade.PosGraduaçãoCompleta:
                    return "Pós Graduação Completa";
                case Escolaridade.CursoTecnico:
                    return "Curso Técnico";
            }

            return "Nenhuma";
        }
    }
}
