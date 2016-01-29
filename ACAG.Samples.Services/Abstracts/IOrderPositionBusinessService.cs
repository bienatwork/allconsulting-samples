// ACAG.Samples.BusinessServices.Abstracts
// *****************************************************************************************
//
// Name:        IOrderPositionBusinessService.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using System.Collections.Generic;
using ACAG.Samples.BusinessServices.Models;

namespace ACAG.Samples.BusinessServices.Abstracts
{
    public interface IOrderPositionBusinessService
    {
        void DeletePositions(IList<int> positionIdList);
        List<OrderPositionModel> GetPositionListByOrderId(int id);
    }
}
