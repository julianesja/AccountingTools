using AccountingTools.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AccountingTools.ConcilBancolombia
{
    public  class ConcilBancolombiaFunction
    {
        private readonly IBancolombiaService bancolombiaService;

        public ConcilBancolombiaFunction(IBancolombiaService bancolombiaService)
        {
            this.bancolombiaService = bancolombiaService;
        }

        [FunctionName("ConcilBancolombiaFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");
            
            IFormFile extracto = req.Form.Files["extracto"];
            string l = req.Form["extracto"];
            string password = req.Form.ContainsKey("password") ? req.Form["password"].ToString() : null ;
            
            using (var ms = new MemoryStream())
            {
                try
                {
                    extracto.CopyTo(ms);
                    Stream excel = bancolombiaService.CreateConcilBancolombia(ms, password);
                    excel.Position = 0;
                    return new FileStreamResult(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { FileDownloadName = $"{Guid.NewGuid().ToString()}.xlsx"};

                }
                catch (Exception ex)
                {

                }

            }





            return new OkObjectResult("");
        }
    }
}
