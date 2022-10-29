using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SpacedScreenshot.Util
{
    internal class XmlUtils
    {
        /// <summary>
        /// Serialize a T object to a xmlFile
        /// </summary>
        /// <typeparam name="T">Object's type</typeparam>
        /// <param name="obj">the object</param>
        /// <param name="xmlFilename">relative or absolute full filepath</param>
        public static void Serialize<T>(T obj, string xmlFilename)
        {
            if (obj == null) return;

            var writer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            var wfile = new System.IO.StreamWriter(xmlFilename);
            writer.Serialize(wfile, obj);
            wfile.Close();
        }


        /// <summary>
        /// Deserialize a xmlFile to T object
        /// </summary>
        /// <typeparam name="T">Object's typs</typeparam>
        /// <param name="xmlFilename">relative or absolute full filepath</param>
        /// <returns></returns>
        public static T Deserialize<T>(string xmlFilename)
        {
            if (!File.Exists(xmlFilename)) return default;

            var reader = new XmlSerializer(typeof(T));
            StreamReader file = new StreamReader(xmlFilename);
            T obj = (T)reader.Deserialize(file);
            file.Close();

            return obj;
        }
    }
}
