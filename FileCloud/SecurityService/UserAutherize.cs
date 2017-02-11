using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileCloud.SecurityService
{
   

    public class UserAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["LogedUser"] == null)
            {
               
                filterContext.Result = new RedirectResult("~/Login/Index");
                
                filterContext.Result.ExecuteResult(filterContext);
            }

              base.OnActionExecuting(filterContext);
        }        
                      
    }
}