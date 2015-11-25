using Rongzi.Entity;
using Rongzi.Infrastructure.Performence;
using Rongzi.Infrastructure.Queue;
using Rongzi.Infrastructure.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Rongzi.Mvc.Infrastructure.App_Utility
{
    public class LogActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                #region 计时器
                Stopwatch stopwatch = new Stopwatch();
                CallContext.SetData("stopwatch", stopwatch);
                stopwatch.Start();
                #endregion
                Log(filterContext.RouteData.Values["action"].ToString(), filterContext.RouteData.Values["controller"].ToString(), filterContext.HttpContext.Request.InputStream);
                base.OnActionExecuting(filterContext);
            }
            catch (Exception e)
            {

            }

        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            try
            {
                string msg = JsonHelper.GetJsonFromObject(filterContext.Result);
                string reqmsg = JsonHelper.GetJsonFromObject(filterContext.RequestContext);
                #region 性能统计
                Stopwatch stopwatch = CallContext.GetData("stopwatch") as Stopwatch;
                if (stopwatch != null)
                {
                    stopwatch.Stop();
                    long interval = stopwatch.ElapsedMilliseconds;
                    msg += "-" + filterContext.RouteData.Values["controller"].ToString() + ":" + filterContext.RouteData.Values["action"].ToString() + "执行成功，耗时：" + interval + "毫秒";
                }

                #endregion
                Rongzi.Infrastructure.Log.Log.Info(filterContext.RouteData.Values["action"].ToString(), filterContext.RouteData.Values["controller"].ToString(), msg);
                base.OnResultExecuted(filterContext);
            }
            catch (Exception e)
            {
            }
        }

        private void Log(string actionName, string controllerName, Stream req)
        {
            try
            {
                req.Seek(0, System.IO.SeekOrigin.Begin);
                var json = new StreamReader(req).ReadToEnd();
                Rongzi.Infrastructure.Log.Log.Info(actionName, controllerName, json);
            }
            catch
            {
                // ignored
            }
        }
    }
}
