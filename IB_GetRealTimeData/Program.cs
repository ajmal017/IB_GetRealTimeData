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
            IBGatewayClientConnectionData iBGatewayClientConnectionData = new IBGatewayClientConnectionData("", 4002, 0); // todo - get from db
            RealTimeData realTimeData = new RealTimeData(iBGatewayClientConnectionData);
            realTimeData.GetRealTimeData();
        }
    }
}
