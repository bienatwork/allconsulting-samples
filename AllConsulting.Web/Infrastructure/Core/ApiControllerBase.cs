using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AllConsulting.Data.Infrastructure;
using AllConsulting.Data.Repositories;
using AllConsulting.Entities;

namespace AllConsulting.Web.Infrastructure.Core
{
    public class ApiControllerBase : ApiController
    {
        protected readonly IEntityBaseRepository<Error> ErrorsRepository;
        protected readonly IUnitOfWork UnitOfWork;

        public ApiControllerBase(IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork)
        {
            ErrorsRepository = errorsRepository;
            UnitOfWork = unitOfWork;
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;

            try
            {
                response = function.Invoke();
            }
            catch (DbUpdateException ex)
            {
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }
        private void LogError(Exception ex)
        {
            try
            {
                Error error = new Error()
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    DateCreated = DateTime.Now
                };

                ErrorsRepository.Add(error);
                UnitOfWork.Commit();
            }
            catch { }
        }
    }
}