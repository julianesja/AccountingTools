using AccountingTools.Model.Dtos;
using AccountingTools.Repository.Interface;
using ClosedXML.Excel;
using IronXL;
using System.Reflection;

namespace AccountingTools.Repository
{
    public class ExcelRepository : IExcelRepository
    {
        public Stream CreateExcel<T>(List<ExcelFileDto<T>> sheets)
        {
            using (var workbook = new XLWorkbook())
            {
                foreach (var sheet in sheets)
                {
                    IXLWorksheet excelSheet = workbook.Worksheets.Add(sheet.Name);
                    // Set headers
                    int cell = 1;
                    int row = 1;
                    foreach (var header in sheet.Headers)
                    {

                        excelSheet.Cell(row, cell).Style.Font.SetBold(true);
                        excelSheet.Cell(row, cell).SetDataType(XLDataType.Text);
                        excelSheet.Cell(row, cell).Value = header;
                        cell++;
                    }
                    row++;

                    foreach (T rowValue in sheet.Data)
                    {
                        cell = 1;
                        foreach (PropertyInfo prop in typeof(T).GetProperties())
                        {
                            excelSheet.Cell(row, cell).SetDataType(GetTypeCell(prop.PropertyType));
                            excelSheet.Cell(row, cell).Value = prop.GetValue(rowValue);
                            cell++;
                        }
                        row++;
                    }

                }
                var ms = new MemoryStream();
                workbook.SaveAs(ms);
                return ms;

            }
            return null;
        }

        private XLDataType GetTypeCell(Type type)
        {
            if (type == typeof(string))
                return XLDataType.Text;
            else if (type == typeof(int))
                return XLDataType.Number;
            else if (type == typeof(DateTime))
                return XLDataType.DateTime;
            else
                return XLDataType.Text;

        }
    }
}
