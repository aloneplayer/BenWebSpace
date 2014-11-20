using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Profile;

namespace WebAppSkeleton
{
    public class Global : System.Web.HttpApplication
    {
        void Application_Start(Object sender, EventArgs e)
        {
        }

        void Application_End(Object sender, EventArgs e)
        {
        }

        void Application_Error(Object sender, EventArgs e)
        {

        }

        void Session_Start(Object sender, EventArgs e)
        {
    
        }

        void Session_End(Object sender, EventArgs e)
        {
        }

        void Profile_MigrateAnonymous(object sender, ProfileMigrateEventArgs e)
        {
            //// get a reference to the previously anonymous user's profile
            //ProfileCommon anonProfile = this.Profile.GetProfile(e.AnonymousID);
            //// if set, copy its Theme and ShoppingCart to the current user's profile
            //if (anonProfile.ShoppingCart.Items.Count > 0)
            //    this.Profile.ShoppingCart = anonProfile.ShoppingCart;
            //if (!string.IsNullOrEmpty(anonProfile.Preferences.Theme))
            //    this.Profile.Preferences.Theme = anonProfile.Preferences.Theme;
            //// delete the anonymous profile
            //ProfileManager.DeleteProfile(e.AnonymousID);
            //AnonymousIdentificationModule.ClearAnonymousIdentifier();
        }
    }
}