using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Util.Test
{
    internal class MyTest
    {
        private static List<string> s_targetFileExtension = new List<string> { ".exe", ".bat" };
        private static string s_targetFilename = "";
        private static bool s_findTargetFile;

        public static void testFindExecuableProgramInDirectoryWithSameName(string directoryPath)
        {
            testFindExecuableProgramInDirectoryWithSameName(directoryPath, null);
        }

        public static void testFindExecuableProgramInDirectoryWithSameName(string directoryPath, List<string> targetFileExtension)
        {
            s_targetFilename = Path.GetFileName(directoryPath);

            if (targetFileExtension == null)
                targetFileExtension = s_targetFileExtension;

            recur(directoryPath);
        }

        private static void recur(string path)
        {
            if (s_findTargetFile)
                return;

            foreach (string file in Directory.GetFiles(path))
            {
                var filename = Path.GetFileNameWithoutExtension(file);
                if (s_targetFilename.Equals(filename))
                {
                    var extension = Path.GetExtension(file);
                    if (s_targetFileExtension.IndexOf(extension) != -1)
                    {
                        s_findTargetFile = true;
                        return;
                    }
                }
            }

            foreach (string dir in Directory.GetDirectories(path))
            {
                recur(dir);
            }

        }
    }
}
