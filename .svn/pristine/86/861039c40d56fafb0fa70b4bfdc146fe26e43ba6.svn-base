using Casting.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;


namespace Casting.Infrastructure.NHibernateMaps
{
    public class PerfilComportalmentalMap : IAutoMappingOverride<PerfilComportalmental> 
    {
        public void Override(AutoMapping<PerfilComportalmental> mapping)
        {
            mapping.HasManyToMany(x => x.Avaliacaos)
                .Inverse()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}
