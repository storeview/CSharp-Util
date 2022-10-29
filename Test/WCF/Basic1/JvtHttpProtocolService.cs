using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Util.Test.WCF.Basic1
{
    class JvtHttpProtocolService : IJvtHttpProtocol
    {
        public string FaceInfo(string request)
        {
            return "response";
        }
    }
}
