using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FunctionApp1
{
    public static class Function1
    {
        public static List<Session> SessionStore = new List<Session>();

        [FunctionName("Sessionize")]
        public static IActionResult Sessions(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Sessions")] HttpRequest req,
            ILogger log)
        {

            return new OkObjectResult(SessionStore);
        }

        [FunctionName("Create")]
        public static async Task<IActionResult> Create(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Create")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Session data = JsonConvert.DeserializeObject<Session>(requestBody);

            data.Id = Guid.NewGuid();
            SessionStore.Add(data);

            return new OkObjectResult(data);
        }
    }
}
