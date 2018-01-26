using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Model;
using Newtonsoft.Json;

namespace WebApp.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/diff")]
    public class DifferencesController : Controller
    {
        ILogger<DifferencesController> _logger;

        public DifferencesController(ILogger<DifferencesController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// http endpoints that accepts JSON base64 encoded binary data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("left")]
        public IActionResult Left()
        {
            using (var db = new DatabaseContext())
            {
                var blogs = db.Blogs.ToList();
                _logger.LogDebug(string.Format("Left({0})", JsonConvert.SerializeObject(blogs, Formatting.Indented)));

                return Ok(blogs);
            }

            _logger.LogInformation("Left");

            return Ok();
        }

        [HttpGet]
        [Route("right")]
        public IActionResult Right()
        {
            _logger.LogInformation("Right");

            return Ok();
        }
    }
}