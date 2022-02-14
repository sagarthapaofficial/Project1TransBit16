using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Project1TransBit16
{
    public class gs
    {
        public gs()
        {
            groups = new List<CityInfo>();
        }
        public List<CityInfo> groups { get; set; }
    }
}
