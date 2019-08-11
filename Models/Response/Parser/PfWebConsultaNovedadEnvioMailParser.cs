using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Models.Response.Json;
using Models.Soap.Response;

namespace Models.Response.Parser
{
    public class PfWebConsultaNovedadEnvioMailParser : IParser
    {
        private XDocument soapResponse = null;
        private Exception error = null;

        private readonly XNamespace retNamespace = "http://dto.pfwebconsultanovedadenviomail.ecobis.cobiscorp";
        private readonly XNamespace errorNamespace = "http://dto2.sdf.cts.cobis.cobiscorp.com";
        private readonly XNamespace errorElementNamespace = "http://dto2.commons.ecobis.cobiscorp";

        public PfWebConsultaNovedadEnvioMailParser(Exception error)
        {
            this.error = error;
        }

        public PfWebConsultaNovedadEnvioMailParser(XDocument soapResponse)
        {
            this.soapResponse = soapResponse;
        }

        public JsonResult GetJsonResponse()
        {
            PfWebConsultaNovedadesEnvioMailList pfWebConsultaNovedadesEnvioMailList = new PfWebConsultaNovedadesEnvioMailList
            {
                NovedadesMail = new List<PfWebConsultaNovedadEnvioMailRet>()
            };
            List<ErrorResponse> errorResponse = new List<ErrorResponse>();

            if (soapResponse != null)
            {
                string errorCode = GetValue(soapResponse, errorElementNamespace + "code");
                // Si no tengo el elemento code, significa que es un error interno de CTS
                if (errorCode != null)
                {
                    if ("0".Equals(errorCode))
                    {
                        IEnumerable<XElement> remoteResponse = soapResponse.Descendants(retNamespace + "pfWebConsultaNovedadEnvioMailRet");
                        // Obtengo la lista de objetos de la respuesta del WS
                        List<PfWebConsultaNovedadEnvioMailRet> pfWebConsultaNovedadEnvioMailRetList = Deserialize(remoteResponse);

                        pfWebConsultaNovedadesEnvioMailList = new PfWebConsultaNovedadesEnvioMailList
                        {
                            NovedadesMail = pfWebConsultaNovedadEnvioMailRetList
                        };
                    }
                    else
                    {
                        string errorMessage = GetValue(soapResponse, errorElementNamespace + "message");

                        errorResponse.Add(new ErrorResponse
                        {
                            codigo = errorCode.ToString(),
                            descripcion = errorMessage
                        }
                        );
                    }
                }
                else
                {
                    string errorMessage = GetValue(soapResponse, "faultcode");

                    if (errorMessage != null)
                        errorMessage += " - " + GetValue(soapResponse, "faultstring");
                    else
                        errorMessage = "Error desconocido";

                    errorResponse.Add(new ErrorResponse
                    {
                        codigo = "999",
                        descripcion = errorMessage
                    }
                    );
                }
            }

            if (error != null)
            {
                errorResponse.Add(new ErrorResponse
                {
                    codigo = "999",
                    descripcion = error.Message
                }
                );
            }

            // Convierto la lista a la estructura que espera VTM
            PfWebConsultaNovedadEnvioMailResponse jsonResponse = new PfWebConsultaNovedadEnvioMailResponse()
            {
                Data = pfWebConsultaNovedadesEnvioMailList,
                Errors = errorResponse
            };

            return new JsonResult(jsonResponse);
        }

        private string GetValue(XDocument soapResponse, XName xName)
        {
            string value = null;

            var enumertor = soapResponse.Descendants(xName).GetEnumerator();

            if (enumertor.MoveNext())
                value = enumertor.Current.Value;

            return value;
        }

        private List<PfWebConsultaNovedadEnvioMailRet> Deserialize(IEnumerable<XElement> elements)
        {
            var serializer = new XmlSerializer(typeof(PfWebConsultaNovedadEnvioMailRet));
            List<PfWebConsultaNovedadEnvioMailRet> result = new List<PfWebConsultaNovedadEnvioMailRet>();

            foreach (var element in elements)
            {
                using (TextReader reader = new StringReader(element.ToString()))
                {
                    result.Add((PfWebConsultaNovedadEnvioMailRet)serializer.Deserialize(reader));
                }
            }

            return result;
        }
    }
}
