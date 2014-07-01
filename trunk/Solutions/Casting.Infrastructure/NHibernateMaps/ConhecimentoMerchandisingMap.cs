using Casting.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;


namespace Casting.Infrastructure.NHibernateMaps
{
    public class ConhecimentoMerchandisingMap : IAutoMappingOverride<ConhecimentoMerchandising> 
    {
        public void Override(AutoMapping<ConhecimentoMerchandising> mapping)
        {
            mapping.HasManyToMany(x => x.Avaliacaos)
                .Inverse()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}
