using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShyamDhokiya_557.Session
{
    public class SessionHelper
    {
        public static int UserId
        {
            get
            {
                return HttpContext.Current.Session["UserId"] == null ? 0 : (int)HttpContext.Current.Session["UserId"];
            }
            set
            {
                HttpContext.Current.Session["UserId"] = value;
            }
        }
        public static string UserEmail
        {
            get
            {
                return HttpContext.Current.Session["UserEmail"] == null ? "" : (string)HttpContext.Current.Session["UserEmail"];
            }
            set
            {
                HttpContext.Current.Session["UserEmail"] = value;
            }

        }

        public static string Role
        {
            get
            {
                return HttpContext.Current.Session["Role"] == null ? "" : (string)HttpContext.Current.Session["Role"];
            }
            set
            {
                HttpContext.Current.Session["Role"] = value;
            }
        }
    }
}