﻿using System;
using System.Web;
using System.Web.Mvc;

namespace TaskManagement
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute() { ExceptionType = typeof(Exception), View = "Error" }); 
        }
    }
}
