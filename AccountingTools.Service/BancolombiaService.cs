using AccountingTools.Model;
using AccountingTools.Model.Dtos;
using AccountingTools.Repository.Interface;
using AccountingTools.Service.Interface;

namespace AccountingTools.Service
{
    public class BancolombiaService : IBancolombiaService
    {
        private readonly IPdfRepository pdfRepository;
        private readonly IExcelRepository excelRepository;

        public BancolombiaService(IPdfRepository pdfRepository, IExcelRepository excelRepository)
        {
            this.pdfRepository = pdfRepository;
            this.excelRepository = excelRepository;
        }

        public Stream CreateConcilBancolombia(MemoryStream pdf, string password = null)
        {
            List<string> pages = pdfRepository.RedPdf(pdf, password);
            List<AccountBancolombia> concilValues = new List<AccountBancolombia>();
            foreach (string page in pages)
            {
                List<string> arrayText = page.Split("\n").ToList();
                string strDate = arrayText[1].Split(" ")[1];
                DateTime date = DateTime.Parse(strDate);

                int index = arrayText.IndexOf("FECHA DESCRIPCIÓN SUCURSAL DCTO. VALOR SALDO");
                List<string> values = arrayText.GetRange(index + 1, arrayText.Count - (index + 3));
                values.Remove("FIN ESTADO DE CUENTA");
                foreach (var value in values)
                {
                    List<string> arrayValue = value.Split(" ").ToList();
                    if (arrayValue.Count >= 3)
                    {
                        AccountBancolombia accountBancolombia = new AccountBancolombia
                        {
                            Date = $"{arrayValue[0]}/{date.Year}",
                            Description = String.Join(" ", arrayValue.GetRange(1, arrayValue.Count - 3).ToArray()),
                            Value = Convert.ToDouble(arrayValue[arrayValue.Count - 2])
                        };
                        concilValues.Add(accountBancolombia);
                    }
                }
            }

            List<ExcelFileDto<AccountBancolombia>> excelFileDtos = this.CreateConcilBancolombiaSheet(concilValues);
            return excelRepository.CreateExcel(excelFileDtos);

        }

        private List<ExcelFileDto<AccountBancolombia>> CreateConcilBancolombiaSheet(List<AccountBancolombia> concilValues)
        {
            List<ExcelFileDto<AccountBancolombia>> excelFileDtos = new List<ExcelFileDto<AccountBancolombia>>();
            List<string> headers = new List<string> { "FECHA", "DESCRIPCIÓN", "VALOR" };
            excelFileDtos.Add(new ExcelFileDto<AccountBancolombia>
            {
                Headers = headers,
                Name = "General",
                Data = concilValues
            });

            excelFileDtos.Add(new ExcelFileDto<AccountBancolombia>
            {
                Headers = headers,
                Name = "Ordenado",
                Data = concilValues.OrderBy(a => a.Description).ToList()
            });

            excelFileDtos.Add(new ExcelFileDto<AccountBancolombia>
            {
                Headers = headers,
                Name = "Ingresos",
                Data = concilValues.Where(c => c.Value > 0).OrderBy(a => a.Description).ToList()
            });

            excelFileDtos.Add(new ExcelFileDto<AccountBancolombia>
            {
                Headers = headers,
                Name = "Salidas",
                Data = concilValues.Where(c => c.Value <= 0).OrderBy(a => a.Description).ToList()
            });

            excelFileDtos.Add(new ExcelFileDto<AccountBancolombia>
            {
                Headers = headers,
                Name = "Ingresos agrupadas",
                Data = concilValues.Where(c => c.Value > 0).GroupBy(c => c.Description)
                .Select(c => new AccountBancolombia
                {
                    Date = String.Empty,
                    Description = c.First().Description,
                    Value = c.Sum(v=> v.Value)
                }).OrderBy(a => a.Description).ToList()
            });

            excelFileDtos.Add(new ExcelFileDto<AccountBancolombia>
            {
                Headers = headers,
                Name = "Salidas agrupadas",
                Data = concilValues.Where(c => c.Value <= 0).GroupBy(c => c.Description)
                .Select(c => new AccountBancolombia
                {
                    Date = String.Empty,
                    Description = c.First().Description,
                    Value = c.Sum(v => v.Value)
                }).OrderBy(a => a.Description).ToList()
            });

            return excelFileDtos;
        }

    }
}
