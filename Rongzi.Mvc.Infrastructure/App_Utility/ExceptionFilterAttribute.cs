using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Rongzi.Mvc.Infrastructure.App_Utility
{
    public class ExceptionFilterAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            Rongzi.Infrastructure.Log.Log.Error(
                filterContext.RouteData.Values["action"].ToString(),
                filterContext.RouteData.Values["controller"].ToString(),
                filterContext.Exception);
        }
    }
}
