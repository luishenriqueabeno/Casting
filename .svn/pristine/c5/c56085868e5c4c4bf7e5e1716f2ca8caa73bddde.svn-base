using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;
using Casting.Domain;

namespace Casting.Infrastructure.NHibernateMaps
{
    class UsuarioMap : IAutoMappingOverride<Usuario>
    {

        public void Override(AutoMapping<Usuario> mapping)
        {
            mapping.Id(x => x.Id, "Id");
        }
    }
}
