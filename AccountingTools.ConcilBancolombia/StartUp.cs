using AccountingTools.ConcilBancolombia;
using AccountingTools.Repository;
using AccountingTools.Repository.Interface;
using AccountingTools.Service;
using AccountingTools.Service.Interface;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AccountingTools.ConcilBancolombia.StartUp))]
namespace AccountingTools.ConcilBancolombia
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            builder.Services.AddScoped<IBancolombiaService, BancolombiaService>();
            builder.Services.AddScoped<IPdfRepository, PdfRepository>();
            builder.Services.AddScoped<IExcelRepository, ExcelRepository>();
        }
    }
}
