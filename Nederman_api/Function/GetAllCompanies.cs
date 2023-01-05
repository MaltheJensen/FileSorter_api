using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Nederman_api.Model;
using Nederman_api.Controller;
using System.Collections.Generic;

namespace Nederman_api.Function
{
    public static class GetAllCompanies
    {
        [FunctionName("GetAllCompanies")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get",  Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            var company = GetCompanies();

            string responseMessage = JsonConvert.SerializeObject(company);

            return new OkObjectResult(responseMessage);
        }
        private static List<Company> GetCompanies()
        {
            List<Company> company = new List<Company>();
            CompanyController cc = new CompanyController();

            company = cc.GetAllCompanies();

            return company;

        }
    }
}
