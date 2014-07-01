using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using Casting.Domain;
using Casting.Domain.Contracts.Tasks;
using Casting.Infrastructure;

namespace Casting.Tasks
{
    public class ServicoDeAutenticacao : IServicoDeAutenticacao
    {
        private readonly IUsuarios _usuarios;
        private IHttpContextProvider _context;

        const bool manterConectado = true;

        public ServicoDeAutenticacao(IUsuarios usuarios, IHttpContextProvider context)
        {
            _usuarios = usuarios;
            _context = context;
        }

        public bool AutenticarPorId(int id)
        {
            Usuario usuario = _usuarios.ObterPorId(id);

            if (usuario != null)
            {
                HttpContext.Current.Session["UsuarioCastingPerfil"] = usuario.Perfil;
                HttpContext.Current.Session["UsuarioCastingAutenticado"] = usuario.Id;

                var ticket = new FormsAuthenticationTicket(Convert.ToInt32(usuario.Id), usuario.Nome, DateTime.Now, DateTime.Now.AddMinutes(10), manterConectado, "Nextvision", FormsAuthentication.FormsCookiePath);
                var hashTicket = FormsAuthentication.Encrypt(ticket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashTicket);
                if (ticket.IsPersistent) authCookie.Expires = ticket.Expiration;

                _context.GetCurrentHttpContext().Response.Cookies.Add(authCookie);
                _context.GetCurrentHttpContext().Session["UsuarioCastingPerfil"] = usuario.Perfil;
                _context.GetCurrentHttpContext().Session["UsuarioCastingAutenticado"] = usuario.Id;

                return true;

            }

            return false;
        }
        
        public bool Autenticar(string login, string senha)
        {
            Usuario usuario = _usuarios.ObterPorLogin(login, senha);

            if (usuario != null)
            {
                HttpContext.Current.Session["UsuarioCastingPerfil"] = usuario.Perfil;
                HttpContext.Current.Session["UsuarioCastingAutenticado"] = usuario.Id;

                var ticket = new FormsAuthenticationTicket(Convert.ToInt32(usuario.Id), usuario.Nome, DateTime.Now, DateTime.Now.AddMinutes(10), manterConectado, "Nextvision", FormsAuthentication.FormsCookiePath);
                var hashTicket = FormsAuthentication.Encrypt(ticket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashTicket);
                if (ticket.IsPersistent) authCookie.Expires = ticket.Expiration;

                _context.GetCurrentHttpContext().Response.Cookies.Add(authCookie);
                _context.GetCurrentHttpContext().Session["UsuarioCastingPerfil"] = usuario.Perfil;
                _context.GetCurrentHttpContext().Session["UsuarioCastingAutenticado"] = usuario.Id;

                return true;

            }
            
            return false;
        }

        public Usuario UsuarioAutenticado()
        {
            return _usuarios.ObterPorId(CodigoDeUsuarioAutenticado());
        }

        public void Sair()
        {
            FormsAuthentication.SignOut();
            _context.GetCurrentHttpContext().Session.Abandon();

            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            _context.GetCurrentHttpContext().Response.Cookies.Add(cookie1);
            _context.GetCurrentHttpContext().Session["UsuarioCastingAutenticado"] = 0;
            _context.GetCurrentHttpContext().Session["UsuarioCastingPerfil"] = "";

            FormsAuthentication.RedirectToLoginPage();
        }

        public Usuario RecuperarSenha(string email)
        {
            throw new NotImplementedException();
        }

        public int CodigoDeUsuarioAutenticado()
        {
            var codigoEmSessao = _context.GetCurrentHttpContext().Session["UsuarioCastingAutenticado"];

            return Convert.ToInt32(codigoEmSessao);
        }
    }

    public class HttpContextProvider : IHttpContextProvider
    {
        public HttpContextBase GetCurrentHttpContext()
        {
            return new HttpContextWrapper(HttpContext.Current);
        }
    }
}
