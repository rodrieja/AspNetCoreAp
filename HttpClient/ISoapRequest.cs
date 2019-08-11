using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace HttpSoapClient
{
    internal interface ISoapRequest
    {
        XDocument CreateRequest();
        void LoadParameters(Dictionary<string, string> parameters);
    }
}
