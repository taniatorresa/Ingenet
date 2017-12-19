using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web_application.Models
{
    public class VMPregunta
    {

        public IEnumerable<SelectListItem> Temas { get; set; }
        public IEnumerable<String> SelectedTemas { get; set; }

    }
}