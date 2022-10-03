namespace AccountingTools.Common.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class ExcelColumnAttribute : Attribute
    {
        private int columnNumber;
        public ExcelColumnAttribute(int columnNumber)
        {
            this.columnNumber = columnNumber;
        }

        public int ColumnNumber { get { return columnNumber; } }
    }
}
