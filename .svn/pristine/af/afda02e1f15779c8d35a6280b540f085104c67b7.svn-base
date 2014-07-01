using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Casting.Domain;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;

namespace Casting.Infrastructure.NHibernateMaps
{
    public class SegmentoMap : IAutoMappingOverride<Segmento>
    {
        public void Override(AutoMapping<Segmento> mapping)
        {
            mapping.HasManyToMany(x => x.Candidatos)
                .Inverse()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}
