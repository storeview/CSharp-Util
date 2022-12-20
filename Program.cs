using CSharp_Util.Test;
using CSharp_Util.Test.MqttTestServer;
using CSharp_Util.Test.WCF.Basic1;
using CSharp_Util.Test.WCF.Basic2;
using CSharp_Util.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Util
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MqttTest1.Run();
        }
    }
}
