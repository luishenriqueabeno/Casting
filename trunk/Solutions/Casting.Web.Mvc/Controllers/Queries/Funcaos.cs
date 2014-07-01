using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Casting.Domain;
using SharpArch.NHibernate;

namespace Casting.Web.Mvc.Controllers.Queries
{
    public class Funcaos : NHibernateQuery, IFuncaos
    {
        public IList<Funcao> ListaFuncoes()
        {
            var funcoes = Session.QueryOver<Funcao>().List();
            return funcoes;
        }

        public Funcao FindByDescr(string descricao)
        {
            var funcao = Session.QueryOver<Funcao>()
                .Where(x => x.Nome == descricao)
                .SingleOrDefault();

            return funcao;
        }
    }
}