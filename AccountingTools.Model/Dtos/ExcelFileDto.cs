using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingTools.Model.Dtos
{
    public class ExcelFileDto<T>
    {
        public string Name { get; set; }
        public List<T> Data { get; set; }
        public List<string> Headers { get; set; }
    }
}
