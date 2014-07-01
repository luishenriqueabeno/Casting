using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Casting.Domain.enums;
using SharpArch.Domain.DomainModel;

namespace Casting.Domain
{
    public class Usuario : Entity
    {
        public virtual string Nome { get; set; }
        public virtual string Email { get; set; }
        public virtual string Cpf { get; set; }
        public virtual string Rg { get; set; }
        public virtual string Telefone { get; set; }
        public virtual string Senha { get; set; }
        public virtual Perfil Perfil { get; set; }

    }
}
