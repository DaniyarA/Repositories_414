using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Util
{
    public class ImageResult : ActionResult //ActionResult представляет собой абстрактный класс, 
                                            //в котором определен один метод ExecuteResult, переопределяемый в классах-наследниках:
    {
        private string path;
        public ImageResult(string path)
        {
            this.path = path;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Write("<div style='width:100%;text-align:center;'>" +
                "<img style='max-width:25%;' src='" + path + "' /></div>");
        }
    }
}