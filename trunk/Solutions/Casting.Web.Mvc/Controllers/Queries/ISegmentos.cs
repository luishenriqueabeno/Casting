using System.Collections.Generic;
using Casting.Domain;

namespace Casting.Web.Mvc.Controllers.Queries
{
    public interface ISegmentos
    {
        IList<Segmento> ListaSegmentos();
        Segmento FindByDescr(string descricao);
    }
}