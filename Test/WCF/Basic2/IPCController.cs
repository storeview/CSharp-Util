using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Util.Test.WCF.Basic2
{

    [ServiceContract(Name = "IPCHTTPService")]
    interface IPCController
    {
        [OperationContract]
        [WebInvoke(Method ="POST", UriTemplate = "/faceinfo", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Message FaceInfo(Stream requestBody);
    }
}
