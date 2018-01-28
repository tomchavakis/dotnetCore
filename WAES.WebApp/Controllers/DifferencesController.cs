using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Model;
using Newtonsoft.Json;
using WAES.BitsConverter;
using WAES.Model;
using WAYS.Cryptography;

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

        [HttpPost]
        [Route("id")]
        public IActionResult GetResultFromDiffs(int id, ComparisonResult result)
        {
            return Ok(result);
        }

        [HttpPost]
        [Route("{id}/left")]
        public IActionResult Left(int id, MessageBinding model)
        {
            _logger.LogDebug(string.Format("(Left-id:{0},message:{1})", id,
                JsonConvert.SerializeObject(model, Formatting.Indented)));

            if (id > 0 && model != null && !string.IsNullOrEmpty(model.Payload) && Methods.IsValidBase64(model.Payload))
            {
                using (var db = new DatabaseContext())
                {
                    List<Message> messages = db.Messages.Where(i => i.MessageId == id).ToList();

                    if (messages.Count > 0)
                    {
                        Message dbMessage = messages.LastOrDefault();

                        byte[] Base64ToByteArrayOfDatabase = Methods.DecodeBase64ToByteArray(dbMessage.Payload);
                        byte[] Base64ToByteArrayOfModel = Methods.DecodeBase64ToByteArray(model.Payload);

                       
                        ComparisonResult res = BitsDiff.CompareByteArrays(Base64ToByteArrayOfDatabase, Base64ToByteArrayOfModel);

                        _logger.LogDebug(string.Format("(Left-id:{0},message:{1})", id,
                            JsonConvert.SerializeObject(res, Formatting.Indented)));

                        return Ok(res);
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

                            _logger.LogDebug(string.Format("(Left:{0})", message));
                            
                            return Ok($"Message with ID: {id} does not exist");
                        }
                        catch (Exception e)
                        {
                            _logger.LogError(string.Format("(Left:{0})", e));
                        }
                    }
                }
            }

            _logger.LogDebug(string.Format("(Left:BadRequest)"));
            return BadRequest();
        }

        [HttpPost]
        [Route("{id}/right")]
        public IActionResult Right(int id, MessageBinding message)
        {
            _logger.LogDebug(string.Format("(Right-id:{0},message:{1})", id,
                JsonConvert.SerializeObject(message, Formatting.Indented)));

            return Ok();
        }
    }
}