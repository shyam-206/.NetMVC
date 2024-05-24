using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizManagement.Session
{
    public class SessionHelper
    {
        public static int UserId
        {
            get
            {
                return HttpContext.Current.Session["UserId"] == null ? 0 : (int)HttpContext.Current.Session["userId"];
            }
            set
            {
                HttpContext.Current.Session["UserId"] = value;
            }
        }

        public static string Useremail
        {
            get
            {
                return HttpContext.Current.Session["Useremail"] == null ? "" : (string)HttpContext.Current.Session["Useremail"];
            }
            set
            {
                HttpContext.Current.Session["Useremail"] = value;
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
    }
}