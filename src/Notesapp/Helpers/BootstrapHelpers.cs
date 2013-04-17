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

            ICollection<string> keys = helper.ViewData.Keys;
            int errorsCnt = 0;
            foreach (var key in keys)
            {
                if ((!excludeModelErrors) || (excludeModelErrors && (key == string.Empty)))
                {
                    foreach (var err in helper.ViewData.ModelState[key].Errors)
                    {
                        retVal += "<p>" + helper.Encode(err.ErrorMessage) + "</p>";
                        errorsCnt++;
                    }
                }
            }
            retVal += "</div></div>";
            if (errorsCnt > 0)
            {
                return new MvcHtmlString(retVal.ToString());
            }
            else
            {
                return new MvcHtmlString("");
            }
        }
    }
}