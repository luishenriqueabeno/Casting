using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Casting.Web.Mvc.Controllers.Queries;

namespace Casting.Web.Mvc.Controllers
{
    public class TesteController : Controller
    {
        //
        // GET: /Teste/

        private readonly IClientes _clientes;

        public TesteController(IClientes clientes)
        {
            _clientes = clientes;
        }

        public ActionResult Index()
        {
            ViewData["Clientes"] = _clientes.ListaClientes();
            return View();
        }

    }
}
