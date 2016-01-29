// ACAG.Samples.Web.Services
// *****************************************************************************************
//
// Name:        SampleConsulting.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using ACAG.Samples.BusinessServices.Abstracts;
using ACAG.Samples.BusinessServices.Impls;
using ACAG.Samples.BusinessServices.Models;
using ACAG.Samples.Data.Infrastructure;
using ACAG.Samples.Data.Repositories;
using ACAG.Samples.Entities;

namespace ACAG.Samples.Web.Services
{
    /// <summary>
    /// This class implements the interface  <see cref="ISampleConsulting"/>
    /// </summary>
    public class SampleConsulting : ISampleConsulting, IDisposable
    {
        #region Fields

        private readonly IOrderBusinessService _orderBusinessService;
        private readonly IOrderPositionBusinessService _positionBusinessService;

        #endregion

        #region Constructor

        public SampleConsulting()
        {
            IDbFactory dbFactory = new DbFactory();
            IUnitOfWork unitOfWork = new UnitOfWork(dbFactory);

            IOrderRepository orderRepository = new OrderRepository(dbFactory);
            _orderBusinessService = new OrderBusinessService(unitOfWork, orderRepository);

            IOrderPositionRepository orderPositionRepository = new OrderPositionRepository(dbFactory);
            _positionBusinessService = new OrderPositionBusinessService(unitOfWork, orderPositionRepository);
        }

        /// <summary>
        /// Constructor class SampleConsulting
        /// </summary>
        /// <param name="orderBusinessService"></param>
        /// <param name="positionBusinessService"></param>
        public SampleConsulting(IOrderBusinessService orderBusinessService,
            IOrderPositionBusinessService positionBusinessService)
        {
            _orderBusinessService = orderBusinessService;
            _positionBusinessService = positionBusinessService;
        }

        #endregion

        #region Utls

        public DateTime GetDateNow()
        {
            return DateTime.Now;
        }

        #endregion

        #region "Order Services"

        public SearchResultModel OrderGetList(int take, int skip, string sort, string searchFullOrder, string key)
        {
            if (string.IsNullOrWhiteSpace(searchFullOrder))
                searchFullOrder = string.Empty;

            var orders = _orderBusinessService.GetAll(sort, searchFullOrder, key).ToList();
            int totalRows = orders.Count;
            return new SearchResultModel
            {
                TotalRows = totalRows,
                ListOrder = orders.Skip(skip).Take(take).ToList()
            };
        }

        public int AddOrder(OrderModel orderModel)
        {
            if (orderModel.OrderPositions.Count < 1)
                return -1;

            var order = new Order
            {
                CustomerNumber = orderModel.CustomerNumber.Trim(),
                TotalPrice = orderModel.TotalPrice,
                DeliveryDate = orderModel.DeliveryDate,
                OrderDate = DateTime.Now
            };

            foreach (var position in orderModel.OrderPositions)
            {
                var positionOrder = new OrderPosition
                {
                    OrderID = order.ID,
                    Pieces = position.Pieces != null ? position.Pieces.Trim() : null,
                    PositionNumber = position.PositionNumber,
                    Price = position.Price,
                    Text = position.Text != null ? position.Text.Trim() : null,
                    Total = position.Total
                };
                order.TotalPrice += positionOrder.Total;
                order.OrderPositions.Add(positionOrder);
            }

            try
            {
                _orderBusinessService.AddOrder(order);
            }
            catch (Exception ex)
            {
                //TODO:
                throw;
            }
            return order.ID;
        }

        public OrderModel OrderById(string id, string key)
        {
            int orderId = 0;
            int.TryParse(id, out orderId);
            var order = _orderBusinessService.FindOrderBy(orderId, true);
            return order;
        }

        public int UpdateOrder(OrderModel orderModel, string id)
        {
            int orderId = 0;
            int.TryParse(id, out orderId);


            try
            {
                _orderBusinessService.UpdateOrder(orderId, orderModel);
            }
            catch (Exception ex)
            {
                //TODO:
                throw;
            }

            return 1;
        }

        public int DeleteOrder(int orderId)
        {
            try
            {
                _orderBusinessService.DeleteOrder(orderId);
                return orderId;
            }
            catch (Exception ex)
            {
                //TODO:
                throw;
            }
        }

        public OrderModel ReOrder(OrderModel model)
        {
            OrderModel newOrder = _orderBusinessService.ReOrder(model.OrderId);
            return OrderById(newOrder.OrderId.ToString(), DateTime.Now.TimeOfDay.ToString());
        }

        #endregion

        #region Position

        public int DeletePossitionOrder(List<int> listOrderId)
        {
            try
            {
                _positionBusinessService.DeletePositions(listOrderId);
            }
            catch (Exception ex)
            {
                //TODO:
                throw;
            }
            return 1;
        }

        public List<OrderPositionModel> GetPossitionList(string id, string key)
        {
            int orderId = 0;
            int.TryParse(id, out orderId);
            List<OrderPositionModel> positionList = _positionBusinessService.GetPositionListByOrderId(orderId);
            return positionList;
        }

        #endregion

        #region Disposable
        /// <summary>
        /// Disponse
        /// </summary>
        public void Dispose()
        {
        }

        ~SampleConsulting()
        {
            Dispose();
        }

        #endregion

    }
}
