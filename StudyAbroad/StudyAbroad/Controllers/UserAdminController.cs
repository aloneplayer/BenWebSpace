using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace StudyAbroad.Controllers
{
    public class UserAdminController : Controller
    {
        /// <summary>
        /// Manages the user.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="searchType">Type of the search.</param>
        /// <param name="searchInput">The search input.</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public ActionResult ManageUser(string searchType, string searchInput)
        {
            List<SelectListItem> searchOptionList = new List<SelectListItem>() 
            {
                new SelectListItem() { Value = "UserName", Text = "UserName" },
                new SelectListItem() { Value = "Email", Text = "Email" }
            };

            ViewData["searchOptionList"] = new SelectList(searchOptionList, "Value", "Text", searchType ?? "UserName");
            ViewData["searchInput"] = searchInput ?? string.Empty;
            ViewData["UsersOnlineNow"] = Membership.GetNumberOfUsersOnline().ToString();
            ViewData["RegisteredUsers"] = Membership.GetAllUsers().Count.ToString();

            MembershipUserCollection viewData;

            if (String.IsNullOrEmpty(searchInput))
                viewData = Membership.GetAllUsers();
            else if (searchType == "Email")
                viewData = Membership.FindUsersByEmail(searchInput);
            else
                viewData = Membership.FindUsersByName(searchInput);

            ViewData["PageTitle"] = "Account Management";
            return View(viewData);
        }

        //[Service, HttpPostOnly]
        public ActionResult DeleteUser(string id)
        {
            Membership.DeleteUser(id);
            return View(new { id = id });
        }


        /// <summary>
        /// Manages the role.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="newRole">The new role.</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public ActionResult ManageRole()
        {
            ViewData["TotalRoles"] = Roles.GetAllRoles().Count();
            ViewData["PageTitle"] = "Role Management";
            return View();
        }

        [Authorize(Roles = "Admin")]
        [AcceptVerbs("POST")]
        public ActionResult CreateRole(string newRole)
        {
            Roles.CreateRole(newRole);
            return RedirectToAction("ManageRole");
        }

        [Authorize(Roles = "Admin")]
        //[Service, HttpPostOnly]
        public ActionResult DeleteRole(string id)
        {
            Roles.DeleteRole(id);
            return View(new { id = id });
        }

        /// <summary>
        /// Edits the user.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="approved">The approved.</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public ActionResult EditUser(string id)
        {
            ViewData["roles"] = (String[])Roles.GetAllRoles();
            MembershipUser membershipUser = Membership.GetUser(id);

            ViewData["PageTitle"] = "Edit " + id;
            return View(membershipUser);
        }

        [AcceptVerbs("POST")]
        public ActionResult EditUser(string id, bool approved)
        {
            //Is a list of all the user roles
            ArrayList removeRoleList = new ArrayList(Roles.GetAllRoles());

            //We are requesting the form variables directly from the form
            foreach (string key in Request.Form.Keys)
            {
                if (key.StartsWith("role."))
                {
                    String userRole = key.Substring(5, key.Length - 5);
                    removeRoleList.Remove(userRole);
                    if (!Roles.IsUserInRole(id, userRole))
                    {
                        Roles.AddUserToRole(id, userRole);
                    }
                }
            }

            foreach (string removeRole in removeRoleList)
                Roles.RemoveUserFromRole(id, removeRole);

            MembershipUser membershipUser = Membership.GetUser(id);
            membershipUser.IsApproved = approved;
            Membership.UpdateUser(membershipUser);

            TempData["SuccessMessage"] = "User Information has been updated";
            ViewData["roles"] = (String[])Roles.GetAllRoles();
            ViewData["PageTitle"] = "Edit " + id;
            return View(membershipUser);
        }
    }
}
