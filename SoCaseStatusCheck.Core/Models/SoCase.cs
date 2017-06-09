using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoCaseStatusCheck.Core.Models
{
    public class SoCase
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public CaseStatue Statue { get; set; }
    }
    public enum CaseStatue
    {
        Delete = 0,
        Normal,
    }
}
