// Type: System.Web.Mvc.WebViewPage`1
// Assembly: System.Web.Mvc, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: c:\Program Files (x86)\Microsoft ASP.NET\ASP.NET MVC 3\Assemblies\System.Web.Mvc.dll

namespace System.Web.Mvc
{
    public abstract class WebViewPage<TModel> : WebViewPage
    {
        public new AjaxHelper<TModel> Ajax { get; set; }
        public new HtmlHelper<TModel> Html { get; set; }
        public new TModel Model { get; }
        public new ViewDataDictionary<TModel> ViewData { get; set; }
        public override void InitHelpers();
        protected override void SetViewData(ViewDataDictionary viewData);
    }
}
