// ACAG.Samples.BusinessServices.Impls
// *****************************************************************************************
//
// Name:        OrderPositionBusinessService.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using System.Collections.Generic;
using System.Linq;
using ACAG.Samples.BusinessServices.Abstracts;
using ACAG.Samples.BusinessServices.Models;
using ACAG.Samples.Data.Infrastructure;
using ACAG.Samples.Data.Repositories;
using ACAG.Samples.Entities;

namespace ACAG.Samples.BusinessServices.Impls
{
    public class OrderPositionBusinessService : IOrderPositionBusinessService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderPositionRepository _orderPositionRepository;

        public OrderPositionBusinessService(IUnitOfWork unitOfWork, IOrderPositionRepository orderPositionRepository)
        {
            _orderPositionRepository = orderPositionRepository;
            _unitOfWork = unitOfWork;
        }
        public void DeletePositions(IList<int> positionIdList)
        {
            if (positionIdList != null)
            {
                foreach (int positionId in positionIdList)
                {
                    var position = _orderPositionRepository.GetSingle(positionId);
                    if (position != null)
                    {
                        _orderPositionRepository.Delete(position);
                    }
                }

                _unitOfWork.Commit();
            }
        }

        public List<OrderPositionModel> GetPositionListByOrderId(int orderId)
        {
            IQueryable<OrderPosition> positions = _orderPositionRepository.FindBy(x => x.OrderID == orderId)
                .OrderByDescending(x=>x.PositionNumber);

            return positions.Select(x => new OrderPositionModel
            {
                OrderId = x.OrderID,
                PositionOrderId = x.ID,
                PositionNumber = x.PositionNumber,
                Pieces = x.Pieces,
                Text = x.Text,
                Price = x.Price,
            }).ToList();
        }
    }
}
