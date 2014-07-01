using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Casting.Domain;
using Casting.Domain.Contracts.Tasks;
using Casting.Domain.enums;
using Casting.Web.Mvc.Controllers.Queries;
using Casting.Web.Mvc.Controllers.ViewModels;
using Casting.Web.Mvc.Helpers;
using Rotativa;

namespace Casting.Web.Mvc.Controllers
{
    public class AdministracaoController : Controller
    {
        private readonly IServicoDeAutenticacao _servicoDeAutenticacao;
        private readonly IUsuarios _usuarios;
        private readonly ICandidatos _candidatos;
        private readonly ISegmentos _segmento;
        private readonly IFuncaos _funcao;
        private readonly IVagas _vagas;
        private readonly IClientes _clientes;
        private readonly IBancos _bancos;
        private readonly IAvaliacoes _avaliacoes;
        private readonly ISituacaoCandidatos _situacaoCandidatos;
        private readonly IEntrevistas _entrevistas;
        private readonly IRecrutamentos _recrutamentos;

        public AdministracaoController(IUsuarios usuarios, IServicoDeAutenticacao servicoDeAutenticacao, IClientes clientes, ICandidatos candidatos, ISegmentos segmento, IFuncaos funcao, IVagas vagas, IBancos bancos, IAvaliacoes avaliacoes, ISituacaoCandidatos situacaoCandidatos, IEntrevistas entrevistas, IRecrutamentos recrutamentos)
        {
            _usuarios = usuarios;
            _servicoDeAutenticacao = servicoDeAutenticacao;
            _clientes = clientes;
            _candidatos = candidatos;
            _segmento = segmento;
            _funcao = funcao;
            _vagas = vagas;
            _bancos = bancos;
            _avaliacoes = avaliacoes;
            _situacaoCandidatos = situacaoCandidatos;
            _entrevistas = entrevistas;
            _recrutamentos = recrutamentos;
            GetUsuario();
        }

        public ActionResult BuscarCandidato()
        {
            var viewModel = new PesquisaAdminViewModel { Funcaos = _funcao.ListaFuncoes(), Segmentos = _segmento.ListaSegmentos() };

            return View(viewModel);
        }

        //[HttpPost]
        //public ActionResult BuscarCandidato(AuxiliarDePesquisaViewModel auxiliarDePesquisaViewModel)
        //{
        //    var candidatos = _candidatos.PesquisaCandidatos(auxiliarDePesquisaViewModel);

        //    var viewModel = ModelToViewModel(candidatos);

        //    return View(viewModel);
        //}

        public ActionResult BuscarVaga()
        {
            var viewModel = new PesquisaVagaViewModel
                                {
                                    Clientes = _clientes.ListaClientes()
                                };

            return View(viewModel);
        }


        public ActionResult PesquisarVagas(JQueryDataTableModel param)
        {
            var cliente = Request["filtro_empresa"];
            var titulo = Request["filtro_titulo_vaga"];

            var sortColumn = "Titulo";
            var sortAlpha = Request["sSortDir_0"];
            var pagina = GetNumeroDaPagina(param.iDisplayStart, param.iDisplayLength);
            var numeroDeRegistros = param.iDisplayLength;

            var totalRegistros = 0;

            var vagasPesquisadas = RecuperarVagas(cliente, titulo, pagina, numeroDeRegistros, out totalRegistros,
                                            sortColumn, sortAlpha);

            if (vagasPesquisadas.Count > 0)
            {
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalRegistros,
                    iTotalDisplayRecords = totalRegistros,
                    aaData = vagasPesquisadas
                },

                JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = new List<VagasPesquisadasViewModel>();
                list.Add(new VagasPesquisadasViewModel());
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalRegistros,
                    iTotalDisplayRecords = totalRegistros,
                    aaData = list
                },

               JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult BuscaAvancada()
        {
            var viewModel = CarregaDadosBuscaAvancada();

            return View(viewModel);
        }

        public ActionResult PesquisaCandidatos(JQueryDataTableModel param)
        {
            var sortColumn = "Titulo";
            var sortAlpha = Request["sSortDir_0"];
            var pagina = GetNumeroDaPagina(param.iDisplayStart, param.iDisplayLength);
            var numeroDeRegistros = param.iDisplayLength;

            if (string.IsNullOrEmpty(Request["pesquisar"])) return Json(new { }, JsonRequestBehavior.AllowGet);

            var auxiliarDePesquisaViewModel = CarregaAuxiliarDePesquisa();

            var totalRegistros = _candidatos.PesquisaCandidatosTotal(auxiliarDePesquisaViewModel);

            var candidatos = _candidatos.PesquisaCandidatos(auxiliarDePesquisaViewModel,
                pagina, numeroDeRegistros, sortColumn, sortAlpha);

            if (candidatos.Count > 0)
            {
                var viewModel = ModelToPesquisaAvancadaViewModel(candidatos);


                return Json(new
                                {
                                    sEcho = param.sEcho,
                                    iTotalRecords = totalRegistros,
                                    iTotalDisplayRecords = totalRegistros,
                                    aaData = viewModel
                                },

                            JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = new List<CandidatoPesquisado>();
                list.Add(new CandidatoPesquisado());
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalRegistros,
                    iTotalDisplayRecords = totalRegistros,
                    aaData = list
                },

                          JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult PesquisaCandidatosAvancado(JQueryDataTableModel param)
        {
            var sortColumn = "Titulo";
            var sortAlpha = Request["sSortDir_0"];
            var pagina = GetNumeroDaPagina(param.iDisplayStart, param.iDisplayLength);
            var numeroDeRegistros = param.iDisplayLength;

            if (string.IsNullOrEmpty(Request["pesquisar"])) return Json(new { });

            var auxiliarDePesquisaViewModel = CarregaAuxiliarDePesquisaAvancado();

            var totalRegistros = _candidatos.PesquisaCandidatosAvancadaTotal(auxiliarDePesquisaViewModel);

            var candidatos = _candidatos.PesquisaCandidatosAvancada(auxiliarDePesquisaViewModel,
                pagina, numeroDeRegistros, sortColumn, sortAlpha);

            if (candidatos.Count > 0)
            {
                var viewModel = ModelToPesquisaAvancadaViewModel(candidatos);

                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalRegistros,
                    iTotalDisplayRecords = totalRegistros,
                    aaData = viewModel
                },

                JsonRequestBehavior.AllowGet);
            }
            else
            {
                var list = new List<CandidatoPesquisado>();
                list.Add(new CandidatoPesquisado());
                return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = totalRegistros,
                    iTotalDisplayRecords = totalRegistros,
                    aaData = list
                },
                JsonRequestBehavior.AllowGet);
            }



            return Json(new { });
        }

        private AuxiliarDePesquisaViewModel CarregaAuxiliarDePesquisa()
        {
            var aux = new AuxiliarDePesquisaViewModel
            {
                Nome = Request["nome"],
                Periodo1 = Request["periodo1"],
                Periodo2 = Request["periodo2"],
                Sexo = Request["sexo"],
                ExperienciaProf = Request["experiencias"],
                Regiao = Request["regiao"],
                Funcoes = (Request["funcoes"] != "null") ? new List<string>(Request["funcoes"].Split(',')) : null,
                Segmentos = (Request["segmentos"] != "null") ? new List<string>(Request["segmentos"].Split(',')) : null
            };

            return aux;

        }

        private AuxiliarDePesquisaViewModel CarregaAuxiliarDePesquisaAvancado()
        {
            var aux = new AuxiliarDePesquisaViewModel
                          {
                              Nome = Request["nome"],
                              IdadeMinima = Request["IdadeMinima"],
                              IdadeMaxima = Request["IdadeMaxima"],
                              Sexo = Request["sexo"],
                              EstadoCivil = (Request["estadoCivil"] != "null") ? new List<string>(Request["estadoCivil"].Split(',')) : null,
                              Escolaridade = (Request["escolaridade"] != "null") ? new List<string>(Request["escolaridade"].Split(',')) : null,
                              //"formacaoAcademica", "clientes", "projetos"
                              Informatica = (Request["informatica"] != "null") ? new List<string>(Request["informatica"].Split(',')) : null,
                              Idiomas = (Request["Idiomas"] != "null") ? new List<string>(Request["Idiomas"].Split(',')) : null,
                              Bairro = Request["bairro"],
                              Cidade = Request["cidade"],
                              Uf = (Request["uf"] != "null") ? new List<string>(Request["uf"].Split(',')) : null,
                              Cep = Request["cep"],
                              Regioes = (Request["Regioes"] != "null") ? new List<string>(Request["Regioes"].Split(',')) : null,
                              CategoriaCNH = (Request["CategoriaCNH"] != "null") ? new List<string>(Request["CategoriaCNH"].Split(',')) : null,
                              Veiculo = (Request["veiculo"] != "null") ? new List<string>(Request["veiculo"].Split(',')) : null,
                              Bancos = (Request["Bancos"] != "null") ? new List<string>(Request["Bancos"].Split(',')) : null,
                              TipoConta = (Request["tipoConta"] != "null") ? new List<string>(Request["tipoConta"].Split(',')) : null,
                              Funcoes = (Request["funcoes"] != "null") ? new List<string>(Request["funcoes"].Split(',')) : null,
                              Segmentos = (Request["segmentos"] != "null") ? new List<string>(Request["segmentos"].Split(',')) : null,
                              ExperienciaProf = Request["experiencias"],
                              ManequimMinimo = Request["manequimMinimo"],
                              ManequimMaximo = Request["manequimMaximo"],
                              AlturaMinima = Request["alturaMinima"],
                              AlturaMaxima = Request["alturaMaxima"],
                              PesoMinimo = Request["pesoMinimo"],
                              PesoMaximo = Request["pesoMaximo"],
                              CalcadoMinimo = Request["calcadoMinimo"],
                              CalcadoMaximo = Request["calcadoMaximo"],
                              CorDosOlhos = (Request["CorDosOlhos"] != "null") ? new List<string>(Request["CorDosOlhos"].Split(',')) : null,
                              CorDoCabelo = (Request["CorDoCabelo"] != "null") ? new List<string>(Request["CorDoCabelo"].Split(',')) : null,
                              CorDaPele = (Request["CorDaPele"] != "null") ? new List<string>(Request["CorDaPele"].Split(',')) : null,
                              AvaliacaoCabelo = (Request["avaliacaoCabelo"] != "null") ? new List<string>(Request["avaliacaoCabelo"].Split(',')) : null,
                              AvaliacaoCorpo = (Request["avaliacaoCorpo"] != "null") ? new List<string>(Request["avaliacaoCorpo"].Split(',')) : null,
                              AvaliacaoDentes = (Request["avaliacaoDentes"] != "null") ? new List<string>(Request["avaliacaoDentes"].Split(',')) : null,
                              AvaliacaoPele = (Request["avaliacaoPele"] != "null") ? new List<string>(Request["avaliacaoPele"].Split(',')) : null,
                              PerfilComportamental = (Request["perfilComportamental"] != "null") ? new List<string>(Request["perfilComportamental"].Split(',')) : null,
                              ConhecimentoMerchandising = (Request["merchandising"] != "null") ? new List<string>(Request["merchandising"].Split(',')) : null,
                              Peculiaridades = (Request["peculiaridades"] != "null") ? new List<string>(Request["peculiaridades"].Split(',')) : null,
                              Padrao = (Request["padrao"] != "null") ? new List<string>(Request["padrao"].Split(',')) : null,
                              PerfilProfissional = (Request["perfilProfissional"] != "null") ? new List<string>(Request["perfilProfissional"].Split(',')) : null,
                              Projetos = (Request["projetos"] != "null") ? new List<string>(Request["projetos"].Split(',')) : null
                          };

            return aux;

        }

        private IList<VagasPesquisadasViewModel> RecuperarVagas(string cliente, string titulo, int pagina,
            int numeroDeRegistros, out int totalRegistros, string orderCol = "", string orderDir = "")
        {

            var vagas = _vagas.PesquisarVagas(titulo, int.Parse(cliente), pagina, numeroDeRegistros, orderCol, orderDir);

            totalRegistros = _vagas.TotalDeVagasPesquisadas(titulo, int.Parse(cliente));

            var viewModel = ModelToViewModel(vagas);

            return viewModel;
        }

        private static int GetNumeroDaPagina(int inicio, int comprimento)
        {
            return inicio > 0 ? (comprimento + inicio) / comprimento : 1;
        }

        private PesquisaAvancadaAdminViewModel CarregaDadosBuscaAvancada()
        {

            return new PesquisaAvancadaAdminViewModel
                                {
                                    Funcaos = _funcao.ListaFuncoes(),
                                    Segmentos = _segmento.ListaSegmentos(),
                                    Bancos = _bancos.ListaBancos(),
                                    Clientes = _clientes.ListaClientes(),
                                    TipoDePeles = _avaliacoes.ListaTiposDePele(),
                                    CabeloTipos = _avaliacoes.ListaTiposCabelo(),
                                    TipoFisicos = _avaliacoes.ListaTipoFisicos(),
                                    AvaliacaoDentes = _avaliacoes.ListaAvaliacaoDentesSorriso(),
                                    PerfilProfissionals = _avaliacoes.ListaPerfilProfissional(),
                                    PerfilComportalmentals = _avaliacoes.ListaPerfilComportalmental(),
                                    ConhecimentoMerchandisings = _avaliacoes.ListaConhecimentoMerchandising(),
                                    ProjetosParticipados = _avaliacoes.ListaProjetosParticipados()
                                };
        }

        public ActionResult CadastrarUsuario()
        {
            return View(new UsuarioViewModel());
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(UsuarioViewModel usuarioViewModel)
        {
            var usuario = viewModelToModel(usuarioViewModel);
            _usuarios.SaveUsuario(usuario);
            return View();
        }

        public ActionResult CadastrarCliente()
        {
            return View(new ClienteViewModel());
        }

        [HttpPost]
        public ActionResult CadastrarCliente(ClienteViewModel clienteViewModel)
        {
            var cliente = viewModelToModel(clienteViewModel);
            _clientes.SaveCliente(cliente);
            return View();
        }

        public ActionResult DetalhesCandidato(string id)
        {
            var usuario = _servicoDeAutenticacao.UsuarioAutenticado();

            if (usuario.Perfil == Perfil.Candidato)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["usuario"] = usuario;

            var candidato = _candidatos.GetCandidato(Convert.ToInt32(id));
            var cv = ModelToViewModel(candidato);

        

            
            return View(cv);
        }

        private void GetUsuario()
        {
            var usuario = _servicoDeAutenticacao.UsuarioAutenticado();
            ViewData["usuario"] = usuario;
        }

        public ActionResult CadastrarVaga(string idVaga = null)
        {
            var viewModel = new VagaViewModel();

            if (idVaga != null)
            {
                var vaga = _vagas.PesquisarVaga(Convert.ToInt32(idVaga));
                viewModel.Id = vaga.Id;
                viewModel.DescricaoVaga = vaga.Descricao;
                viewModel.TituloVaga = vaga.Titulo;
                viewModel.TotalVagasDisponivel = vaga.TotalVagasDisponiveis;
                viewModel.TotalVagasPreenchidas = vaga.TotalVagasPreenchidas;
                viewModel.Cliente = vaga.Cliente;
            }

            viewModel.Clientes = _clientes.ListaClientes();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CadastrarVaga(VagaViewModel vagaViewModel)
        {
            var vaga = viewModelToModel(vagaViewModel);
            _vagas.SaveVaga(vaga);

            var viewModel = new VagaViewModel
            {
                Id = vaga.Id,
                DescricaoVaga = vaga.Descricao,
                TituloVaga = vaga.Titulo,
                Cliente = vaga.Cliente,
                TotalVagasDisponivel = vaga.TotalVagasDisponiveis,
                TotalVagasPreenchidas = vaga.TotalVagasPreenchidas,
                Clientes = _clientes.ListaClientes()
            };

            return View(viewModel);
        }

        public ActionResult AvaliacaoAparencia(string idCandidato)
        {
            var viewModel = ModelToViewModel(idCandidato);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AvaliacaoAparencia(AvaliacaoViewModel avaliacaoViewModel)
        {

            var avaliacao = AvaliacaoAparenciaFormToModel(avaliacaoViewModel);
            _avaliacoes.SaveAvaliacao(avaliacao);

            var recrutamento =
                _recrutamentos.GetRecrutamento(avaliacao.Vaga, avaliacao.Candidato)
                    ?? new Recrutamento
                            {
                                Candidato = avaliacao.Candidato,
                                Vaga = avaliacao.Vaga
                            };

            recrutamento.StatusCandidato = StatusCandidato.ProcessoDeSelecao;

            _recrutamentos.SaveRecrutamento(recrutamento);

            return RedirectToAction("AvaliacaoGeral", "Administracao", new { idAvaliacao = avaliacao.Id, idCandidato = avaliacao.Candidato.Id });
        }

        public ActionResult AvaliacaoGeral(string idAvaliacao, string idCandidato)
        {
            if (idAvaliacao == "" || idAvaliacao == "0" || idCandidato == "" || idCandidato == "0")
            {
                return RedirectToAction("Index", "Home");
            }

            var avaliacao = _avaliacoes.GetAvaliacao(Convert.ToInt32(idAvaliacao));

            if (avaliacao == null)
            {
                return RedirectToAction("AvaliacaoGeral", "Administracao", new { idCandidato });
            }

            var viewModel = new AvaliacaoViewModel
            {
                PerfilComportalmentals = _avaliacoes.ListaPerfilComportalmental(),
                PerfilProfissionals = _avaliacoes.ListaPerfilProfissional(),
                ConhecimentoMerchandisings = _avaliacoes.ListaConhecimentoMerchandising(),
                IdCandidato = Convert.ToInt32(idCandidato),
                IdAvaliacao = Convert.ToInt32(idAvaliacao)
            };

            return View(viewModel);

        }

        [HttpPost]
        public ActionResult AvaliacaoGeral(FormCollection formCollection)
        {

            var avaliacao = ViewModelToModel(formCollection);

            _avaliacoes.SaveAvaliacao(avaliacao);

            return RedirectToAction("DetalhesCandidato", "Administracao", new { id = avaliacao.Candidato.Id });
        }

        public ActionResult Relatorio(int idCandidato)
        {
            var viewModel = ModelToViewModel(_candidatos.GetCandidato(idCandidato));

            return View(viewModel);
        }


        public ActionResult RelatorioPartial(int idCandidato)
        {
            var viewModel = ModelToViewModel(_candidatos.GetCandidato(idCandidato));

            return PartialView("PartialRelatorioConteudoImpressao", viewModel);
        }
        public ActionResult PrintRelatorioToPdf(int idCandidato)
        {
            return new ActionAsPdf("RelatorioPartial", new { idCandidato }) { FileName = "RelatorioCandidato_" + idCandidato + ".pdf" };
        }

        public void DesligaCandidato(int idCandidato, string motivoDesligamento)
        {

            var candidato = _candidatos.GetCandidato(idCandidato);
            var recrutamento = _recrutamentos.GetRecrutamentoAtualDoCandidato(candidato);

            if (recrutamento == null) return;

            if (recrutamento.Vaga.TotalVagasPreenchidas > 0 && recrutamento.StatusCandidato == StatusCandidato.Aprovado)
            {
                recrutamento.Vaga.TotalVagasPreenchidas = recrutamento.Vaga.TotalVagasPreenchidas - 1;
            }

            recrutamento.StatusCandidato = StatusCandidato.DesligadoDaVaga;
            recrutamento.MotivoDesligamento = motivoDesligamento;

            _recrutamentos.SaveRecrutamento(recrutamento);
        }

        public ActionResult PesquisaEntrevistasMarcadasPagaVaga(int vagaId)
        {
            var entrevistas = _entrevistas.EntrevistasEmAbertoDaVaga(vagaId);
            var vaga = _vagas.PesquisarVaga(vagaId);
            var viewModel = ModelToViewModel(entrevistas, vaga);

            return PartialView("PartialListaDeEntrevistasDaVaga", viewModel);
        }

        public ActionResult PesquisaPessoasContratadasParaVaga(int vagaId)
        {
            var recrutamentos = _recrutamentos.RecrutamentosDaVaga(vagaId);
            var vaga = _vagas.PesquisarVaga(vagaId);
            var viewmodel = ModelToViewModel(recrutamentos, vaga);

            return PartialView("PartialListaDePessoasDaVaga", viewmodel);
        }

        public ActionResult AvaliacaoDetalhada(int avaliacaoId)
        {
            var avaliacao = _avaliacoes.GetAvaliacao(avaliacaoId);
            var avaliacaoViewModel = ModelToViewModel(avaliacao);

            return PartialView("PartialAvaliacaoCandidato", avaliacaoViewModel);
        }

        private PesquisaVagaViewModel ModelToViewModel(List<Recrutamento> recrutamentos, Vaga vaga)
        {
            var viewModel = new PesquisaVagaViewModel();

            var listaDeRecrutados = new List<PesquisaCandidatoViewModel>();

            foreach (var recrutamento in recrutamentos)
            {
                listaDeRecrutados.Add(new PesquisaCandidatoViewModel
                                                {
                                                    Altura = recrutamento.Candidato.Altura,
                                                    CorDosOlhos = recrutamento.Candidato.CorDosOlhos.ToString(),
                                                    Nome = recrutamento.Candidato.Nome,
                                                    Escolaridade = recrutamento.Candidato.Escolaridade.ToString(),
                                                    Idade = (recrutamento.Candidato.DataNascimento != null) ? CalculaIdade((DateTime)recrutamento.Candidato.DataNascimento) : "",
                                                    IdCandidato = recrutamento.Candidato.Id,
                                                    Situacao = recrutamento.Candidato.SituacaoCandidato.Nome,
                                                    Peso = recrutamento.Candidato.Peso,
                                                    Foto = (string.IsNullOrEmpty(recrutamento.Candidato.FotoRosto))
                                                       ? (string.IsNullOrEmpty(recrutamento.Candidato.FotoCorpoFrente)) ?
                                                               "" : recrutamento.Candidato.FotoCorpoFrente : recrutamento.Candidato.FotoRosto
                                                });
            }

            viewModel.TituloDaVagaParaDetalhes = vaga.Titulo;
            viewModel.NomeDoClienteParaDetalhes = vaga.Cliente.Nome;
            viewModel.PessoasNaVaga = listaDeRecrutados;

            return viewModel;
        }

        private PesquisaVagaViewModel ModelToViewModel(List<Entrevista> entrevistas, Vaga vaga)
        {
            var viewModel = new PesquisaVagaViewModel();
            var listaDeEntrevistas = new List<EntrevistasDaVaga>();

            foreach (var entrevista in entrevistas)
            {
                listaDeEntrevistas.Add(new EntrevistasDaVaga
                    {
                        Nome = entrevista.Candidato.Nome,
                        Escolaridade = entrevista.Candidato.Escolaridade.ToString(),
                        Idade = (entrevista.Candidato.DataNascimento != null) ? CalculaIdade((DateTime)entrevista.Candidato.DataNascimento) : "",
                        IdCandidato = entrevista.Candidato.Id,
                        Situacao = entrevista.Candidato.SituacaoCandidato.Nome,
                        DataEntrevista = entrevista.DataEntrevista.ToShortDateString(),
                        HoraEntrevista = entrevista.HorarioEntrevista,
                        Foto = (string.IsNullOrEmpty(entrevista.Candidato.FotoRosto))
                                                        ? (string.IsNullOrEmpty(entrevista.Candidato.FotoCorpoFrente)) ?
                                                                "" : entrevista.Candidato.FotoCorpoFrente : entrevista.Candidato.FotoRosto
                    });
            }

            viewModel.TituloDaVagaParaDetalhes = vaga.Titulo;
            viewModel.NomeDoClienteParaDetalhes = vaga.Cliente.Nome;
            viewModel.Entrevistas = listaDeEntrevistas;

            return viewModel;
        }

        private Avaliacao AvaliacaoAparenciaFormToModel(AvaliacaoViewModel avaliacaoViewModel)
        {

            var avaliacao = _avaliacoes.GetAvaliacao(avaliacaoViewModel.IdAvaliacao) ?? new Avaliacao();

            avaliacao.Vaga = _vagas.PesquisarVaga(avaliacaoViewModel.IdVaga);
            avaliacao.Candidato = _candidatos.GetCandidato(avaliacaoViewModel.IdCandidato);
            avaliacao.SituacaoCandidato = avaliacao.Candidato.SituacaoCandidato;
            avaliacao.HorarioEntrevista = avaliacaoViewModel.HoraEntrevista;
            avaliacao.DataEntrevista = Convert.ToDateTime(String.Format(avaliacaoViewModel.DataEntrevista, "dd/MM/yyyy"));
            avaliacao.CabeloCor = avaliacaoViewModel.CabeloCor;
            avaliacao.CabeloTipo = avaliacaoViewModel.CabeloTipo;
            avaliacao.CabeloComprimento = avaliacaoViewModel.CabeloComprimento;
            avaliacao.AvaliacaoDentesSorriso = avaliacaoViewModel.AvaliacaoDentesSorriso;
            avaliacao.AvaliacaoDentesCor = avaliacaoViewModel.AvaliacaoDentesCor;
            avaliacao.TipoFisico = avaliacaoViewModel.TipoFisico;
            avaliacao.TipoDePele = avaliacaoViewModel.TipoDePele;

            avaliacao.PossuiAparelho = avaliacaoViewModel.PossuiAparelho;
            avaliacao.PrevisaoRetiradaAparelho = avaliacaoViewModel.RetiradaAparelho;

            avaliacao.PossuiCicatriz = avaliacaoViewModel.PossuiCicatriz;
            avaliacao.PossuiPiercing = avaliacaoViewModel.PossuiPiercing;
            avaliacao.PossuiTatuagem = avaliacaoViewModel.PossuiTatuagem;
            avaliacao.LocalCicatriz = avaliacaoViewModel.LocalCicatriz;
            avaliacao.LocalPiercing = avaliacaoViewModel.LocalPiercing;
            avaliacao.LocalTatuagem = avaliacaoViewModel.LocalTatuagem;

            return avaliacao;
        }

        private AvaliacaoViewModel ModelToViewModel(Avaliacao avaliacao)
        {
            var viewModel = new AvaliacaoViewModel
            {
                Cliente = avaliacao.Vaga.Cliente,
                DataEntrevista = avaliacao.DataEntrevista.ToString("dd/MM/yyyy"),
                CabeloComprimento = avaliacao.CabeloComprimento,
                CabeloCor = avaliacao.CabeloCor,
                CabeloTipo = avaliacao.CabeloTipo,
                TipoFisico = avaliacao.TipoFisico,
                AvaliacaoDentesSorriso = avaliacao.AvaliacaoDentesSorriso,
                AvaliacaoDentesCor = avaliacao.AvaliacaoDentesCor,
                PossuiAparelho = avaliacao.PossuiAparelho,
                RetiradaAparelho = avaliacao.PrevisaoRetiradaAparelho,
                PerfilPadrao = avaliacao.PerfilPadrao,
                TipoDePele = avaliacao.TipoDePele,
                PossuiCicatriz = avaliacao.PossuiCicatriz,
                LocalCicatriz = avaliacao.LocalCicatriz,
                PossuiPiercing = avaliacao.PossuiPiercing,
                LocalPiercing = avaliacao.LocalPiercing,
                PossuiTatuagem = avaliacao.PossuiTatuagem,
                LocalTatuagem = avaliacao.LocalTatuagem,
                PerfilComportalmentals = (IList<PerfilComportalmental>)avaliacao.PerfilComportalmentals,
                OutroPerfilComportalmental = avaliacao.OutroPerfilComportalmental,
                AvaliacaoConhecimentoMerchandising = avaliacao.AvaliacaoConhecimentoMerchandising,
                ConhecimentoMerchandisings = (IList<ConhecimentoMerchandising>)avaliacao.ConhecimentoMerchandisings,
                PerfilProfissionals = (IList<PerfilProfissional>)avaliacao.PerfilProfissionals,
                AnaliseFinal = avaliacao.AnaliseFinal,
                IdAvaliacao = avaliacao.Id
            };

            return viewModel;
        }

        private AvaliacaoViewModel ModelToViewModel(string idCandidato)
        {
            var avaliacao = new AvaliacaoViewModel
            {
                CabeloTipos = _avaliacoes.ListaTiposCabelo(),
                CabeloComprimentos = _avaliacoes.ListaComprimentosCabelo(),
                CabeloCores = _avaliacoes.ListaCoresCabelo(),
                AvaliacaoDentesSorrisos = _avaliacoes.ListaAvaliacaoDentesSorriso(),
                AvaliacaoDentesCores = _avaliacoes.ListaAvaliacaoDentesCor(),
                TipoFisicos = _avaliacoes.ListaTipoFisicos(),
                TiposDePele = _avaliacoes.ListaTiposDePele(),
                IdCandidato = Convert.ToInt32(idCandidato),
                Clientes = _clientes.ListaClientes(),
                NomeCandidato = _candidatos.GetCandidato(int.Parse(idCandidato)).Nome
            };

            return avaliacao;
        }

        private List<CandidatoPesquisado> ModelToPesquisaAvancadaViewModel(IList<Candidato> candidatos)
        {
            var candidatosPesquisados = new List<CandidatoPesquisado>();

            foreach (var candidato in candidatos)
            {
                candidatosPesquisados.Add(new CandidatoPesquisado
                                              {
                                                  IdCandidato = candidato.Id,
                                                  Imagem = candidato.FotoRosto,
                                                  Nome = candidato.Nome,
                                                  Altura = candidato.Altura,
                                                  CorDosOlhos = candidato.CorDosOlhos.ToString(),
                                                  Escolaridade = GetEscolaridade(candidato.Escolaridade),
                                                  Idade = (candidato.DataNascimento != null) ? CalculaIdade((DateTime)candidato.DataNascimento) : "",
                                                  Peso = candidato.Peso,
                                                  Foto = (string.IsNullOrEmpty(candidato.FotoRosto))
                                                        ? (string.IsNullOrEmpty(candidato.FotoCorpoFrente)) ?
                                                                "" : candidato.FotoCorpoFrente : candidato.FotoRosto,
                                                  Situacao = (candidato.SituacaoCandidato != null) ? candidato.SituacaoCandidato.Nome : ""
                                              }
                    );
            }

            return candidatosPesquisados;

            //return new PesquisaAvancadaAdminViewModel
            //           {
            //               CandidatoPesquisados = candidatosPesquisados,
            //               Funcaos = _funcao.ListaFuncoes(),
            //               Segmentos = _segmento.ListaSegmentos(),
            //               Bancos = _bancos.ListaBancos(),
            //               Clientes = _clientes.ListaClientes(),
            //               TipoDePeles = _avaliacoes.ListaTiposDePele(),
            //               CabeloTipos = _avaliacoes.ListaTiposCabelo(),
            //               TipoFisicos = _avaliacoes.ListaTipoFisicos(),
            //               AvaliacaoDentes = _avaliacoes.ListaAvaliacaoDentesSorriso(),
            //               PerfilProfissionals = _avaliacoes.ListaPerfilProfissional(),
            //               PerfilComportalmentals = _avaliacoes.ListaPerfilComportalmental(),
            //               ConhecimentoMerchandisings = _avaliacoes.ListaConhecimentoMerchandising()

            //           };
        }

        private PesquisaAdminViewModel ModelToViewModel(IList<Candidato> candidatos)
        {
            var candidatosPesquisados = new List<CandidatoPesquisado>();

            foreach (var candidato in candidatos)
            {
                candidatosPesquisados.Add(new CandidatoPesquisado
                                              {
                                                  IdCandidato = candidato.Id,
                                                  Imagem = candidato.FotoRosto,
                                                  Nome = candidato.Nome,
                                                  Altura = candidato.Altura,
                                                  CorDosOlhos = candidato.CorDosOlhos.ToString(),
                                                  Escolaridade = candidato.Escolaridade.ToString(),
                                                  Idade = (candidato.DataNascimento != null) ? CalculaIdade((DateTime)candidato.DataNascimento) : "",
                                                  Peso = candidato.Peso,
                                                  Situacao = (candidato.SituacaoCandidato != null) ? candidato.SituacaoCandidato.Nome : "",
                                                  Foto = (string.IsNullOrEmpty(candidato.FotoRosto))
                                                        ? (string.IsNullOrEmpty(candidato.FotoCorpoFrente)) ?
                                                                "" : candidato.FotoCorpoFrente : candidato.FotoRosto
                                              }
                    );
            }

            return new PesquisaAdminViewModel
                        {
                            CandidatoPesquisados = candidatosPesquisados,
                            Funcaos = _funcao.ListaFuncoes(),
                            Segmentos = _segmento.ListaSegmentos()
                        }
                ;
        }

        private List<VagasPesquisadasViewModel> ModelToViewModel(IList<Vaga> vagas)
        {
            var vagasPesquisadas = new List<VagasPesquisadasViewModel>();

            foreach (var vaga in vagas)
            {
                vagasPesquisadas.Add(new VagasPesquisadasViewModel
                                                   {
                                                       Id = vaga.Id.ToString(),
                                                       Cliente = vaga.Cliente.Nome,
                                                       TituloVaga = vaga.Titulo,
                                                       TotalVagas = vaga.TotalVagasDisponiveis,
                                                       TotalVagasNaoPreenchidas = vaga.TotalVagasDisponiveis - vaga.TotalVagasPreenchidas
                                                   });

            }

            return vagasPesquisadas;
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
                Idade = (candidato.DataNascimento != null) ? CalculaIdade((DateTime)candidato.DataNascimento) : "",
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
                Funcoes = (IList<Funcao>)candidato.Funcoes,
                OutrasFuncoes = candidato.OutrasFuncoes,
                Segmentos = (IList<Segmento>)candidato.Segmentos,
                OutrosSegmentos = candidato.OutrosSegmentos,
                ExperienciasProfissionais = (IList<ExperienciaProfissional>)candidato.ExperienciasProfissionais,
                DataAlteracao = candidato.DataAlteracao.ToString(),
                DataCadastro = candidato.DataCadastro.ToString(),
                ClienteList = _clientes.ListaClientes(),
                SituacaoCandidato = candidato.SituacaoCandidato,
                SituacaoCandidatoList = _situacaoCandidatos.ListaSituacaoCandidato(),
                HistoricoDeAvaliacao = ListaHistoricosDeAvaliacao(candidato.Id),
                Recrutamentos = ListaDeRecrutamento(candidato.Id),
                RecrutamentoAtual = _recrutamentos.GetRecrutamentoAtualDoCandidato(candidato),
                EntrevistaAgendada = _entrevistas.EntrevistaMarcadaParaCandidato(candidato.Id)
            };

            return cv;

        }

        private List<HistoricoDeAvaliacao> ListaHistoricosDeAvaliacao(int candidatoId)
        {
            var avaliacoes = _avaliacoes.ListaAvaliacoesPorCandidato(candidatoId);

            var historicos = new List<HistoricoDeAvaliacao>();
            foreach (var avaliacao in avaliacoes)
            {
                var recrutamento = _recrutamentos.GetRecrutamento(avaliacao.Vaga, avaliacao.Candidato);


                var type = typeof(StatusCandidato);
                var memInfo = type.GetMember(recrutamento.StatusCandidato.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                var status = ((DisplayAttribute)attributes[0]).Name;

                var historico = new HistoricoDeAvaliacao
                                    {
                                        Cliente = avaliacao.Vaga.Cliente.Nome,
                                        DataAvaliacao = string.Concat(avaliacao.DataEntrevista.ToString("dd/MM/yyyy"), " - ", avaliacao.HorarioEntrevista),
                                        IdAvaliacao = avaliacao.Id,
                                        Situacao = avaliacao.SituacaoCandidato,
                                        Status = status,
                                        TituloVaga = avaliacao.Vaga.Titulo
                                    };

                historicos.Add(historico);
            }

            return historicos;
        }
        private List<HistoricoDeRecrutamento> ListaDeRecrutamento(int candidatoId)
        {
            var recrutamentos = _recrutamentos.ListaRecrutamentosPorCandidato(candidatoId);

            var historicos = new List<HistoricoDeRecrutamento>();
            foreach (var recrutamento in recrutamentos)
            {
                
                var type = typeof(StatusCandidato);
                var memInfo = type.GetMember(recrutamento.StatusCandidato.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof (DisplayAttribute), false);
                var status = ((DisplayAttribute) attributes[0]).Name;
                //var al = (DisplayAttribute.GetCustomAttribute());
                 
                var historico = new HistoricoDeRecrutamento()
                {

                    Cliente = recrutamento.Vaga.Cliente.Nome,
                    IdRecrutamento = recrutamento.Id,
                    Situacao = recrutamento.Candidato.SituacaoCandidato,
                    Status = status,
                    TituloVaga = recrutamento.Vaga.Titulo
                };

                historicos.Add(historico);
            }

            return historicos;
        }

        private string FormataStatus(StatusCandidato statusCandidato)
        {
            switch (statusCandidato)
            {
                case StatusCandidato.ProcessoDeSelecao:
                    return "Em processo de seleção";
                case StatusCandidato.StandyBy:
                    return "Stand-by";
            }

            return statusCandidato.ToString();
        }

        private Vaga viewModelToModel(VagaViewModel vagaViewModel)
        {
            var vaga = new Vaga();

            if (vagaViewModel.Id != 0)
            {
                vaga = _vagas.PesquisarVaga(vagaViewModel.Id);
            }

            vaga.Titulo = vagaViewModel.TituloVaga;
            vaga.Cliente = vagaViewModel.Cliente;
            vaga.Descricao = vagaViewModel.DescricaoVaga;
            vaga.TotalVagasDisponiveis = vagaViewModel.TotalVagasDisponivel;

            return vaga;
        }

        private Usuario viewModelToModel(UsuarioViewModel usuarioViewModel)
        {
            var usuario = new Usuario
            {
                Nome = usuarioViewModel.Nome,
                Rg = usuarioViewModel.Rg,
                Cpf = usuarioViewModel.Cpf,
                Email = usuarioViewModel.Email,
                Senha = usuarioViewModel.Senha,
                Telefone = usuarioViewModel.Telefone,
                Perfil = usuarioViewModel.Perfil
            };

            return usuario;
        }

        private Cliente viewModelToModel(ClienteViewModel clienteViewModel)
        {
            var cliente = new Cliente
                              {
                                  Nome = clienteViewModel.Nome,
                                  Cnpj = clienteViewModel.Cnpj,
                                  Rg = clienteViewModel.Rg,
                                  Cpf = clienteViewModel.Cpf,
                                  ContatoNome = clienteViewModel.ContatoResponsavel,
                                  Email = clienteViewModel.Email,
                                  Senha = clienteViewModel.Senha,
                                  Telefone = clienteViewModel.Telefone
                              };

            return cliente;

        }

        private Avaliacao ViewModelToModel(FormCollection formCollection)
        {
            var avaliacaoId = formCollection["IdAvaliacao"];

            var avaliacao = new Avaliacao();

            if (!string.IsNullOrEmpty(avaliacaoId))
            {
                avaliacao = _avaliacoes.GetAvaliacao(Convert.ToInt32(avaliacaoId));
            }

            avaliacao.OutroPerfilComportalmental = formCollection["OutroPerfilComportalmental"];
            avaliacao.PerfilPadrao = (PerfilPadrao)Enum.Parse(typeof(PerfilPadrao), formCollection["PerfilPadrao"]);
            avaliacao.AvaliacaoConhecimentoMerchandising = (AvaliacaoConhecimentoMerchandising)Enum.Parse(typeof(AvaliacaoConhecimentoMerchandising), formCollection["AvaliacaoConhecimentoMerchandising"]);
            avaliacao.AnaliseFinal = formCollection["AnaliseFinal"];

            if (formCollection["PerfilComportamental"] != null)
            {
                var perfisCompMarcados = formCollection["PerfilComportamental"].Split(',');

                foreach (var perfilCMarcado in perfisCompMarcados)
                {
                    var perfilComp = _avaliacoes.GetPerfilComp(Convert.ToInt32(perfilCMarcado));
                    avaliacao.IncluirPerfilComportamental(perfilComp);
                }
            }


            if (formCollection["PerfilProfissional"] != null)
            {
                var perfisProfMarcados = formCollection["PerfilProfissional"].Split(',');

                foreach (var perfilPMarcado in perfisProfMarcados)
                {
                    var perfilProf = _avaliacoes.GetPerfilProf(Convert.ToInt32(perfilPMarcado));
                    avaliacao.IncluirPerfilProfissional(perfilProf);
                }
            }

            if (formCollection["ConhecimentoMerchandisings"] != null)
            {
                var conhecimentoMerchMarcados = formCollection["ConhecimentoMerchandisings"].Split(',');

                foreach (var conhecimentoMarcado in conhecimentoMerchMarcados)
                {
                    var conhecimento = _avaliacoes.GetConhecimentoMerchan(Convert.ToInt32(conhecimentoMarcado));
                    avaliacao.IncluirConhecimentoMerchandising(conhecimento);
                }
            }

            return avaliacao;
        }

        private Recrutamento ViewModelToModelSelecao(AgendamentoEntrevista agendamentoEntrevista, Vaga vaga)
        {

            var candidato = _candidatos.GetCandidato(Convert.ToInt32(agendamentoEntrevista.IdCandidato));

            var recrutamento = _recrutamentos.GetRecrutamento(vaga, candidato) ?? new Recrutamento();

            recrutamento.Candidato = candidato;
            recrutamento.StatusCandidato = agendamentoEntrevista.StatusCandidato;
            recrutamento.DataInicio = Convert.ToDateTime(agendamentoEntrevista.DataEntrevista);
            recrutamento.HorarioInicio = agendamentoEntrevista.HorarioEntrevista;
            recrutamento.Endereco = agendamentoEntrevista.Endereco;
            recrutamento.Sms = agendamentoEntrevista.Sms.ToString();
            recrutamento.Email = agendamentoEntrevista.Email.ToString();
            recrutamento.Vaga = vaga;

            return recrutamento;
        }

        private Entrevista ViewModelToModel(AgendamentoEntrevista agendamentoEntrevista)
        {
            var entrevista = new Entrevista
                                 {
                                     Candidato = _candidatos.GetCandidato(Convert.ToInt32(agendamentoEntrevista.IdCandidato)),
                                     DataEntrevista = Convert.ToDateTime(agendamentoEntrevista.DataEntrevista),
                                     HorarioEntrevista = agendamentoEntrevista.HorarioEntrevista,
                                     Sms = agendamentoEntrevista.Sms.ToString(),
                                     Email = agendamentoEntrevista.Email.ToString(),
                                     Vaga = _vagas.PesquisarVaga(Convert.ToInt32(agendamentoEntrevista.VagaAgendamento))

                                 };

            return entrevista;
        }

        private string ArrumarCampoData(DateTime? date)
        {
            if (date.ToString() == "01/01/0001 00:00:00" || date == null)
            {
                return "";
            }

            //O toString do candidato não permite essa formatação
            return Convert.ToDateTime(date).ToString("dd/MM/yyyy");

        }
        private string CalculaIdade(DateTime dataNascimento)
        {

            var idade = DateTime.Today.Year - dataNascimento.Year;

            if (DateTime.Today.Month < dataNascimento.Month)
            {
                idade = idade - 1;
            }
            else if (DateTime.Today.Month == dataNascimento.Month)
            {
                if ( DateTime.Today.Day< dataNascimento.Day)
                {
                    idade = idade - 1;
                }
            }

            idade = (idade < 0) ? 0 : idade;

            return idade.ToString();
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

            return "";

        }

        public ActionResult SelecionaVagasporCliente(int idCliente)
        {
            var vagas = _vagas.ListaVagasPorCliente(idCliente);
            return Json(vagas
                .Select(c => new { c.Id, c.Titulo }),
            JsonRequestBehavior.AllowGet);
        }

        public String AlteraSituacaoCandidato(int situacaoId, int candidatoId)
        {
            var situacao = _situacaoCandidatos.GetSituacao(situacaoId);
            var candidato = _candidatos.GetCandidato(candidatoId);
            candidato.SituacaoCandidato = situacao;
            _candidatos.SaveCandidato(candidato);
            return candidato.SituacaoCandidato.Nome;
        }

        public String AgendarEntrevista(AgendamentoEntrevista agendamentoEntrevista)
        {
            var entrevista = ViewModelToModel(agendamentoEntrevista);

            _entrevistas.SaveEntrevista(entrevista);

            return entrevista.DataEntrevista.ToShortDateString() + "  " + entrevista.HorarioEntrevista;
        }

        public String RealizaSelecao(AgendamentoEntrevista agendamentoEntrevista)
        {
            var vaga = _vagas.PesquisarVaga(Convert.ToInt32(agendamentoEntrevista.VagaAgendamento));

            if (vaga.TotalVagasDisponiveis == vaga.TotalVagasPreenchidas)
            {
                return "VagasPreenchidas";
            }

            var dadosDaSelecao = ViewModelToModelSelecao(agendamentoEntrevista, vaga);

            if (dadosDaSelecao.StatusCandidato == StatusCandidato.Aprovado)
            {
                dadosDaSelecao.Vaga.TotalVagasPreenchidas = dadosDaSelecao.Vaga.TotalVagasPreenchidas + 1;
            }

            _recrutamentos.SaveRecrutamento(dadosDaSelecao);

            return "CadastradoComSucesso";
        }

        public class AgendamentoEntrevista
        {
            public string IdCandidato { get; set; }
            public string ClienteAgendamento { get; set; }
            public string VagaAgendamento { get; set; }
            public string DataEntrevista { get; set; }
            public string HorarioEntrevista { get; set; }
            public bool Sms { get; set; }
            public bool Email { get; set; }

            public string Endereco { get; set; }
            public StatusCandidato StatusCandidato { get; set; }
        }

    }
}
