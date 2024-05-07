using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagement.Session
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

        public static string Username
        {
            get
            {
                return HttpContext.Current.Session["Username"] == null ? "" : (string)HttpContext.Current.Session["Username"];
            }
            set
            {
                HttpContext.Current.Session["Username"] = value;
            }
        }

        public static string UserRole
        {
            get
            {
                return HttpContext.Current.Session["UserRole"] == null ? "" : (string)HttpContext.Current.Session["UserRole"];
            }
            set
            {
                HttpContext.Current.Session["UserRole"] = value;
            }
        }

    }
}