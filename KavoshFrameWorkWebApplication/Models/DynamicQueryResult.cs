using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KavoshFrameWorkWebApplication.Models
{
    public class DynamicQueryResult
    {
        public IEnumerable Data { get; set; }
        public long Total { get; set; }
        public object Aggregates { get; set; }
        public IList<string> Errors { get; set; }
    }
}
