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
using Microsoft.EntityFrameworkCore;

namespace FunctionApp1
{
    public static class Function1
    {
        //public static List<Session> SessionStore = new List<Session>();

        [FunctionName("Sessionize")]
        public static async Task<IActionResult> Sessions(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Sessions")] HttpRequest req,
            ILogger log)
        {
            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                Description = "Default description",
                Title = "Default itle",
                Duration = 75
            };

            using (var context = new DataContext())
            {
                var sessions = await context.Sessions.ToListAsync();

                return new OkObjectResult(sessions);
            }
            
        }

        [FunctionName("Create")]
        public static async Task<IActionResult> Create(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Create")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Session data = JsonConvert.DeserializeObject<Session>(requestBody);

            data.Id = Guid.NewGuid();


            using (var context = new DataContext())
            {
                context.Sessions.Add(data);
                await context.SaveChangesAsync();
                return new OkObjectResult(data);
            }
        }
    }
}
