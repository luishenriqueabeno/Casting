using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Casting.Web.Mvc.Controllers.ViewModels
{
    public class ChartViewModel
    {
        public Chart chart { get; set; }
        public IList<ChartCategories> data { get; set; }
    }

    public class Chart
    {
        public string caption { get; set; }
        public string showpercentageinlabel { get; set; }
        public string showlegend { get; set; }
    }

    public class ChartCategories
    {
        public string value { get; set; }
        public string label { get; set; }
        public string color { get; set; }
    }

}