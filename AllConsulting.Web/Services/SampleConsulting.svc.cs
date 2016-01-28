// Order services
// *****************************************************************************************
//
// Name:		SampleConsulting.cs
//
// Created:		28.01.2016 ACAG  
// Modified:	28.01.2016 ACAG  	: Creation 
//
// *****************************************************************************************
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq; 
using ACAG.Data.Infrastructure;
using ACAG.Data.Repositories;
using ACAG.Entities;
using ACAG.Web.Models;

namespace ACAG.Web.Services
{ 
    /// <summary>
    /// This class implements the interface  <see cref="ISampleConsulting"/>
    /// </summary>
    public class SampleConsulting : ISampleConsulting, IDisposable
    {
        #region Fields

        private readonly IEntityBaseRepository<Order> _ordersRepository;
        private readonly IEntityBaseRepository<OrderPosition> _positionRepository;
        private IDbFactory _dbFactory;
        private IUnitOfWork _unitOfWork;


        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor, initializes a new <see cref="SampleConsulting class instance."/>
        /// </summary>
        public SampleConsulting()
        {
            _dbFactory = new DbFactory();
            _unitOfWork = new UnitOfWork(_dbFactory);
            _ordersRepository = new EntityBaseRepository<Order>(_dbFactory);
            _positionRepository = new EntityBaseRepository<OrderPosition>(_dbFactory);
        }
        /// <summary>
        /// Constructor class SampleConsulting
        /// </summary>
        /// <param name="ordersRepository">ordersRepository</param>
        /// <param name="positionRepository">positionRepository</param>
        public SampleConsulting(
            IEntityBaseRepository<Order> ordersRepository,
            IEntityBaseRepository<OrderPosition> positionRepository)
        {
            _ordersRepository = ordersRepository;
            _positionRepository = positionRepository;
        }


        #endregion 

        #region Utls

        public DateTime GetDateNow()
        {
            return DateTime.Now;
        }

        #endregion

        #region Order

        public FilterOrderModel OrderGetList(
           int take,
           int skip,
           string sort,
           string searchFullOrder,
           string key)
        {

            if (string.IsNullOrWhiteSpace(searchFullOrder)) searchFullOrder = string.Empty;
            // Dictionary<string, string> -- operation and value
            var list =
                _ordersRepository.GetAll()
                    .OrderByDescending(o => o.ID)
                    .Where(
                        x =>
                            x.CustomerNumber.Contains(searchFullOrder)
                            ||
                            (SqlFunctions.DateName("dd", x.OrderDate) + "/" +
                             SqlFunctions.DatePart("mm", x.OrderDate) + "/" +
                             SqlFunctions.DateName("yyyy", x.OrderDate)).Contains(searchFullOrder)
                            ||
                            (SqlFunctions.DateName("dd", x.DeliveryDate) + "/" +
                             SqlFunctions.DatePart("mm", x.DeliveryDate) + "/" +
                             SqlFunctions.DateName("yyyy", x.DeliveryDate)).Contains(searchFullOrder) ||
                             SqlFunctions.StringConvert(x.TotalPrice).Contains(searchFullOrder));
            var result = new FilterOrderModel();
            result.TotalRows = list.Count();
            switch (sort.ToLower())
            {
                case "customernumber desc":
                    list = list.OrderByDescending(x => x.CustomerNumber);
                    break;
                case "customernumber asc":
                    list = list.OrderBy(x => x.CustomerNumber);
                    break;
                case "deliverydate desc":
                    list = list.OrderByDescending(x => x.DeliveryDate);
                    break;
                case "deliverydate asc":
                    list = list.OrderBy(x => x.DeliveryDate);
                    break;
                case "orderdate desc":
                    list = list.OrderByDescending(x => x.OrderDate);
                    break;
                case "orderdate asc":
                    list = list.OrderBy(x => x.OrderDate);
                    break;
                case "totalprice desc":
                    list = list.OrderByDescending(x => x.TotalPrice);
                    break;
                case "totalprice asc":
                    list = list.OrderBy(x => x.TotalPrice);
                    break;
            }
            result.ListOrder = list.Skip(skip).Take(take).Select(
                x => new OrderModel()
                {
                    OrderId = x.ID,
                    OrderDate = x.OrderDate,
                    CustomerNumber = x.CustomerNumber,
                    DeliveryDate = x.DeliveryDate,
                    TotalPrice = x.TotalPrice
                }).ToList();
            return result;
        }

        public int AddOrder(OrderModel o)
        {
            if (o.ListPositionOrderDto.Count < 1)
                return -1;
            var order = new Order();
            order.CustomerNumber = o.CustomerNumber.Trim();
            order.TotalPrice = o.TotalPrice;
            order.DeliveryDate = o.DeliveryDate;
            order.OrderDate = DateTime.Now;

            _ordersRepository.Add(order);
            // commit
            _unitOfWork.Commit();
            foreach (var OrderModel in o.ListPositionOrderDto)
            {
                var positionOrder = new OrderPosition();
                positionOrder.OrderID = order.ID;
                positionOrder.Pieces = OrderModel.Pieces != null ? OrderModel.Pieces.Trim() : null;
                positionOrder.PositionNumber = OrderModel.PositionNumber;
                positionOrder.Price = OrderModel.Price;
                positionOrder.Text = OrderModel.Text != null ? OrderModel.Text.Trim() : null;
                positionOrder.Total = OrderModel.Total;
                order.TotalPrice += positionOrder.Total;
                _positionRepository.Add(positionOrder);
            }
            _unitOfWork.Commit();

            return order.ID;
        }

        public OrderModel OrderById(
            string id,
            string key)
        {
            int orderId;
            int.TryParse(id, out orderId);
            if (orderId < 1) return null;
            var order = _ordersRepository.GetSingle(orderId);
            if (order == null) return null;
            var o = new OrderModel();
            o.CustomerNumber = order.CustomerNumber;
            o.DeliveryDate = order.DeliveryDate;
            o.OrderDate = order.OrderDate;
            o.OrderId = order.ID;
            o.TotalPrice = order.TotalPrice;

            var listposition = _positionRepository.FindBy(p => p.OrderID == orderId);
            o.ListPositionOrderDto = new List<PositionOrderModel>();
            foreach (var positionOrder in listposition)
            {
                var possitionOrderModel = new PositionOrderModel();
                possitionOrderModel.OrderId = positionOrder.OrderID;
                possitionOrderModel.PositionOrderId = positionOrder.ID;
                possitionOrderModel.PositionNumber = positionOrder.PositionNumber;
                possitionOrderModel.Pieces = positionOrder.Pieces;
                possitionOrderModel.Text = positionOrder.Text;
                possitionOrderModel.Price = positionOrder.Price;
                o.ListPositionOrderDto.Add(possitionOrderModel);
            }
            return o;
        }

        public int UpdateOrder(
            OrderModel o,
            string id)
        {
            int orderId = 0;
            int.TryParse(id, out orderId);
            var order = _ordersRepository.GetSingle(orderId);
            if (order != null)
            {
                order.DeliveryDate = o.DeliveryDate;
                order.CustomerNumber = o.CustomerNumber.Trim();
                order.TotalPrice = 0;
                foreach (var poDto in o.ListPositionOrderDto)
                {
                    if (poDto.PositionOrderId > 0)
                    {
                        var positionOrder = _positionRepository.GetSingle(poDto.PositionOrderId);
                        if (positionOrder != null)
                        {
                            positionOrder.OrderID = order.ID;
                            positionOrder.Pieces = poDto.Pieces != null ? poDto.Pieces.Trim() : null;
                            positionOrder.PositionNumber = poDto.PositionNumber;
                            positionOrder.Price = poDto.Price;
                            positionOrder.Text = poDto.Text != null ? poDto.Text.Trim() : null;
                            positionOrder.Total = poDto.Total;
                            order.TotalPrice += positionOrder.Total;
                            _positionRepository.Edit(positionOrder);
                        }
                    }
                    else
                    {
                        var positionOrder = new OrderPosition();
                        positionOrder.OrderID = order.ID;
                        positionOrder.Pieces = poDto.Pieces;
                        positionOrder.PositionNumber = poDto.PositionNumber;
                        positionOrder.Price = poDto.Price;
                        positionOrder.Text = poDto.Text;
                        positionOrder.Total = poDto.Total;
                        order.TotalPrice += positionOrder.Total;
                        _positionRepository.Add(positionOrder);
                    }
                }
                _ordersRepository.Edit(order);
                try
                {
                    _unitOfWork.Commit();
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            return 1;
        }



        public int DeleteOrder(int orderId)
        {
            if (orderId < 1) return -1;
            var order = _ordersRepository.GetSingle(orderId);
            _ordersRepository.Delete(order);
            _unitOfWork.Commit();
            return order.ID;
        }

        public OrderModel ReOrder(OrderModel o)
        {
            var order = new Order();
            order.CustomerNumber = o.CustomerNumber.Trim();
            order.DeliveryDate = null;
            order.OrderDate = DateTime.Now;
            order.TotalPrice = 0;
            _ordersRepository.Add(order);
            _unitOfWork.Commit();
            foreach (var OrderModel in o.ListPositionOrderDto)
            {
                var positionOrder = new OrderPosition();
                positionOrder.OrderID = order.ID;
                positionOrder.Pieces = OrderModel.Pieces != null ? OrderModel.Pieces.Trim() : null;
                positionOrder.PositionNumber = OrderModel.PositionNumber;
                positionOrder.Price = OrderModel.Price;
                positionOrder.Text = OrderModel.Text != null ? OrderModel.Text.Trim() : null;
                positionOrder.Total = OrderModel.Total;
                order.TotalPrice += positionOrder.Total;
                _positionRepository.Add(positionOrder);
            }
            _unitOfWork.Commit();
            return OrderById(
                order.ID.ToString(),
                DateTime.Now.TimeOfDay.ToString());
        }

        #endregion

        #region Position

        public int DeletePossitionOrder(List<int> listOrderId)
        {
            if (listOrderId == null) return -1;
            if (listOrderId.Count < 1) return -1;
            foreach (var id in listOrderId)
            {
                if (id < 1) continue;
                var positionOrder = _positionRepository.GetSingle(id);
                _positionRepository.Delete(positionOrder);

            }
            _unitOfWork.Commit();
            return 1;
        }

        public List<PositionOrderModel> GetPossitionList(
          string id,
          string key)
        {
            int orderId = 0;
            int.TryParse(id, out orderId);
            if (orderId < 1) return null;
            List<PositionOrderModel> list = null;
            var positionOrder =
                _positionRepository.FindBy(po => po.OrderID == orderId).OrderByDescending(o => o.PositionNumber);
            list = new List<PositionOrderModel>();
            foreach (var op in positionOrder)
            {
                var opDto = new PositionOrderModel();
                opDto.OrderId = op.OrderID;
                opDto.Pieces = op.Pieces;
                opDto.Text = op.Text;
                opDto.Price = op.Price;
                opDto.PositionNumber = op.PositionNumber;
                opDto.Total = op.Total;
                list.Add(opDto);
            }
            return list;
        } 

        #endregion

        #region Disposable
        /// <summary>
        /// Disponse
        /// </summary>
        public void Dispose()
        { 
            _dbFactory = null;
            _unitOfWork = null;
        }

        ~SampleConsulting()
        {
            Dispose();
        }

        #endregion
         
    }
}
