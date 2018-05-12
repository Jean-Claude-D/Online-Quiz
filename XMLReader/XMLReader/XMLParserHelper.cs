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
        public static T getTChild<T>(XElement element, string name, Func<string, T> parse)
        {
            XElement childElement = element
                .Elements()
                .FirstOrDefault((child) => child.Name.LocalName.Equals(name));

            return parse(childElement?.Value?.ToString());
        }

        public static string getStringChild(XElement element, string name)
        {
            return getTChild<string>(element, name, (child) => child);
        }
    }
}
