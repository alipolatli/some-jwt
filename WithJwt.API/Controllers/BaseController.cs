using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace WithJwt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IActionResult ActionResultInstance<T>(ResponseResult<T> responseResult) where T : class
        {
            return new ObjectResult(responseResult)
            {
                StatusCode = responseResult.StatusCode,
            };

        }
    }
}
