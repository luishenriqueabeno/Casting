using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;
using Casting.Domain;

namespace Casting.Infrastructure.NHibernateMaps
{
    public class AvaliacaoMap : IAutoMappingOverride<Avaliacao>
    {
        public void Override(AutoMapping<Avaliacao> mapping)
        {
            mapping.HasManyToMany(x => x.PerfilComportalmentals)
                .Cascade.SaveUpdate()
                .Access.CamelCaseField(Prefix.Underscore);

            mapping.HasManyToMany(x => x.PerfilProfissionals)
                .Cascade.SaveUpdate()
                .Access.CamelCaseField(Prefix.Underscore);

            mapping.HasManyToMany(x => x.ConhecimentoMerchandisings)
                .Cascade.SaveUpdate()
                .Access.CamelCaseField(Prefix.Underscore);

            mapping.Id(x => x.Id, "Id");
        }
    }
}
