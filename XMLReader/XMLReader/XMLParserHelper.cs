using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Linq;

namespace XMLReader
{
    public static class XMLParserHelper
    {
        public static T GetTChild<T>(XElement element, string name, Func<string, T> parse)
        {
            XElement childElement = element
                .Elements()
                .FirstOrDefault((child) => child.Name.LocalName.Equals(name));

            return parse(childElement?.Value?.ToString());
        }
        
        public static string GetStringChild(XElement element, string name)
        {
            return GetTChild<string>(element, name, (child) => child?.Trim());
        }

        public static T GetTAttribute<T>(XElement element, string name, Func<string, T> parse)
        {
            XAttribute attr = element
                .Attributes()
                .FirstOrDefault((child) => child.Name.LocalName.Equals(name));

            return parse(attr?.Value?.ToString());
        }

        public static string GetStringAttribute(XElement element, string name)
        {
            return GetTAttribute<string>(element, name, (attr) => attr?.Trim());
        }
    }
}
