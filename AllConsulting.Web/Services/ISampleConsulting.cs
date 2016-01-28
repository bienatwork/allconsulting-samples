// Order services
// *****************************************************************************************
//
// Name:		ISampleConsulting.cs
//
// Created:		28.01.2016 ACAG  
// Modified:	28.01.2016 ACAG  	: Creation 
//
// *****************************************************************************************

using System;
using System.Collections.Generic;
using System.ServiceModel; 
using System.ServiceModel.Web;  
using ACAG.Web.Models;

namespace ACAG.Web.Services
{
    /// <summary>
    /// This is the class interface definition.
    /// </summary>
    [ServiceContract]
    public interface ISampleConsulting  
    {
        #region Utls

        /// <summary>
        /// This method return DateTime.Now.
        /// <remarks>Attribute Method</remarks>
        /// <list type="bullet">
        /// <item><term><description>Method: Get</description></term></item>
        /// <item><term><description>Response: Json</description></term></item>
        /// <item><term><description>Client Reference path: /Services/SampleConsulting.svc/getDateNow/</description></term></item>
        /// </list>
        /// </summary>
        /// <returns>DateTime</returns>
        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "getDateNow/")]
        DateTime GetDateNow();

        #endregion

        #region Order

        /// <summary>
        /// This method return List Order
        /// <remarks>Attribute Method</remarks>
        /// <list type="bullet">
        /// <item><term><description>Method: POST</description></term></item>
        /// <item><term><description>Response: Json</description></term></item>
        /// <item><term><description>Client Reference path: /Services/SampleConsulting.svc/list-orders/{key}</description></term></item>
        /// </list>
        /// <param name="take">Page Size</param>
        /// <param name="skip">Page Index</param>
        /// <param name="sort">Sort field</param>
        /// <param name="searchFullOrder">Search</param>
        /// <param name="key">Reresh data.</param>
        /// </summary>
        /// <returns>FilterOrderModel</returns>
        [
        OperationContract,
        WebInvoke(
            Method = "POST", 
            ResponseFormat = WebMessageFormat.Json, 
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "list-orders/{key}")
        ]
        FilterOrderModel OrderGetList(
            int take,
            int skip,
            string sort,
            string searchFullOrder,
            string key);
        /// <summary>
        /// Get Order by Id
        /// <remarks>Attribute Method</remarks>
        /// <list type="bullet">
        /// <item><term><description>Method: GET</description></term></item>
        /// <item><term><description>Response: Json</description></term></item>
        /// <item><term><description>Client Reference path: /Services/SampleConsulting.svc/orders/{OrderId}/{key}</description></term></item>
        /// </list>
        /// <param name="id">OrderID</param> 
        /// <param name="key">Reresh data.</param>
        /// </summary>
        /// <returns>OrderModel</returns>
        [
        OperationContract,
        WebInvoke(
            Method = "GET", 
            ResponseFormat = WebMessageFormat.Json, 
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "orders/{id}/{key}")
        ]
        OrderModel OrderById(
            string id,
            string key);
        /// <summary>
        /// Add Order
        /// <remarks>Attribute Method</remarks>
        /// <list type="bullet">
        /// <item><term><description>Method: POST</description></term></item>
        /// <item><term><description>Response: Json</description></term></item>
        /// <item><term><description>Client Reference path: /Services/SampleConsulting.svc/addOrder/}</description></term></item>
        /// </list>
        /// <param name="order">order</param>  
        /// </summary>
        /// <returns>int</returns>
        [
        OperationContract,
        WebInvoke(Method = "POST", 
            ResponseFormat = WebMessageFormat.Json, 
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "addOrder/")
        ]
        int AddOrder(OrderModel order);
        /// <summary>
        /// Update Order
        /// <remarks>Attribute Method</remarks>
        /// <list type="bullet">
        /// <item><term><description>Method: POST</description></term></item>
        /// <item><term><description>Response: Json</description></term></item>
        /// <item><term><description>Client Reference path: /Services/SampleConsulting.svc/updateOrder/{id}</description></term></item>
        /// </list>
        /// <param name="order">order</param>  
        /// <param name="id">OrderId</param>
        /// </summary>
        /// <returns>int</returns>
        [
        OperationContract,
        WebInvoke(Method = "POST", 
            ResponseFormat = WebMessageFormat.Json, 
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "updateOrder/{id}")
        ]
        int UpdateOrder(
            OrderModel order,
            string id);
        /// <summary>
        /// Delete Order
        /// <remarks>Attribute Method</remarks>
        /// <list type="bullet">
        /// <item><term><description>Method: POST</description></term></item>
        /// <item><term><description>Response: Json</description></term></item>
        /// <item><term><description>Client Reference path: /Services/SampleConsulting.svc/deleteOrder/</description></term></item>
        /// </list>
        /// <param name="orderId">orderId</param>  
        /// </summary>
        /// <returns>int</returns>
        [
        OperationContract,
        WebInvoke(
            Method = "POST", 
            ResponseFormat = WebMessageFormat.Json, 
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "deleteOrder/")
        ]
        int DeleteOrder(int orderId);
        /// <summary>
        /// Clone order
        /// <remarks>Attribute Method</remarks>
        /// <list type="bullet">
        /// <item><term><description>Method: POST</description></term></item>
        /// <item><term><description>Response: Json</description></term></item>
        /// <item><term><description>Client Reference path: /Services/SampleConsulting.svc/reOrder/</description></term></item>
        /// </list>
        /// <param name="order">OrderModel</param>  
        /// </summary>
        /// <returns>OrderModel</returns>
        [
        OperationContract,
        WebInvoke(
            Method = "POST", 
            ResponseFormat = WebMessageFormat.Json, 
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "reOrder/")
        ]
        OrderModel ReOrder(OrderModel order);

        #endregion

        #region Position

        /// <summary>
        /// Get list position by OrderId
        /// <remarks>Attribute Method</remarks>
        /// <list type="bullet">
        /// <item><term><description>Method: GET</description></term></item>
        /// <item><term><description>Response: Json</description></term></item>
        /// <item><term><description>Client Reference path: /Services/SampleConsulting.svc/position/{orderid}/{key}</description></term></item>
        /// </list>
        /// <param name="id">id</param>  
        /// <param name="key">Refresh data.</param>
        /// </summary>
        /// <returns>List PositionOrderModel</returns>
        [
        OperationContract,
        WebInvoke(
            Method = "GET", 
            ResponseFormat = WebMessageFormat.Json, 
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "position/{id}/{key}")]
        List<PositionOrderModel> GetPossitionList(
            string id,
            string key);
        /// <summary>
        /// Delete list position id
        /// <remarks>Attribute Method</remarks>
        /// <list type="bullet">
        /// <item><term><description>Method: POST</description></term></item>
        /// <item><term><description>Response: Json</description></term></item>
        /// <item><term><description>Client Reference path: /Services/SampleConsulting.svc/deletePositionOrder/</description></term></item>
        /// </list>
        /// <param name="listPositionId">list Position Id</param>  
        /// </summary>
        /// <returns>int</returns>
        [
        OperationContract,
        WebInvoke(
            Method = "POST", 
            ResponseFormat = WebMessageFormat.Json, 
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "deletePositionOrder/")]
        int DeletePossitionOrder(List<int> listPositionId);

        #endregion 

    }
}
