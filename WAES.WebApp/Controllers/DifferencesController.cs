using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApp.Model;
using Newtonsoft.Json;
using WAES.BitsConverter;
using WAES.Client;
using WAES.Model;
using WAYS.Cryptography;

namespace WebApp.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/diff")]
    public class DifferencesController : Controller
    {
        ILogger<DifferencesController> _logger;
        public IConfiguration Configuration { get; }

        public DifferencesController(ILogger<DifferencesController> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }


        /// <summary>
        /// HTTP Endpoint that returns the base64 encoded binary differences
        /// </summary>
        /// <param name="id">ID Of the Message</param>
        /// <param name="model">ComparisonResult model</param>
        /// <returns>ComparisonResult</returns>
        [HttpPost]
        [Route("{id}")]
        public IActionResult GetResultFromDiffs(int id, [FromBody] ComparisonResult model)
        {
            _logger.LogDebug(string.Format("(GetResultFromDiffs-id:{0},model:{1})", id,
                JsonConvert.SerializeObject(model, Formatting.Indented)));

            if (model != null)
                return Ok(model);

            return BadRequest();
        }

        /// <summary>
        /// HTTP Endpoint that accepts a base64 encoded binary data and compares them from the same of the database
        /// </summary>
        /// <param name="id">ID Of the Message</param>
        /// <param name="model">MessageBinding Model</param>
        /// <returns>MessageResponse</returns>
        [HttpPost]
        [Route("{id}/left")]
        public IActionResult Left(int id, [FromBody] MessageBinding model)
        {
            return Common(id, model, "Right");
        }

        /// <summary>
        /// HTTP Endpoint that accepts a base64 encoded binary data and compares them from the same of the database
        /// </summary>
        /// <param name="id">ID Of the Message</param>
        /// <param name="model">MessageBinding Model</param>
        /// <returns>MessageResponse</returns>
        [HttpPost]
        [Route("{id}/right")]
        public IActionResult Right(int id, [FromBody] MessageBinding model)
        {
            return Common(id, model, "Right");
        }

        [ApiExplorerSettings(IgnoreApi=true)]
        [HttpGet]
        public IActionResult Common(int id, MessageBinding model, string method)
        {
            _logger.LogDebug(string.Format("({0}-id:{1},model:{2})", method, id,
                JsonConvert.SerializeObject(model, Formatting.Indented)));

            if (id > 0 && model != null && !string.IsNullOrEmpty(model.Payload) && Methods.IsValidBase64(model.Payload))
            {
                using (var db = new DatabaseContext())
                {
                    List<Message> messages = db.Messages.Where(i => i.MessageId == id).ToList();

                    if (messages.Count > 0)
                    {
                        Message dbMessage = messages[messages.Count-1];
                        
//                        _logger.LogDebug(string.Format("{0}", JsonConvert.SerializeObject(dbMessage, Formatting.Indented)));
//                        
                        byte[] Base64ToByteArrayOfDatabase = Methods.DecodeBase64ToByteArray(dbMessage.Payload);
                        byte[] Base64ToByteArrayOfModel = Methods.DecodeBase64ToByteArray(model.Payload);

                      
                        ComparisonResult res = BitsDiff.CompareByteArrays(Base64ToByteArrayOfDatabase, Base64ToByteArrayOfModel);
                      

                        string middleEndpointUrl = Configuration["EndPoints:middle"];
                        int v = Convert.ToInt32(HttpContext.GetRequestedApiVersion().ToString());
                       
                        var resultFromMiddleEndPoint = DifferencesClient.Middle(middleEndpointUrl, v, id, res).Result;

                        _logger.LogDebug(string.Format("({0} id:{1},message:{2})", method, id,
                            JsonConvert.SerializeObject(resultFromMiddleEndPoint, Formatting.Indented)));

                        return Ok(resultFromMiddleEndPoint);
                    }
                    else
                    {
                        try
                        {
                            Message message = new Message()
                            {
                                MessageId = id,
                                Payload = model.Payload
                            };

                            db.Messages.Add(message);
                            db.SaveChanges();

                            _logger.LogDebug(string.Format(
                                "({0}:{1}, Message with ID: {2} does not exist and added to the database.", method,
                                message,
                                id));

                            return Ok();
                        }
                        catch (Exception e)
                        {
                            _logger.LogError(string.Format("({0}:{1})", method, e));
                            return BadRequest();
                        }
                    }
                }
            }

            _logger.LogDebug(string.Format("({0}:BadRequest)", method));
            return BadRequest();
        }
    }
}