using Contellect.ContactApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contellect.ContactApp.Controllers
{
    public class LogInController : Controller
    {
        const string SessionName = "userCode";
        public IActionResult logInPage()
        {
            if (HttpContext.Session.GetInt32(SessionName).HasValue)
                HttpContext.Session.Remove(SessionName);
            return View();
        }

        [HttpPost]
        public IActionResult logInPage(User user)
        {
            if (user.userName == "user1" && user.userPassword == "user1")
            {
                HttpContext.Session.SetInt32(SessionName, 1);
                return RedirectToAction("Index", "Contact");
            }
            else if (user.userName == "user2" && user.userPassword == "user2")
            {
                HttpContext.Session.SetInt32(SessionName, 2);
                return RedirectToAction("Index", "Contact");
            }
            else
                TempData["msg"] = "Invalid User Name Or PassWord";
            return View();
        }

    }
}
