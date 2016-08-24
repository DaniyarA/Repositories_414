using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookStore.Controllers
{
    public class MyController : IController
    {
        public void Execute(RequestContext requestContext)
        {
            string ip = requestContext.HttpContext.Request.UserHostAddress;
            string hostName = requestContext.HttpContext.Request.UserHostAddress;
            var response = requestContext.HttpContext.Response;
            string provider = requestContext.HttpContext.Request.UserAgent;
            response.Write("<h2> Ваш IP- адрес: " + ip + "</h2><br />" + "<h2>HostName = " + hostName + "</h2>" + "<h2>Agent = " + provider + "</h2>");
        }
    }
}