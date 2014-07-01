using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;
using Casting.Domain;

namespace Casting.Infrastructure.NHibernateMaps
{
    public class CandidatoMap : IAutoMappingOverride<Candidato>
    {
        public void Override(AutoMapping<Candidato> mapping)
        {
            mapping.HasManyToMany(x => x.Segmentos)
                .Cascade.SaveUpdate()
                .Access.CamelCaseField(Prefix.Underscore);

            mapping.Id(x => x.Id, "Id");

            mapping.HasManyToMany(x => x.Funcoes)
                .Cascade.SaveUpdate()
                .Access.CamelCaseField(Prefix.Underscore);

            mapping.HasManyToMany(x => x.ExperienciasProfissionais)
                .Cascade.SaveUpdate()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}
