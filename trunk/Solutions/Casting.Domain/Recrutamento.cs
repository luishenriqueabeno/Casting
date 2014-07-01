using System;
using Casting.Domain.enums;
using SharpArch.Domain.DomainModel;

namespace Casting.Domain
{
    public class Recrutamento : Entity
    {
        public virtual StatusCandidato StatusCandidato { get; set; }
        public virtual Vaga Vaga { get; set; }
        public virtual Candidato Candidato { get; set; }
        public virtual DateTime? DataInicio { get; set; }
        public virtual string HorarioInicio { get; set; }
        public virtual string Endereco { get; set; }
        public virtual string Email { get; set; }
        public virtual string Sms { get; set; }
        public virtual string MotivoDesligamento { get; set; }

    }
}
