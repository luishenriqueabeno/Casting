using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace Casting.Domain
{
    public class ExperienciaProfissional  : Entity
    {
        public virtual string Nome { get; set; }
        public virtual string Produto { get; set; }
        public virtual string Empresa { get; set; }
        public virtual string TipoAcao { get; set; }
        public virtual string Periodo1 { get; set; }
        public virtual string Periodo2 { get; set; }

        public virtual IEnumerable<Candidato> Candidatos { get { return _candidatos; } }
        private IList<Candidato> _candidatos;
    }
}

