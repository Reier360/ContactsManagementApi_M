using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Helpers
{
    public class DatatablePagination
    {
        public int skip { get; set; }
        public int take { get; set; }
        public string orderColumn { get; set; }
        public string ascDesc { get; set; }
    }
}
