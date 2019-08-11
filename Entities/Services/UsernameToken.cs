using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Entities.Services
{
    [XmlRoot(Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")]
    public class UsernameToken
    {
        //[XmlAttribute(Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd")]
        //public string Id { get; set; }

        [XmlElement]
        public string Username { get; set; }
        //[XmlElement(DataType = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText")]
        [XmlElement]
        public string Password { get; set; }
    }
}
