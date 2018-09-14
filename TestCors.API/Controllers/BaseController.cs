using Microsoft.AspNetCore.Mvc;
using System.Net;
using TestCors.Common;
using TestCors.Common.Enums;

namespace TestCors.API.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult ReturnResult(ServiceResult serviceResult)
        {
            if (serviceResult.Status == EServiceResultStatus.Success)
            {
                return StatusCode((int)HttpStatusCode.NoContent);
            }

            if (serviceResult.Status == EServiceResultStatus.ServerError)
            {
                var message = string.Empty;
#if DEBUG
                message = serviceResult.Message;
#endif
                return StatusCode((int)HttpStatusCode.InternalServerError, message);
            }

            return serviceResult.Status == EServiceResultStatus.Error
                ? BadRequest(serviceResult.Message)
                : Forbid() as IActionResult;
        }

        protected IActionResult ReturnResult<T>(ServiceResult<T> serviceResult)
        {
            if (serviceResult.Status == EServiceResultStatus.Success)
            {
                return Ok(serviceResult.Entity);
            }

            if (serviceResult.Status == EServiceResultStatus.ServerError)
            {
                var message = string.Empty;
#if DEBUG
                message = serviceResult.Message;
#endif
                return StatusCode((int)HttpStatusCode.InternalServerError, message);
            }

            return serviceResult.Status == EServiceResultStatus.Error
                ? BadRequest(serviceResult.Message)
                : Forbid() as IActionResult;
        }
    }
}
