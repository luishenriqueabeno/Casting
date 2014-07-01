using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Casting.Web.Mvc.Controllers.ViewModels
{
    public class FullCalendarViewModel
    {
        //public FullCalendarCabecalho header { get; set; }
        //public List<FullCalendarEventos> events { get; set; }

        public string id { get; set; }
        public string title { get; set; }
        public string date { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string url { get; set; }
        public bool allDay { get; set; }
        public string minTime { get; set; }
        public string maxTime { get; set; }
         
    }

    //public class FullCalendarCabecalho
    //{
    //    public string left { get; set; }
    //    public string center { get; set; }
    //    public string right { get; set; }
    //}

    //public class FullCalendarEventos
    //{
    //    public string title { get; set; }
    //    public string start { get; set; }
    //    public string allDay { get; set; }
    //}
}