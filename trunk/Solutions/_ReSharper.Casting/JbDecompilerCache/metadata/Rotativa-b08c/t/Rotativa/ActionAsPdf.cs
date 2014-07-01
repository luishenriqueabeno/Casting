// Type: Rotativa.ActionAsPdf
// Assembly: Rotativa, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Projetos\Casting\Packages\Rotativa.1.3.2\lib\net40\Rotativa.dll

using System.Web.Mvc;
using System.Web.Routing;

namespace Rotativa
{
    public class ActionAsPdf : AsPdfResultBase
    {
        public ActionAsPdf(string action);
        public ActionAsPdf(string action, RouteValueDictionary routeValues);
        public ActionAsPdf(string action, object routeValues);
        protected override string GetUrl(ControllerContext context);
    }
}
