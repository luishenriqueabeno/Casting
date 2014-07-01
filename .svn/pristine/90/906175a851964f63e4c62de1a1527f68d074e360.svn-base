using Casting.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;

namespace Casting.Infrastructure.NHibernateMaps
{
    class FuncaoMap : IAutoMappingOverride<Funcao>
    {
        public void Override(AutoMapping<Funcao> mapping)
        {
            mapping.HasManyToMany(x => x.Candidatos)
                .Inverse()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}
