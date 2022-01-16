using Contellect.ContactApp.Core.IServices;
using Contellect.ContactApp.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contellect.ContactApp.Reposatory.Paging;
using Microsoft.AspNetCore.Http;

namespace Contellect.ContactApp.Controllers
{
    public class ContactController : Controller
    {
        IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IActionResult> Index(int? pageStatus, int pageNumber = 1)
        {
            var userCode = HttpContext.Session.GetInt32("userCode");
            _contactService.RemoveStatusUpdatedRow((int)userCode);

            Contacts contact = new Contacts();
            ViewBag.results = await PaginatedList<Contacts>.CreateAsync(_contactService.GetAllContatct(), pageNumber, 5);

            if (pageStatus > 0)
                contact = _contactService.GetByID((int)pageStatus);

            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string SearchString, int pageNumber = 1)
        {
            var userCode = HttpContext.Session.GetInt32("userCode");
            _contactService.RemoveStatusUpdatedRow((int)userCode);

            Contacts contact = new Contacts();
            contact.ContactName = SearchString;

            if (String.IsNullOrEmpty(SearchString))
                ViewBag.results = await PaginatedList<Contacts>.CreateAsync(_contactService.GetAllContatct(), pageNumber, 5);
            else
                ViewBag.results = await PaginatedList<Contacts>.CreateAsync(_contactService.FilterContatct(contact), pageNumber, 5);

            return View(contact);
        }

        [HttpGet]
        public IActionResult GetByID(int id)
        {
            var obj = _contactService.GetByID(id);
            //edit
            var userCode = HttpContext.Session.GetInt32("userCode");

            obj.UserUpdatedID = (int)userCode;
            _contactService.SaveContatct(obj);

            return Json(new { data = obj });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveContact([Bind] Contacts contact)
        {
            if (ModelState.IsValid)
            {
                contact.UserUpdatedID = 0;
                if (_contactService.SaveContatct(contact))
                    TempData["msg"] = "Save Is Done Successfully";
                else
                    TempData["msg"] = "Save Is Failed";
            }
            else
                TempData["msg"] = "Save Is Failed";

            TempData["rowId"] = contact.ContactID;
            return RedirectToAction("Index");
        }

        public IActionResult DeleteContact(int id)
        {
            if(_contactService.DeleteContatct(id))
                TempData["msg"] = "Delete Is Done Successfully";
            else
                TempData["msg"] = "Delete Is Failed";

            return RedirectToAction("Index");
        }
    }
}
