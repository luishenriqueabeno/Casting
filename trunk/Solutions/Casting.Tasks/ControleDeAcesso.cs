using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Casting.Tasks
{
    public class ControleDeAcesso : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpContext = filterContext.HttpContext;

            //var perfilDoUsuario = HttpContext.Current.Session["PerfilDoUsuarioAutenticado"] as String;

            //if (perfilDoUsuario != null && (perfilDoUsuario.ToLower() != "promotor" && perfilDoUsuario.ToLower() != "coordenador"))
            //{
            //    httpContext.Response.Redirect("/Usuario/Logout");
            //}

            if (HttpContext.Current.Session["UsuarioCastingAutenticado"] == null)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
            }
        }
    }
}