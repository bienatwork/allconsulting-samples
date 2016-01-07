using System;
using System.Collections.Generic;
using System.ServiceModel; 
using System.ServiceModel.Web; 
using AllConsulting.Web.Dto;

namespace AllConsulting.Web.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISampleConsulting" in both code and config file together.
    [ServiceContract]
    public interface ISampleConsulting
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "list-orders/{key}")]
        FilterOrderDto OrderGetList(int take, int skip, string sort, string searchFullOrder, string key);

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "getDateNow/")]
        DateTime GetDateNow();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "position/{id}/{key}")]
        List<PositionOrderDto> GetPossitionList(string id, string key);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "orders/{id}/{key}")]
        OrderDto OrderById(string id, string key);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "addOrder/")]
        int AddOrder(OrderDto order);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "updateOrder/{id}")]
        int UpdateOrder(OrderDto order, string id);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "deletePositionOrder/")]
        int DeletePossitionOrder(List<int> listOrderId);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "deleteOrder/")]
        int DeleteOrder(int orderId);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "reOrder/")]
        OrderDto ReOrder(OrderDto order);

    }
}
