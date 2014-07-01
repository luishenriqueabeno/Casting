using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Casting.Domain;
using SharpArch.NHibernate;

namespace Casting.Web.Mvc.Controllers.Queries
{
    public class Segmentos : NHibernateQuery, ISegmentos
    {
        public IList<Segmento> ListaSegmentos()
        {
            var user = Session.QueryOver<Segmento>().List();

            return user;
        }

        public Segmento FindByDescr(string descricao)
        {
            var seg = Session.QueryOver<Segmento>()
                .Where(x => x.Nome == descricao)
                .SingleOrDefault();

            return seg;
        }
    }
}