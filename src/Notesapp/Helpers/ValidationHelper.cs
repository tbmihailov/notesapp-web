using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class BootstrapHelpers
    {
        public static MvcHtmlString BootstrapValidationSummary(this HtmlHelper helper, bool excludeModelErrors = false, string validationMessage = "")
        {
            string retVal = "";
            if (helper.ViewData.ModelState.IsValid)
                return new MvcHtmlString("");

            retVal += "<div class='alert alert-error'><a class='close' data-dismiss='alert'>×</a><span>";
            if (!String.IsNullOrEmpty(validationMessage))
                retVal += helper.Encode(validationMessage);
            retVal += "</span>";
            retVal += "<div class='text'>";

            ICollection<string> keys;
            if (!excludeModelErrors)
            {
                keys = helper.ViewData.Keys;
            }
            else
            {
                keys = new List<string>();
                keys.Add(string.Empty);
            }


            foreach (var key in keys)
            {
                foreach (var err in helper.ViewData.ModelState[key].Errors)
                {
                    retVal += "<p>" + helper.Encode(err.ErrorMessage) + "</p>";
                }
            }
            retVal += "</div></div>";
            return new MvcHtmlString(retVal.ToString());
        }
    }
}