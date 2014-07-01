using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace Casting.Domain
{
    public class ProjetosParticipados : Entity
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Titulo { get; set; }

        public virtual IList<ProjetosParticipados> Projetos
        {
            get { return _projetos; }
        }

        private IList<ProjetosParticipados> _projetos;
    }
}
