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
    class ExperienciaProfissionalMap : IAutoMappingOverride<ExperienciaProfissional>
    {
        public void Override(AutoMapping<ExperienciaProfissional> mapping)
        {
            mapping.HasManyToMany(x => x.Candidatos)
                .Inverse()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}
