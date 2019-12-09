using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChangarroUser.Filters
{
    public class AjaxChildActionOnlyAttribute : ActionMethodSelectorAttribute
    {
        /// <summary>
        /// Este método se utiliza para el filtro de AjaxChildActionOnly que permite usar las vistas parciales renderizadas con ajax.
        /// </summary>
        /// <param name="controllerContext">Es la información encapsulada del controlador sobre la solicitud HTTP.</param>
        /// <param name="methodInfo">Es la información del método accesible.</param>
        /// <returns></returns>
        public override bool IsValidForRequest(ControllerContext controllerContext, System.Reflection.MethodInfo methodInfo)
        {
            return controllerContext.RequestContext.HttpContext.Request.IsAjaxRequest() || controllerContext.IsChildAction;
        }
    }
}