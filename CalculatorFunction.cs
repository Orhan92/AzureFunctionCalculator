using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LocalFunctionProj
{
    public static class CalculatorFunction
    {
        [FunctionName("CalculatorFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try 
            {
                string name = req.Query["name"];
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                name = name ?? data?.name;

                double firstValue = 0, secondValue = 0;

                firstValue = Convert.ToDouble(req.Query["firstValue"]);
                secondValue = Convert.ToDouble(req.Query["secondValue"]);

                var sum = firstValue + secondValue;
                string responseMessage = $"Name: {name}\nFirst Value: {firstValue}\nSecond Value: {secondValue}\nTotal sum: {firstValue} + {secondValue} = {sum}";

                return new OkObjectResult(responseMessage);
            }

            catch 
            {
                return new BadRequestObjectResult("Invalid calculations. Try again!");
            }
        }
    }
}
