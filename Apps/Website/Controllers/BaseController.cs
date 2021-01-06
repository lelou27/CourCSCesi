using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Controllers
{
    public abstract class BaseController : Controller
    {
        public const string FlashMessageKey = "FlashMessage"; 
        protected void Success(string message)
        {
            TempData[FlashMessageKey] = message;            
        }
    }
}
