using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HttpSoapClient;
using Models.Request;
using System.Xml.Linq;
using Models.Response.Parser;
using Models.Response.Json;
using Logger;
using System;

namespace ConsultaNovedadesMailApi.Controllers
{
    [Route("ConsultaNovedadEnvioMail")]
    [ApiController]
    public class MailNewsController : ControllerBase
    {
        private ILogger logger;

        public MailNewsController(ILogger logger)
        {
            this.logger = logger;
        }

        PfWebConsultaNovedadEnvioMailFil fil;

        [HttpGet("NovedadEnvioMailBody")]
        public ActionResult<IEnumerable<PfWebConsultaNovedadEnvioMailResponse>> GetPfWebConsultaNovedadEnvioMailBody(PfWebConsultaNovedadEnvioMailFil inputParams)
        {
            this.fil = inputParams;

            return GetResponse();
        }

        [HttpGet("NovedadEnvioMail")]
        public ActionResult<IEnumerable<PfWebConsultaNovedadEnvioMailResponse>> GetPfWebConsultaNovedadEnvioMailString(string isSiguiente)
        {
            PfWebConsultaNovedadEnvioMailFil mailFil = new PfWebConsultaNovedadEnvioMailFil
            {
                isSiguiente = string.IsNullOrEmpty(isSiguiente) ? "0" : isSiguiente
            };

            this.fil = mailFil;

            return GetResponse();
        }

        private JsonResult GetResponse()
        {
            logger.WriteLog(LogLevel.DEBUG, "Configuring parameters");

            Dictionary<string, string> ctsParams = new Dictionary<string, string>
            {
                { "WsSecurity-Username", "franco.milanese" },
                { "Endpoint", "http://172.28.195.215:9080/COBISCorp.eCOBIS.PfWebConsultaNovedadEnvioMail.Service/PfWebConsultaNovedadEnvioMailWSService" },
                { "Timeout", "10000" },
                { "SOAPAction", "http://service.pfwebconsultanovedadenviomail.ecobis.cobiscorp.ws/PfWebConsultaNovedadEnvioMail/PfWebConsultaNovedadEnvioMail" },
                { "SOAPVersion", "1.1" }
            };

            ICtsBodyRequest input;
            SoapClient soapClient;

            input = new PfWebConsultaNovedadEnvioMail(this.fil);

            logger.WriteLog(LogLevel.DEBUG, "Creating SoapClient for: " + ctsParams["Endpoint"]);
            soapClient = new SoapClient(logger, input, ctsParams);
            IParser ctsParser;

            try
            {
                logger.WriteLog(LogLevel.INFO, "Getting Soap Response");
                XDocument soapResponse = soapClient.GetResponse();

                logger.WriteLog(LogLevel.INFO, "Parsing Soap Response");
                ctsParser = new PfWebConsultaNovedadEnvioMailParser(soapResponse);

                logger.WriteLog(LogLevel.INFO, "Retrieving JSON Response");
            }
            catch (Exception ex)
            {
                ctsParser = new PfWebConsultaNovedadEnvioMailParser(ex);
            }

            return ctsParser.GetJsonResponse();
        }
    }
}