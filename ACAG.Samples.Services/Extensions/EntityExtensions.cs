// ACAG.Samples.BusinessServices.Extensions
// *****************************************************************************************
//
// Name:        EntityExtensions.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using System.Collections.Generic;
using System.Linq;
using ACAG.Samples.BusinessServices.Models;
using ACAG.Samples.Entities;

namespace ACAG.Samples.BusinessServices.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdateOrder(this Order entity, OrderModel viewModel)
        {
            entity.CustomerNumber = viewModel.CustomerNumber;
            entity.OrderDate = viewModel.OrderDate;
            entity.DeliveryDate = viewModel.DeliveryDate;

            if (viewModel.OrderPositions != null)
            {
                if (entity.OrderPositions == null)
                    entity.OrderPositions = new List<OrderPosition>();

                foreach (OrderPositionModel pos in viewModel.OrderPositions)
                {
                    var posEntity = entity.OrderPositions.SingleOrDefault(x => x.ID == pos.PositionOrderId);
                    if (posEntity == null || posEntity.ID == 0)
                    {
                        posEntity = new OrderPosition();
                        posEntity.UpdateOrderPosition(pos);
                        entity.OrderPositions.Add(posEntity);
                    }
                    else
                    {
                        posEntity.UpdateOrderPosition(pos);
                    }
                }
            }

            entity.TotalPrice = entity.OrderPositions.Sum(x => x.Total);
        }

        public static void UpdateOrderPosition(this OrderPosition entity, OrderPositionModel model)
        {
            entity.PositionNumber = model.PositionNumber;
            entity.Pieces = model.Pieces;
            entity.Text = model.Text;
            entity.Price = model.Price;
            entity.Total = model.PositionNumber * model.Price;
        }

        public static void CopyFrom(this Order newOrder, Order oldOrder)
        {
            newOrder.CustomerNumber = oldOrder.CustomerNumber;
            newOrder.OrderDate = oldOrder.OrderDate;
            newOrder.DeliveryDate = oldOrder.DeliveryDate;
            newOrder.TotalPrice = oldOrder.TotalPrice;

            if (oldOrder.OrderPositions != null)
            {
                newOrder.OrderPositions = oldOrder.OrderPositions.Select(x => new OrderPosition
                {
                    PositionNumber = x.PositionNumber,
                    Pieces = x.Pieces,
                    Text = x.Text,
                    Price = x.Price,
                    Total = x.Total
                }).ToList();
            }
        }
    }
}
