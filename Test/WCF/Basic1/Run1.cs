using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Util.Test.WCF.Basic1
{
    internal class Run1
    {
        public static void Run()
        {
            Uri baseAddress = new Uri("http://localhost:8000/JvtHttpProtocol");
            ServiceHost selfHost = new ServiceHost(typeof(JvtHttpProtocolService), baseAddress);

            try
            {
                selfHost.AddServiceEndpoint(typeof(IJvtHttpProtocol), new WSHttpBinding(), "JvtHttpProtocolService");

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb);

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
