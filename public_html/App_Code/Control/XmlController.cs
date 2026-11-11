using System;
using System.IO;
using System.Xml.Serialization;

namespace Control
{
    public class XmlController
    {
        public T Deserialize<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return default(T);
            }
            try
            {
                var reader = new StringReader(xml);
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
            catch(Exception ex)
            {
                var ctrl = new SysExceptionController();
                ctrl.OnException(ex, 0);
                return default(T);
            }
        }
    }
}