using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
using System.Web.Security;
using MyBeerHouse.Models;

namespace MyBeerHouse.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    //[HandleError]
    public class UserController : Controller
    {
        /// <summary>
        /// Logouts this instance.
        /// </summary>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return this.Redirect(Request.UrlReferrer != null ? 
                   Request.UrlReferrer.ToString() : FormsAuthentication.DefaultUrl);
        }

        /// <summary>
        /// Logins the specified user name.
        /// </summary>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="persistent"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        public ActionResult Login(string userName, string password, bool persistent, string returnUrl)
        {
            if (returnUrl != null && returnUrl.IndexOf("/user/login", StringComparison.OrdinalIgnoreCase) >= 0)
                returnUrl = null;

            if (Membership.ValidateUser(userName, password))
            {
                FormsAuthentication.SetAuthCookie(userName, persistent);

                if (!String.IsNullOrEmpty(returnUrl))
                    return this.Redirect(returnUrl);
                else
                    return this.Redirect(FormsAuthentication.DefaultUrl);
            }
            else
            {
                TempData["ErrorMessage"] = "Login failed! Please make sure you are using the correct user name and password.";
            }
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View(new UserInformation());
        }

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="secretAnswer">The secret answer.</param>
        /// <returns></returns>
        [AcceptVerbs("POST")]
        public ActionResult ForgotPassword(string userName, string secretAnswer)
        {
            if (!String.IsNullOrEmpty(secretAnswer))
            {
                string resetPassword = Membership.Provider.ResetPassword(userName, secretAnswer);
                if (Membership.ValidateUser(userName, resetPassword))
                {
                    FormsAuthentication.SetAuthCookie(userName, false);
                    return RedirectToAction("ChangePassword", new { resetPassword = resetPassword });
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid Response";
                    return View();
                }
            }

            MembershipUser membershipUser = Membership.GetUser(userName, false);
            UserInformation userinformation = new UserInformation();
            if (membershipUser != null)
            {
                userinformation.SecretQuestion = membershipUser.PasswordQuestion;
                userinformation.UserName = userName;
            }
            else
            {
                TempData["ErrorMessage"] = "The user you have specified is invalid, please recheck your username and try again";
                userinformation.SecretQuestion = String.Empty;
            }

            return View(userinformation);
        }

        /// <summary>
        /// Registers the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <param name="email">The email.</param>
        /// <param name="secretQuestion">The secret question.</param>
        /// <param name="secretAnswer">The secret answer.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>

        public ActionResult Register()
        {
            string returnUrl = string.Empty;
            if(!String.IsNullOrEmpty(Request.QueryString["returnUrl"]))
            {
                returnUrl = Request.QueryString["returnUrl"];
            }
            else if(Request.UrlReferrer != null)
            {
                returnUrl = Request.UrlReferrer.ToString();
            }
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [AcceptVerbs("POST")]
        public ActionResult Register(UserInformation userInformation)
        {
            if (userInformation.Password != userInformation.ConfirmPassword)
            {
                TempData["ErrorMessage"] = "Registration failed! Your passwords must match, please re-enter and try again";
            }
            else
            {
                MembershipCreateStatus membershipCreateStatus = new MembershipCreateStatus();
                try
                {
                    Membership.CreateUser(userInformation.UserName, userInformation.Password, userInformation.Email, userInformation.SecretQuestion, userInformation.SecretAnswer, true, out membershipCreateStatus);
                    if (membershipCreateStatus == MembershipCreateStatus.Success)
                    {
                        if (Membership.ValidateUser(userInformation.UserName, userInformation.Password))
                        {
                            FormsAuthentication.SetAuthCookie(userInformation.UserName, false);
                            if (!String.IsNullOrEmpty(userInformation.ReturnUrl))
                                return this.Redirect(userInformation.ReturnUrl);
                            else
                                return this.Redirect(FormsAuthentication.DefaultUrl);
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Login failed! Please make sure you are using the correct user name and password.";
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = GetErrorMessage(membershipCreateStatus);
                    }
                }
                catch (Exception exception)
                {
                    TempData["ErrorMessage"] = exception.Message;
                }
            }
            //If failed, keep the registory page.
            return View("Register", userInformation);
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="confirmNewPassword">The confirm new password.</param>
        /// <param name="resetPassword">The reset password.</param>
        /// <returns></returns>
        public ActionResult ChangePassword(string resetPassword)
        {
            UserInformation userInformation = new UserInformation();
            if (!string.IsNullOrEmpty(resetPassword))
            {
                userInformation.Password = resetPassword;
            }
            return View(userInformation);
        }

        [AcceptVerbs("POST")]
        public ActionResult ChangePassword(UserInformation userInformation)
        {
            if (userInformation.ChangePassword != userInformation.ConfirmPassword)
            {
                TempData["ErrorMessage"] = "Your new passwords do not match, please retype them and try again";
                return View(userInformation);
            }

            try
            {
                MembershipUser membershipUser = Membership.GetUser(HttpContext.User.Identity.Name, false);
                membershipUser.ChangePassword(userInformation.Password, userInformation.ChangePassword);
                TempData["SuccessMessage"] = "Your password has been sucessfully changed";
                return View(userInformation);
            }
            catch (Exception exception)
            {
                TempData["ErrorMessage"] = "Password change has failed because: " + exception.Message;
                return View(userInformation);
            }
        }

        /// <summary>
        /// Users the profile.
        /// initially render our UserProfile view
        /// </summary>
        [Authorize]
        public ActionResult UserProfile()
        {
            string id = HttpContext.User.Identity.Name.ToString();

            ProfileBase profileBase;
            if (!String.IsNullOrEmpty(id))
            {
                profileBase = ProfileBase.Create(id);
            }
            else
            {
                profileBase = HttpContext.Profile as ProfileBase;
            }

            ViewData["subscriptionType"] = ProfileInformation.GetSubscriptionList(profileBase.GetPropertyValue("Subscription").ToString());
            ViewData["genderType"] = ProfileInformation.GetGenderList(profileBase.GetPropertyValue("PersonalInformation.Gender").ToString());
            ViewData["country"] = ProfileInformation.GetLanguageList(profileBase.GetPropertyValue("ContactInformation.Country").ToString());
            ViewData["occupation"] = ProfileInformation.GetOccupationList(profileBase.GetPropertyValue("PersonalInformation.Occupation").ToString());
            ViewData["language"] = ProfileInformation.GetLanguageList(profileBase.GetPropertyValue("Language").ToString());

            ProfileInformation profileInformation = new ProfileInformation()
            {
                FirstName = profileBase.GetPropertyValue("PersonalInformation.FirstName").ToString(),
                LastName = profileBase.GetPropertyValue("PersonalInformation.LastName").ToString(),
                BirthDate = (DateTime)profileBase.GetPropertyValue("PersonalInformation.BirthDate"),
                Website = profileBase.GetPropertyValue("PersonalInformation.Website").ToString(),
                Street = profileBase.GetPropertyValue("ContactInformation.Street").ToString(),
                City = profileBase.GetPropertyValue("ContactInformation.City").ToString(),
                State = profileBase.GetPropertyValue("ContactInformation.State").ToString(),
                Zipcode = profileBase.GetPropertyValue("ContactInformation.ZipCode").ToString()
            };

            return View(profileInformation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="profileInformation"></param>
        /// <returns></returns>
        [Authorize]
        [AcceptVerbs("POST")]
        public ActionResult UserProfile(ProfileInformation profileInformation)
        {
            ProfileBase profileBase = HttpContext.Profile as ProfileBase;
            profileBase.SetPropertyValue("Subscription", profileInformation.SubscriptionType);
            profileBase.SetPropertyValue("Language", profileInformation.Language);

            profileBase.SetPropertyValue("PersonalInformation.FirstName", profileInformation.FirstName);
            profileBase.SetPropertyValue("PersonalInformation.LastName", profileInformation.LastName);
            profileBase.SetPropertyValue("PersonalInformation.Gender", profileInformation.GenderType);
            if (profileInformation.BirthDate != null)
            {
                profileBase.SetPropertyValue("PersonalInformation.BirthDate", profileInformation.BirthDate);
            }
            profileBase.SetPropertyValue("PersonalInformation.Occupation", profileInformation.Occupation);
            profileBase.SetPropertyValue("PersonalInformation.Website", profileInformation.Website);

            profileBase.SetPropertyValue("ContactInformation.Street", profileInformation.Street);
            profileBase.SetPropertyValue("ContactInformation.City", profileInformation.City);
            profileBase.SetPropertyValue("ContactInformation.State", profileInformation.State);
            profileBase.SetPropertyValue("ContactInformation.ZipCode", profileInformation.Zipcode);
            profileBase.SetPropertyValue("ContactInformation.Country", profileInformation.Country);
            profileBase.Save();

            ViewData["subscriptionType"] = ProfileInformation.GetSubscriptionList(profileBase.GetPropertyValue("Subscription").ToString());
            ViewData["genderType"] = ProfileInformation.GetGenderList(profileBase.GetPropertyValue("PersonalInformation.Gender").ToString());
            ViewData["country"] = ProfileInformation.GetLanguageList(profileBase.GetPropertyValue("ContactInformation.Country").ToString());
            ViewData["occupation"] = ProfileInformation.GetOccupationList(profileBase.GetPropertyValue("PersonalInformation.Occupation").ToString());
            ViewData["language"] = ProfileInformation.GetLanguageList(profileBase.GetPropertyValue("Language").ToString());

            TempData["SuccessMessage"] = "Your profile information has been saved";
            return View(profileInformation);
        }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <param name="membershipCreateStatus">The membership create status.</param>
        /// <returns></returns>
        [NonAction]
        private string GetErrorMessage(MembershipCreateStatus membershipCreateStatus)
        {
            switch (membershipCreateStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }

}