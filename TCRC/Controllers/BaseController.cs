using System;
using System.Web.Mvc;
using TCRC.Helpers;

namespace TCRC.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Begin execute
        /// </summary>
        /// <param name="requestContext">The request context</param>
        /// <param name="callback">The callback</param>
        /// <param name="state">The state</param>
        protected override IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state)
        {
            string activeAction = requestContext.RouteData.Values["action"].ToString();
            string activeController = requestContext.RouteData.Values["controller"].ToString();
            NavigationMenuHelper.SetActiveNavigationMenuItem(activeAction, activeController);

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}