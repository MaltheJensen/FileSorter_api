using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Nederman_api.Dto;
using Nederman_api.Controller;

namespace Nederman_api.Function
{
    public static class SaveNewReport
    {
        [FunctionName("SaveNewReport")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            NewReportDto data = JsonConvert.DeserializeObject<NewReportDto>(requestBody);

            SaveReport(data);

            return new OkObjectResult(data);
        }

        private static void SaveReport(NewReportDto report) 
        {
            CompanyController cc = new CompanyController();

            cc.SaveNewReport(report);
        }
    }
}
