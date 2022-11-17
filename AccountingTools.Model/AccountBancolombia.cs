using AccountingTools.Common.Attributes;

namespace AccountingTools.Model
{
    public class AccountBancolombia
    {
        [ExcelColumn(1)]
        public string Date { get; set; }
        [ExcelColumn(2)]
        public string Description { get; set; }
        
        [ExcelColumn(3, columnFormat: "$ #,##0.00")]
        public double Value { get; set; }
    }
}
