using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AllConsulting.Data.Infrastructure;
using AllConsulting.Data.Repositories;
using AllConsulting.Entities;
using AllConsulting.Web.Infrastructure.Core;
using AllConsulting.Web.Infrastructure.Extensions;
using AllConsulting.Web.Models;
using AutoMapper;

namespace AllConsulting.Web.API
{
    [RoutePrefix("api/v1/orders")]
    public class OrdersController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Order> _ordersRepository;
        public OrdersController(IEntityBaseRepository<Order> ordersRepository, IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork)
            : base(errorsRepository, unitOfWork)
        {
            _ordersRepository = ordersRepository;
        }

        [Route("{page:int=0}/{pageSize=15}/{filter?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page ?? 1;
            int currentPageSize = pageSize ?? 1;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Order> orderList = null;
                int totalOrders = 0;

                if (!string.IsNullOrEmpty(filter))
                {
                    orderList = _ordersRepository
                        .FindBy(x => x.CustomerNumber.Contains(filter))
                        .OrderBy(x => x.ID)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalOrders = _ordersRepository
                        .FindBy(x => x.CustomerNumber.Contains(filter))
                        .Count();
                }
                else
                {
                    orderList = _ordersRepository.GetAll()
                       .OrderBy(x => x.ID)
                       .Skip(currentPage * currentPageSize)
                       .Take(currentPageSize)
                       .ToList();

                    totalOrders = _ordersRepository.GetAll()
                        .Count();
                }

                IEnumerable<OrderViewModel> viewModel = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(orderList);
                var pagedSet = new PaginationSet<OrderViewModel>
                {
                    Page = currentPage,
                    TotalCount = totalOrders,
                    TotalPages = (int)Math.Ceiling((decimal)totalOrders / currentPageSize),
                    Items = viewModel
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);
                return response;
            });
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, OrderViewModel order)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    return response;
                }

                if (order.OrderPositions == null || order.OrderPositions.Count < 1)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    return response;
                }

                Order orderEntity = new Order();
                orderEntity.UpdateOrder(order);

                _ordersRepository.Add(orderEntity);
                UnitOfWork.Commit();

                // Update the view model
                order = Mapper.Map<Order, OrderViewModel>(orderEntity);
                response = request.CreateResponse(HttpStatusCode.Created, order);

                return response;
            });
        }

    }
}
