using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Perseus.Filters
{
    public class RenderTimeFilter : ActionFilterAttribute
    {
        readonly Stopwatch _sw = new Stopwatch();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _sw.Start();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _sw.Stop();
            filterContext.HttpContext.Response.Write(
                String.Format("<small>Controller: {0} | Action: {1} | Render time: {2} ms</small>",
                filterContext.RouteData.Values["controller"],
                filterContext.RouteData.Values["action"],
                _sw.ElapsedMilliseconds));
        }
        
    }
}