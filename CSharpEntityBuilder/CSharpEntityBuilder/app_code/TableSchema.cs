using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpEntityBuilder
{
    public class TableSchema
    {

        public bool isPri { get; set; }
        public string column_name { get; set; }
        public string data_type { get; set; }
        public string csharp_data_type { get; set; }
        public string csharp_data_type2 { get; set; }

        public string  column_comment{get;set;}
        public string  column_key{get;set;}
        public string  EXTRA{get;set;}
        public string maxLength { get; set; }
        public bool  IS_NULLABLE{get;set;}
        public string  COLUMN_KEY{get;set;}
    }
}
