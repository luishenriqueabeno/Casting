﻿using System.Collections.Generic;
using SharpArch.Domain.DomainModel;

namespace Casting.Domain
{
    public class ConhecimentoMerchandising : Entity
    {
        public virtual string Nome { get; set; }

        public virtual IList<Avaliacao> Avaliacaos { get { return _avaliacaos; } }
        private IList<Avaliacao> _avaliacaos;
    }
}
