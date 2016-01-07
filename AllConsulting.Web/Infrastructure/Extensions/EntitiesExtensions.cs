using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AllConsulting.Entities;
using AllConsulting.Web.Models;

namespace AllConsulting.Web.Infrastructure.Extensions
{
    public static class EntitiesExtensions
    {
        public static void UpdateOrder(this Order entity, OrderViewModel viewModel)
        {
            entity.CustomerNumber = viewModel.CustomerNumber;
            entity.OrderDate = viewModel.OrderDate;
            entity.DeliveryDate = viewModel.DeliveryDate;
            entity.TotalPrice = viewModel.TotalPrice;
        }
    }
}