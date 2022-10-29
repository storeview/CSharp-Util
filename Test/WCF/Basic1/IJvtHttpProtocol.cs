using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Util.Test.WCF.Basic1
{
    /// <summary>
    /// WCF 实践
    ///     1. 定义 construct
    ///     2. 定义 operation
    ///     3. 实现 service
    ///     4. 开启服务
    /// </summary>
    [ServiceContract]
    public interface IJvtHttpProtocol
    {
        [OperationContract]
        string FaceInfo(string request);
    }
}
