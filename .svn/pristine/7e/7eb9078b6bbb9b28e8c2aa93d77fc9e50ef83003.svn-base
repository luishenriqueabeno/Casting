using Casting.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;


namespace Casting.Infrastructure.NHibernateMaps
{
    public class PerfilProfissionalMap : IAutoMappingOverride<PerfilProfissional> 
    {
        public void Override(AutoMapping<PerfilProfissional> mapping)
        {
            mapping.HasManyToMany(x => x.Avaliacaos)
                .Inverse()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}
