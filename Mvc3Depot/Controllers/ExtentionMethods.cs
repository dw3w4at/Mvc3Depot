using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc3Depot.Controllers
{
    public static class ExtentionMethods
    {
        private const string CartSessionKey = "CartId";

        public static Guid GetCartId(this Controller controller)
        {
            HttpContextBase context = controller.HttpContext;
            Guid result;
            if (context.Session[CartSessionKey] == null)
            {
                context.Session[CartSessionKey] = Guid.NewGuid();
            }
            Guid.TryParse(context.Session[CartSessionKey].ToString(), out result);
            return result;
        }

        public static void DeleteCartId(this Controller controller)
        {
            controller.HttpContext.Session[CartSessionKey] = null;
        }
    }
}