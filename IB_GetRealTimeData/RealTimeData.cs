﻿using System;
using System.Collections.Generic;
using System.Text;
using IBApi;
using System.Threading;

namespace IB_GetRealTimeData
{
    class RealTimeData
    {
        public void GetRealTimeData()
        {
            // Create the ibClient object to represent the connection
            // If you changed the samples Namespace name, use your new 
            // name here in place of "Samples".
            EWrapperImpl ibClient = new EWrapperImpl();

            ibClient.ClientSocket.eConnect("", 4002, 0);

            var reader = new EReader(ibClient.ClientSocket, ibClient.Signal);
            reader.Start();
            new Thread(() => {
                while (ibClient.ClientSocket.IsConnected())
                {
                    ibClient.Signal.waitForSignal();
                    reader.processMsgs();
                }
            })
            { IsBackground = true }.Start();

            // Pause here until the connection is complete 
            while (ibClient.NextOrderId <= 0) { }

            // Create a new contract to specify the security we are searching for
            Contract contract = new Contract();
            contract.Symbol = "IBM";
            contract.SecType = "STK";
            contract.Exchange = "SMART";
            contract.Currency = "USD";
            // Create a new TagValue List object (for API version 9.71) 
            List<TagValue> historicalDataOptions = new List<TagValue>();

            // Kick off the request for market data for this
            // contract.  reqMktData Parameters are:
            // tickerId           - A unique id to represent this request
            // contract           - The contract object specifying the financial instrument
            // genericTickList    - A string representing special tick values
            // snapshot           - When true obtains only the latest price tick
            //                      When false, obtains data in a stream
            // regulatory snapshot - API version 9.72 and higher. Remove for earlier versions of API
            // mktDataOptions   - TagValueList of options 
            //ibClient.ClientSocket.reqMktData(1, contract, "", false, false, mktDataOptions);

            //for (int i=0; i < 4; i++)
            //{
            //    ibClient.ClientSocket.reqMktData(i, contract, string.Empty, false, false, null);
            //}

            ibClient.ClientSocket.reqMktData(1, contract, string.Empty, false, false, null);


            // Pause so we can view the output
            Console.ReadKey();

            // Cancel the subscription/request. Parameter is:
            // tickerId         - A unique id to represent the request 
            ibClient.ClientSocket.cancelMktData(1);

            // Disconnect from TWS
            ibClient.ClientSocket.eDisconnect();

        }
    }
}



