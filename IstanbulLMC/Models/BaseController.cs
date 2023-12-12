using Microsoft.AspNetCore.Mvc;

namespace IstanbulLMC.Models
{
    public class BaseController : Controller
    {
        protected new ViewResult View(object model)
        {
            // Set the custom view path
            var viewPath = $"~/Views/Admin/{this.RouteData.Values["controller"]}/{this.RouteData.Values["action"]}.cshtml";
            return base.View(viewPath, model);
        }
    }
}
