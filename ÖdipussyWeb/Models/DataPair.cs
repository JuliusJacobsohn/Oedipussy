using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÖdipussyWeb.Models
{
    public class DataPair
    {
        public string Data { get; set; }
        public Type Type { get; set; }
        public string TransformationLog { get; set; }
        public bool IsTransformed { get; set; }
        public override string ToString()
        {
            return Data;
            //return $"{Data} ({Type})";
        }
    }
}
