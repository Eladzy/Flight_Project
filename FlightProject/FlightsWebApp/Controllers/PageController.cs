using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightProjectWebServices.Controllers
{
    public class PageController : Controller
    {
        // GET: Page
        public ActionResult DeparturesPage()
        {
            return new FilePathResult("~/Views/Page/departurespage.html", "text/html");
        }
       
    }
}