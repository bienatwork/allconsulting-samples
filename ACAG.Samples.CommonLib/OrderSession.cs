// ACAG.Samples.CommonLib
// *****************************************************************************************
//
// Name:        OrderSession.cs
//
// Created:     21.12.2015 Kloon
// Modified:    21.12.2015 Kloon    : Creation
//
// *****************************************************************************************
using System.Web;

namespace ACAG.Samples.CommonLib
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
