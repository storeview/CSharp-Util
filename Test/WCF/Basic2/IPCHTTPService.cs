using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Util.Test.WCF.Basic2
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    class IPCHTTPService : IPCController
    {
        public Message FaceInfo(Stream requestBody)
        {
            var reqBody = new StreamReader(requestBody).ReadToEnd().Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("    ", "").Replace("  ", "");

            Console.WriteLine(reqBody);

            var root = new
            {
                Name = "captureInfoResponse",
                Code = 1,
                Message = "Success"
            };

            var context = WebOperationContext.Current;
            HttpResponseHeader header = HttpResponseHeader.ContentType;
            context.OutgoingResponse.Headers.Add(header, "application/json;charset=utf-8");

            return context.CreateTextResponse(JsonConvert.SerializeObject(root));
        }
    }
}
