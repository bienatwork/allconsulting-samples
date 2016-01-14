﻿using System.Web;

namespace AllConsulting.CommonLib
{
   public static class OrderSession
   {

       public static void SetOrderSession(string key, object value)
       {
           HttpContext.Current.Session[key] = value;
       }

       public static object GetOrderSession(string key)
       {
           return HttpContext.Current.Session[key];
       }

       public static T GetSession<T>(this HttpSessionStateBase session, string key)
       {
           return (T)session[key];
       }

       public static void SetSession<T>(this HttpSessionStateBase session, string key, object value)
       {
           session[key] = value;
       }
    }
}