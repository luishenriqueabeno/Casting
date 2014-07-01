using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Casting.Domain;
using Casting.Domain.enums;

namespace Casting.Web.Mvc.Controllers.ViewModels
{
    public class UsuarioViewModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }

        public string Rg { get; set; }
        public string Telefone { get; set; }
        public Perfil Perfil { get; set; }
    }
}