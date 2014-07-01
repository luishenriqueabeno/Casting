using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace Casting.Domain
{
    public class Funcao : Entity
    {
        public virtual string Nome { get; set; }

        public virtual IList<Candidato> Candidatos { get { return _candidatos; } }
        private IList<Candidato> _candidatos;
    }
}
