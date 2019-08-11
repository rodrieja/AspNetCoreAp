using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace HttpSoapClient
{
    public interface ICtsBodyRequest
    {
        string BodyNamespaceName { get; set; }
        XNamespace BodyNamespace { get; set; }
        string BodyType { get; set; }


        string RequestConnectionNamespaceName { get; set; }
        XNamespace RequestConnectionNamespace { get; set; }
        string RequestConnectionFieldNamespaceName { get; set; }
        XNamespace RequestConnectionFieldNamespace { get; set; }


        string FilNamespaceName { get; set; }
        XNamespace FilNamespace { get; set; }
        string FilType { get; set; }


        string SubFilNamespaceName { get; set; }
        XNamespace SubFilNamespace { get; set; }
        string SubFilType { get; set; }


        string FilFieldNamespaceName { get; set; }
        XNamespace FilFieldNamespace { get; set; }


        List<XElement> GetInputParamElements();
    }
}
