using CSharp_Util.Test;
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

            var dir = "D:\\Windows数据移动到此文件夹\\Desktop\\MQTTnet-4.1.0.247";
            var ret = FileUtil.FindExecutableProgramWithSameNameAsDirectory(dir);

            var curDir = Directory.GetCurrentDirectory();
            Console.WriteLine(ret);
            Console.WriteLine(dir);
            Console.WriteLine(ret.Replace(dir, ""));
            Console.WriteLine(curDir + ret.Replace(dir, ""));

            Console.ReadKey();
        }
    }
}
