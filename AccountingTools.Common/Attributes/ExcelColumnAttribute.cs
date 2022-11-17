namespace AccountingTools.Common.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class ExcelColumnAttribute : Attribute
    {
        private int columnNumber;
        private string columnFormat;
        public ExcelColumnAttribute(int columnNumber, string columnFormat = null)
        {
            this.columnNumber = columnNumber;
            this.columnFormat = columnFormat;
        }

        public int ColumnNumber { get { return columnNumber; } }

        public string ColumnFormat { get { return columnFormat; } }
    }
}
