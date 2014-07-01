using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Casting.Web.Mvc.Controllers.ViewModels
{
    public class EmailViewModel
    {

        public void EnviarEmail()
        {
            WebMail.SmtpServer = "smtp.gmail.com";

            WebMail.Send("tricolor.paulinho@gmail.com","Teste de Envio","Teeeeeeeeeeeesteeeeeee",
                "tricolor.paulinho@gmail.com");
            
        }
    }
}