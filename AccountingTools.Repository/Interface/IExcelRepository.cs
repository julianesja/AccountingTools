using AccountingTools.Model.Dtos;

namespace AccountingTools.Repository.Interface
{
    public interface IExcelRepository
    {
        Stream CreateExcel<T>(List<ExcelFileDto<T>> sheets);
    }
}
