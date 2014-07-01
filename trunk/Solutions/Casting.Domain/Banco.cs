using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace Casting.Domain
{
    public class Banco : Entity
    {
        public virtual string Nome { get; set; }

    }
}
