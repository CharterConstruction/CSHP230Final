using Microsoft.AspNetCore.Mvc;


namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error/{code}")]
        [HttpGet]
        public IActionResult Error(int code)
          => new ObjectResult(new ApiResponse(code));
        
    }


}
