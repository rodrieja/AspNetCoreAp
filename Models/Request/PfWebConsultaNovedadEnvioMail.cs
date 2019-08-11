using System.Collections.Generic;
using System.Xml.Linq;
using HttpSoapClient;

namespace Models.Request
{
    public class PfWebConsultaNovedadEnvioMail : ICtsBodyRequest
    {
        private PfWebConsultaNovedadEnvioMailFil fil;

        public PfWebConsultaNovedadEnvioMail(PfWebConsultaNovedadEnvioMailFil fil)
        {
            this.fil = fil;

            BodyType = "PfWebConsultaNovedadEnvioMail";
            BodyNamespaceName = "ser";
            BodyNamespace = XNamespace.Get("http://service.pfwebconsultanovedadenviomail.ecobis.cobiscorp");

            RequestConnectionNamespaceName = BodyNamespaceName;
            RequestConnectionNamespace = BodyNamespace;

            RequestConnectionFieldNamespaceName = "dto2";
            RequestConnectionFieldNamespace = XNamespace.Get("http://dto2.commons.ecobis.cobiscorp");

            FilType = "fil";
            FilNamespaceName = BodyNamespaceName;
            FilNamespace = BodyNamespace;

            SubFilType = "inPfWebConsultaNovedadEnvioMailFil";
            SubFilNamespaceName = BodyNamespaceName;
            SubFilNamespace = BodyNamespace;

            FilFieldNamespaceName = "dto";
            FilFieldNamespace = XNamespace.Get("http://dto.pfwebconsultanovedadenviomail.ecobis.cobiscorp");
        }

        #region Interface Properties
        public string BodyType { get; set; }
        public string FilType { get; set; }
        public string SubFilType { get; set; }
        public XNamespace BodyNamespace { get; set; }
        public string BodyNamespaceName { get; set; }
        public XNamespace FilFieldNamespace { get; set; }
        public XNamespace RequestConnectionNamespace { get; set; }
        public string FilNamespaceName { get; set; }
        public string RequestConnectionNamespaceName { get; set; }
        public XNamespace FilNamespace { get; set; }
        public string SubFilNamespaceName { get; set; }
        public XNamespace SubFilNamespace { get; set; }
        public string FilFieldNamespaceName { get; set; }
        public string RequestConnectionFieldNamespaceName { get; set; }
        public XNamespace RequestConnectionFieldNamespace { get; set; }
        #endregion

        public List<XElement> GetInputParamElements()
        {
            List<XElement> parameters = new List<XElement>();

            foreach (var field in this.fil.GetType().GetProperties())
            {
                parameters.Add(new XElement(FilFieldNamespace + field.Name, field.GetValue(this.fil, null)));
            }

            return parameters;
        }
    }
}
