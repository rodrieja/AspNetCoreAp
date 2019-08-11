using System.Xml.Serialization;

namespace Models.Soap.Response
{
    [XmlRoot("pfWebConsultaNovedadEnvioMailRet", Namespace = "http://dto.pfwebconsultanovedadenviomail.ecobis.cobiscorp")]
    public class PfWebConsultaNovedadEnvioMailRet
    {
        /// <remarks/>
        [XmlElement]
        public int onSolicitud { get; set; }
        /// <remarks/>
        [XmlElement]
        public string oeSolicitud { get; set; }
        /// <remarks/>
        [XmlElement]
        public string onIdDebin { get; set; }
        /// <remarks/>
        [XmlElement]
        public string onCuil { get; set; }
        /// <remarks/>
        [XmlElement]
        public int onEnte { get; set; }
        /// <remarks/>
        [XmlElement]
        public string odEmail { get; set; }
        /// <remarks/>
        [XmlElement]
        public string onCbu { get; set; }
        /// <remarks/>
        [XmlElement]
        public string ocTipoDocumento { get; set; }
        /// <remarks/>
        [XmlElement]
        public string onDocumento { get; set; }
        /// <remarks/>
        [XmlElement]
        public string odNombre { get; set; }
        /// <remarks/>
        [XmlElement]
        public string odApellido { get; set; }
        /// <remarks/>
        [XmlElement]
        public string onTelefono { get; set; }
        /// <remarks/>
        [XmlElement]
        public string odTipoPlazoFijo { get; set; }
        /// <remarks/>
        [XmlElement]
        public decimal oiCapitalPlazoFijo { get; set; }
        /// <remarks/>
        [XmlElement]
        public int okPlazo { get; set; }
        /// <remarks/>
        [XmlElement]
        public double opTasaSolicitud { get; set; }
        /// <remarks/>
        [XmlElement]
        public double opTasaParamPf { get; set; }
        /// <remarks/>
        /// <remarks/>
        [XmlElement]
        public string onCertificado { get; set; }
        /// <remarks/>
        [XmlElement]
        public decimal oiInteresEstimadoPf { get; set; }
        /// <remarks/>
        [XmlElement]
        public string ofAltaSolicitud { get; set; }
        /// <remarks/>
        [XmlElement]
        public string ofAprobacionDebin { get; set; }
        /// <remarks/>
        [XmlElement]
        public string ofAltaPlazoFijo { get; set; }
        /// <remarks/>
        [XmlElement]
        public string ofVencimientoPlazoFijo { get; set; }
        /// <remarks/>
        [XmlElement]
        public string ofCancelacionPlazoFijo { get; set; }
        /// <remarks/>
        [XmlElement]
        public string ofEnvioMail { get; set; }
        /// <remarks/>
        [XmlElement]
        public string ofRechazoTrasferencia { get; set; }
        /// <remarks/>
        [XmlElement]
        public string ofTransferenciaRealizada { get; set; }
    }
}
