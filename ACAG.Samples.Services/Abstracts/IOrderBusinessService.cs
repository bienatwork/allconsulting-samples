// ACAG.Samples.BusinessServices.Abstracts
// *****************************************************************************************
//
// Name:        IOrderBusinessService.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using System.Collections.Generic;
using ACAG.Samples.BusinessServices.Models;
using ACAG.Samples.Entities;

namespace ACAG.Samples.BusinessServices.Abstracts
{
    public interface IOrderBusinessService
    {
        IEnumerable<OrderModel> GetAll(string sort, string searchFullOrder, string key);
        void AddOrder(Order order);
        OrderModel FindOrderBy(int orderId, bool includedPositions);
        void UpdateOrder(int orderId, OrderModel orderModel);
        void DeleteOrder(int orderId);
        OrderModel ReOrder(int orderId);
    }
}
