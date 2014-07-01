using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Casting.Domain;
using SharpArch.NHibernate;

namespace Casting.Web.Mvc.Controllers.Queries
{
    public class ExperienciaProfissionals : NHibernateQuery, IExperienciaProfissionals
    {
        public ExperienciaProfissional GetExperienciaProfissional(int id)
        {
            return Session.QueryOver<ExperienciaProfissional>()
                .Where(x => x.Id == id)
                .SingleOrDefault();
        }
    }
}