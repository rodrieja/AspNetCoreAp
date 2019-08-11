using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace HttpSoapClient
{
    class SoapRequest<T> : ISoapRequest where T : ICtsBodyRequest
    {
        T input;

        // Envelope
        private XNamespace bodyNamespace = "";
        private XNamespace requestConnectionNamespace = "";
        private XNamespace requestConnectionFieldNamespace = "";
        private XNamespace filNamespace = "";
        private XNamespace subFilNamespace = "";
        private XNamespace filFieldNamespace = "";
        // Generics
        private readonly XNamespace ns = "http://schemas.xmlsoap.org/soap/envelope/";
        private readonly XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
        private readonly XNamespace xsd = "http://www.w3.org/2001/XMLSchema";
        //WS-Security
        private readonly XNamespace wsse = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";
        private readonly string PasswordType = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText";

        Dictionary<string, string> parameters = new Dictionary<string, string>();

        public SoapRequest(object input)
        {
            this.input = (T)input;
        }

        private XElement CreateBody()
        {
            XElement body;
            XElement bodyElements;
            XElement requestConnection;
            XElement fil;
            XElement subFil;

            body = new XElement(ns + "Body");
            bodyElements = new XElement(bodyNamespace + input.BodyType);

            string user = parameters.ContainsKey("requestConnection-user") ? parameters["requestConnection-user"] : "externo";
            string applicationID = parameters.ContainsKey("requestConnection-applicationID") ? parameters["requestConnection-applicationID"] : "1";

            requestConnection = new XElement(requestConnectionNamespace + "requestConnection",
                                    new XElement(requestConnectionFieldNamespace + "user", user),
                                    new XElement(requestConnectionFieldNamespace + "password", "?"),
                                    new XElement(requestConnectionFieldNamespace + "applicationID", applicationID),
                                    new XElement(requestConnectionFieldNamespace + "sessionID", "?")
                                );

            fil = new XElement(filNamespace + input.FilType);

            if (!string.IsNullOrEmpty(input.SubFilType))
            {
                subFil = new XElement(subFilNamespace + input.SubFilType);

                foreach (XElement inparam in input.GetInputParamElements())
                {
                    subFil.Add(inparam);
                }

                fil.Add(subFil);
            }
            else
            {
                foreach (XElement inparam in input.GetInputParamElements())
                {
                    fil.Add(inparam);
                }
            }

            bodyElements.Add(requestConnection);
            bodyElements.Add(fil);

            body.Add(bodyElements);

            return body;
        }

        private XElement CreateHeader()
        {
            XElement header;

            string _Username = parameters.ContainsKey("WsSecurity-Username") ? parameters["WsSecurity-Username"] : "?";
            string _mustUnderstand = parameters.ContainsKey("WsSecurity-mustUnderstand") ? parameters["WsSecurity-mustUnderstand"] : "1";

            header = new XElement(ns + "Header",
                        new XAttribute(XNamespace.Xmlns + "wsse", wsse),
                        new XElement(wsse + "Security",
                            new XAttribute(ns + "mustUnderstand", _mustUnderstand),
                            new XElement(wsse + "UsernameToken",
                                new XElement(wsse + "Username", _Username),
                                new XElement(wsse + "Password",
                                    new XAttribute(XName.Get("Type", ""), PasswordType)
                                )
                            )
                        )
                    );

            return header;
        }

        public XDocument CreateRequest()
        {
            XDocument soapRequest = new XDocument(
                new XDeclaration("1.0", "UTF-8", "no")
            );

            soapRequest.Add(CreateEnvelope());

            return soapRequest;
        }

        private XElement CreateEnvelope()
        {
            XElement envelope = new XElement(ns + "Envelope");

            envelope.Add(new XAttribute(XNamespace.Xmlns + "soapenv", ns));
            envelope.Add(new XAttribute(XNamespace.Xmlns + "xsi", xsi));
            envelope.Add(new XAttribute(XNamespace.Xmlns + "xsd", xsd));

            List<string> namespaces = new List<string>();

            if (!String.IsNullOrEmpty(input.BodyNamespaceName))
            {
                bodyNamespace = input.BodyNamespace;
                namespaces.Add(input.BodyNamespaceName);

                XAttribute attr = new XAttribute(XNamespace.Xmlns + input.BodyNamespaceName, input.BodyNamespace);
                envelope.Add(attr);
            }

            if (!String.IsNullOrEmpty(input.RequestConnectionNamespaceName))
            {
                requestConnectionNamespace = input.RequestConnectionNamespace;

                if (!namespaces.Contains(input.RequestConnectionNamespaceName))
                {
                    namespaces.Add(input.RequestConnectionNamespaceName);
                    XAttribute attr = new XAttribute(XNamespace.Xmlns + input.RequestConnectionNamespaceName, input.RequestConnectionNamespace);
                    envelope.Add(attr);
                }
            }

            if (!String.IsNullOrEmpty(input.RequestConnectionFieldNamespaceName))
            {
                requestConnectionFieldNamespace = input.RequestConnectionFieldNamespace;

                if (!namespaces.Contains(input.RequestConnectionFieldNamespaceName))
                {
                    namespaces.Add(input.RequestConnectionFieldNamespaceName);
                    XAttribute attr = new XAttribute(XNamespace.Xmlns + input.RequestConnectionFieldNamespaceName, input.RequestConnectionFieldNamespace);
                    envelope.Add(attr);
                }
            }

            if (!String.IsNullOrEmpty(input.FilNamespaceName))
            {
                filNamespace = input.FilNamespace;

                if (!namespaces.Contains(input.FilNamespaceName))
                {
                    namespaces.Add(input.FilNamespaceName);
                    XAttribute attr = new XAttribute(XNamespace.Xmlns + input.FilNamespaceName, input.FilNamespace);
                    envelope.Add(attr);
                }
            }

            if (!String.IsNullOrEmpty(input.SubFilNamespaceName))
            {
                subFilNamespace = input.SubFilNamespace;

                if (!namespaces.Contains(input.SubFilNamespaceName))
                {
                    namespaces.Add(input.SubFilNamespaceName);
                    XAttribute attr = new XAttribute(XNamespace.Xmlns + input.SubFilNamespaceName, input.SubFilNamespace);
                    envelope.Add(attr);
                }
            }

            if (!String.IsNullOrEmpty(input.FilFieldNamespaceName))
            {
                filFieldNamespace = input.FilFieldNamespace;

                if (!namespaces.Contains(input.FilFieldNamespaceName))
                {
                    namespaces.Add(input.FilFieldNamespaceName);
                    XAttribute attr = new XAttribute(XNamespace.Xmlns + input.FilFieldNamespaceName, input.FilFieldNamespace);
                    envelope.Add(attr);
                }
            }

            envelope.Add(CreateHeader());
            envelope.Add(CreateBody());

            return envelope;
        }

        public void LoadParameters(Dictionary<string, string> parameters)
        {
            this.parameters = parameters;
        }
    }
}
