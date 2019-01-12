using System.Collections.Generic;

namespace Reporter.Data
{
    public class Table
    {
        public Table() { }

        public string CornerHeader { get; set; }
        public IEnumerable<TableRow> Rows { get; set; }
        public IEnumerable<string> Columns { get; set; }
    }

    public class TableRow
    {
        public string HeaderRow { get; set; }
        public IEnumerable<object> Values { get; set; }
    }
}
