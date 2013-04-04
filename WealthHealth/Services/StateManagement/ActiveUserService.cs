using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using WebMatrix.WebData;

namespace WealthHealth.Services.StateManagement
{
    public class ActiveUserService
    {
        const string ActiveUserSessionKey = "currentUser";

        public bool UnsetUserSession()
        {
            HttpContext.Current.Session[ActiveUserSessionKey] = null;
            return (HttpContext.Current.Session[ActiveUserSessionKey] == null);
        }

        public bool VerifyActiveUserSessionExists()
        {
            return (GetCurrentUserSession() != null);
        }

        public int GetUserId()
        {
            var userIdStoredInSession = GetCurrentUserSessionSpecifiedValue("userId");
            return userIdStoredInSession != "" ? Convert.ToInt32(userIdStoredInSession) : 0;
        }

        public string GetUserName()
        {
            return GetCurrentUserSessionSpecifiedValue("userName");
        }

        public string GetCurrentUserSessionSpecifiedValue(string key)
        {
            Dictionary<string, string> userSession = GetCurrentUserSession();

            if (userSession != null && userSession.ContainsKey(key))
            {
                return userSession[key];
            }

            return null;
        }

        public Dictionary<string, string> GetCurrentUserSession()
        {
            if (HttpContext.Current.Session[ActiveUserSessionKey] != null)
            {
                var userSession = (Dictionary<string, string>)HttpContext.Current.Session[ActiveUserSessionKey];

				if (userSession["userName"].Equals(HttpContext.Current.User.Identity.Name.ToLower()))
                {
                    return userSession;
                }
            }
            
            return GenerateCurrentUserSession() ? GetCurrentUserSession() : null;
        }

        public bool GenerateCurrentUserSession()
        {
            var sessionDictionary = BuildCurrentUserStorageDictionary();
            if (sessionDictionary != null && sessionDictionary.ContainsKey("userId") && sessionDictionary.ContainsKey("userName"))
            {
                HttpContext.Current.Session[ActiveUserSessionKey] = sessionDictionary;
                return true;
            }

            return false;
        }

        public Dictionary<string, string> BuildCurrentUserStorageDictionary()
        {
            var userDictionary = new Dictionary<string, string>
                                     {
                                         {"userId", WebSecurity.GetUserId(HttpContext.Current.User.Identity.Name).ToString(CultureInfo.InvariantCulture)},
                                         {"userName", HttpContext.Current.User.Identity.Name.ToLower()}
                                     };

            return userDictionary;
        }
    }
}