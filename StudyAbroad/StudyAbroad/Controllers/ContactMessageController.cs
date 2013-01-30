using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyAbroad.Models;

namespace StudyAbroad.Controllers
{
    public class ContactMessageController : Controller
    {
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ContactMessage message)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StudyAbroadDataContext dc = new StudyAbroadDataContext();

                    message.AddedByIP = Request.UserHostAddress;
                    message.AddedDate = DateTime.Now;

                    dc.ContactMessages.Add(message);

                    // save changes to database
                    dc.SaveChanges();
                }
                catch (Exception exp)
                {
                    ViewBag.ErrorMessage = exp.Message;
                    return View(message);
                }
                return PartialView("_Success", message);
            }
            else
            {
                return PartialView(message);
            }
        }
        //
        // GET: /Admin/
        [Authorize(Roles = "Admin")]
        public ActionResult Manage(int page)
        {
            StudyAbroadDataContext dc = new StudyAbroadDataContext();
            Pagination<ContactMessage> viewData = dc.ContactMessages.GetContactMessageForPagination(page);

            ViewBag.Title = "Manage Contact Message";

            return View(viewData);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            StudyAbroadDataContext dc = new StudyAbroadDataContext();
            ContactMessage message = dc.ContactMessages.GetMessage(id);

            // throw a 404 Not Found if the requested article is not in the database
            if (message == null)
                throw new HttpException(404, "The contact message could not be found.");

            dc.ContactMessages.Remove(message);
            dc.SaveChanges();

            return View(new { commentId = id });
        }
    }
}
