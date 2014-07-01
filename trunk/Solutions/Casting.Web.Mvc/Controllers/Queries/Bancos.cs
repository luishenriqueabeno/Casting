using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Casting.Domain;
using SharpArch.NHibernate;

namespace Casting.Web.Mvc.Controllers.Queries
{
    public class Bancos : NHibernateQuery, IBancos
    {
        public IList<Banco> ListaBancos()
        {
            var funcoes = Session.QueryOver<Banco>().List();
            return funcoes;
        }

        public Banco FindByNome(string nome)
        {
            var banco = Session.QueryOver<Banco>()
                .Where(x => x.Nome == nome)
                .SingleOrDefault();

            return banco;
        }
    }
}