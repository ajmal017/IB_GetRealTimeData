using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB_GetRealTimeData
{
    class Program
    {
        static void Main(string[] args)
        {
            RealTimeData realTimeData = new RealTimeData();
            realTimeData.GetRealTimeData();
        }
    }
}
