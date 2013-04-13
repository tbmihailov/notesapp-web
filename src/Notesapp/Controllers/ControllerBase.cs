using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Notesapp.Controllers
{
    public class ControllerBase : Controller
    {
        public string CurrentUserName
        {
            get
            {
                return this.User.Identity.IsAuthenticated ? this.User.Identity.Name : string.Empty;
            }
        }
    }
}