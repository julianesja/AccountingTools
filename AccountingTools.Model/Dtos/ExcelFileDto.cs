namespace AccountingTools.Model.Dtos
{
    public class ExcelFileDto<T>
    {
        public string Name { get; set; }
        public List<T> Data { get; set; }
        public List<string> Headers { get; set; }
    }
}
