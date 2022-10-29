using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Util.Test.WCF.Basic2
{
    internal class Run2
    {
        public static void Run()
        {

            var service = new IPCHTTPService();

            Uri baseAddress = new Uri("http://localhost:8000");
            WebServiceHost selfHost = new WebServiceHost(typeof(IPCHTTPService), baseAddress);
            WebHttpBinding binding = new WebHttpBinding()
                {
                    TransferMode = TransferMode.Buffered,
                    MaxBufferSize = 2147483647,
                    MaxReceivedMessageSize = 2147483647,
                    MaxBufferPoolSize = 2147483647,
                    Security = { Mode = WebHttpSecurityMode.None }
                };

            try
            {
                selfHost.AddServiceEndpoint(typeof(IPCController), new WebHttpBinding(), baseAddress);

                //ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                //smb.HttpGetEnabled = true;
                //selfHost.Description.Behaviors.Add(smb);

                selfHost.Open();
                Console.WriteLine("The service is ready.");

                Console.ReadKey();
                selfHost.Close();
            } catch(CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
                Console.ReadKey();
            }
        }
    }
}
