using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using PfWebConsultaNovedadEnvioMailService;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Entities.Services
{
    public class MailNewsCtsService : ICtsProvider
    {
        private IParser parser;

        public JsonResult JsonCtsResponse { get; set; }

        public async Task<ICtsResponse> GetCtsResponseAsync()
        {
            PfWebConsultaNovedadEnvioMailClient client = new PfWebConsultaNovedadEnvioMailClient();
            EndpointAddress endpoint = new EndpointAddress("http://172.28.195.215:9080/COBISCorp.eCOBIS.PfWebConsultaNovedadEnvioMail.Service/PfWebConsultaNovedadEnvioMailWSService");
            
            client = new PfWebConsultaNovedadEnvioMailClient(PfWebConsultaNovedadEnvioMailClient.EndpointConfiguration.PfWebConsultaNovedadEnvioMailWSPort, endpoint);

            PfWebConsultaNovedadEnvioMailResponse response;

            using (var scope = new FlowingOperationContextScope(client.InnerChannel))
            {
                // Add a SOAP Header to an outgoing request 
                MessageHeaders messageHeadersElement = OperationContext.Current.OutgoingMessageHeaders;
                messageHeadersElement.Add(new Security()
                {
                    UsernameToken = new UsernameToken()
                    {
                        Username = "franco.milanese",
                        Password = ""
                    }
                });

                RequestConnection requestConnection = new RequestConnection
                {
                    user = "externo",
                    applicationID = "1"
                };

                Fil fil = new Fil
                {
                    inPfWebConsultaNovedadEnvioMailFil = new PfWebConsultaNovedadEnvioMailFil
                    {
                        isSiguiente = 0
                    }
                };

                PfWebConsultaNovedadEnvioMailRequest request = new PfWebConsultaNovedadEnvioMailRequest()
                {
                    requestConnection = requestConnection,
                    fil = fil
                };

                //response = client.PfWebConsultaNovedadEnvioMailAsync(request, fil).GetAwaiter().GetResult();
                //response = await client.PfWebConsultaNovedadEnvioMailAsync(request, fil).ContinueOnScope(scope);
                response = await client.PfWebConsultaNovedadEnvioMailAsync(request).ContinueOnScope(scope);
                //response = client.PfWebConsultaNovedadEnvioMailAsync(request).GetAwaiter().GetResult();
            }

            parser = new MailNewsParser(response.executeResponse);

            ICtsResponse ctsResponse = new MailNewsResponse();

            return ctsResponse;
        }

        private Fil CreateFil()
        {
            Fil fil = new Fil
            {
                inPfWebConsultaNovedadEnvioMailFil = new PfWebConsultaNovedadEnvioMailFil
                {
                    isSiguiente = 0
                }
            };

            return fil;
        }

        private RequestConnection CreateRequest()
        {
            RequestConnection request = new RequestConnection
            {
                user = "externo",
                applicationID = "1"
            };

            return request;
        }
    }
}
