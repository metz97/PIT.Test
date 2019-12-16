using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend.Models
{
    public class TokenContainer : ITokenContainer
    {
        private const string ApiTokenKey = "ApiToken";
        public const string UsernameKey = "Username";
        public object ApiToken
        {
            get { return Current.Session != null ? Current.Session[ApiTokenKey] : null; }
            set { if (Current.Session != null) Current.Session[ApiTokenKey] = value; }
        }

        public object Username
        {
            get { return Current.Session != null ? Current.Session[UsernameKey] : null; }
            set { if (Current.Session != null) Current.Session[UsernameKey] = value; }
        }

        private static HttpContextBase Current
        {
            get { return new HttpContextWrapper(HttpContext.Current); }
        }
    }
}