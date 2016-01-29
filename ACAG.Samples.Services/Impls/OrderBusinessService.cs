// ACAG.Samples.BusinessServices.Impls
// *****************************************************************************************
//
// Name:        OrderBusinessService.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using ACAG.Samples.BusinessServices.Abstracts;
using ACAG.Samples.BusinessServices.Extensions;
using ACAG.Samples.BusinessServices.Models;
using ACAG.Samples.Data.Infrastructure;
using ACAG.Samples.Data.Repositories;
using ACAG.Samples.Entities;

namespace ACAG.Samples.BusinessServices.Impls
{
    public class OrderBusinessService : IOrderBusinessService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;

        public OrderBusinessService(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
        }

        public IEnumerable<OrderModel> GetAll(string sort, string searchFullOrder, string key)
        {
            var foundOrders = _orderRepository.Find(sort, searchFullOrder);
            if (foundOrders != null)
            {
                return foundOrders.Select(x => new OrderModel
                {
                    OrderId = x.ID,
                    OrderDate = x.OrderDate,
                    CustomerNumber = x.CustomerNumber,
                    DeliveryDate = x.DeliveryDate,
                    TotalPrice = x.TotalPrice
                });
            }
            return null;
        }

        public void AddOrder(Order order)
        {
            _orderRepository.Add(order);
            _unitOfWork.Commit();
        }

        public OrderModel FindOrderBy(int orderId, bool includedPositions)
        {
            IQueryable<Order> entiryQuery = includedPositions ? _orderRepository.AllIncluding(x => x.OrderPositions) : _orderRepository.All;
            var entity = entiryQuery.SingleOrDefault(x => x.ID == orderId);
            if (entity != null)
            {
                var order = new OrderModel
                {
                    OrderId = entity.ID,
                    OrderDate = entity.OrderDate,
                    CustomerNumber = entity.CustomerNumber,
                    DeliveryDate = entity.DeliveryDate,
                    TotalPrice = entity.TotalPrice
                };

                if (entity.OrderPositions != null)
                {
                    order.OrderPositions = new List<OrderPositionModel>();
                    foreach (var positionEntity in entity.OrderPositions)
                    {
                        var positionModel = new OrderPositionModel
                        {
                            OrderId = positionEntity.OrderID,
                            PositionOrderId = positionEntity.ID,
                            PositionNumber = positionEntity.PositionNumber,
                            Pieces = positionEntity.Pieces,
                            Text = positionEntity.Text,
                            Price = positionEntity.Price,
                        };
                        order.OrderPositions.Add(positionModel);
                    }
                }

                return order;
            }
            return null;
        }

        public void UpdateOrder(int orderId, OrderModel orderModel)
        {
            var entity = _orderRepository.AllIncluding(x => x.OrderPositions).SingleOrDefault(x => x.ID == orderId);
            if (entity != null)
            {
                entity.UpdateOrder(orderModel);
                _orderRepository.Edit(entity);
                _unitOfWork.Commit();
            }
        }

        public void DeleteOrder(int orderId)
        {
            var entity = _orderRepository.GetSingle(orderId);
            if (entity != null)
            {
                _orderRepository.Delete(entity);
                _unitOfWork.Commit();
            }
        }

        public OrderModel ReOrder(int orderId)
        {
            var entity = _orderRepository.AllIncluding(x => x.OrderPositions).SingleOrDefault(x => x.ID == orderId);
            if (entity != null)
            {
                // Copy from the existing order and then set the order date to today
                var newOrder = new Order();
                newOrder.CopyFrom(entity);
                newOrder.OrderDate = DateTime.Now;
                newOrder.DeliveryDate = null;

                _orderRepository.Add(newOrder);
                _unitOfWork.Commit();

                var orderModel = new OrderModel
                {
                    OrderId = newOrder.ID,
                    OrderDate = newOrder.OrderDate,
                    CustomerNumber = newOrder.CustomerNumber,
                    DeliveryDate = newOrder.DeliveryDate,
                    TotalPrice = newOrder.TotalPrice
                };

                if (newOrder.OrderPositions != null)
                {
                    orderModel.OrderPositions = new List<OrderPositionModel>();
                    foreach (var positionEntity in newOrder.OrderPositions)
                    {
                        var positionModel = new OrderPositionModel
                        {
                            OrderId = positionEntity.OrderID,
                            PositionOrderId = positionEntity.ID,
                            PositionNumber = positionEntity.PositionNumber,
                            Pieces = positionEntity.Pieces,
                            Text = positionEntity.Text,
                            Price = positionEntity.Price,
                        };
                        orderModel.OrderPositions.Add(positionModel);
                    }
                }
                return orderModel;
            }
            return null;
        }
    }
}
