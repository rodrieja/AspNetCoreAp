using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Logger;

namespace HttpSoapClient
{
    public class SoapClient
    {
        private ISoapRequest soapRequest;
        private ILogger logger;
        private Dictionary<string, string> parameters;

        public SoapClient(ILogger logger, ICtsBodyRequest ctsRequest, Dictionary<string, string> parameters)
        {
            this.logger = logger;
            this.parameters = parameters;

            soapRequest = new SoapRequest<ICtsBodyRequest>(ctsRequest);
            soapRequest.LoadParameters(parameters);

            Console.WriteLine(soapRequest.CreateRequest());
        }

        public XDocument GetResponse()
        {
            string _apiUrl = parameters["Endpoint"];
            TimeSpan _timeout = TimeSpan.FromMilliseconds(Int32.Parse(parameters["Timeout"]));
            XDocument soapResponse;

            try
            {
                using (var client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip }) { Timeout = _timeout })
                {
                    var httpRequest = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(_apiUrl),
                        Method = HttpMethod.Post,
                    };

                    XDocument request = soapRequest.CreateRequest();

                    httpRequest.Content = new StringContent(request.ToString(), Encoding.UTF8, "text/xml");

                    httpRequest.Headers.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
                    httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");
                    httpRequest.Headers.Add("SOAPAction", parameters["SOAPAction"]);
                    httpRequest.Version = new Version(parameters["SOAPVersion"]);

                    logger.WriteLog(LogLevel.DEBUG, "Calling external WS: " + _apiUrl);
                    logger.WriteLog(LogLevel.DEBUG, httpRequest.ToString());
                    logger.WriteLog(LogLevel.DEBUG, request.ToString());

                    HttpResponseMessage response = client.SendAsync(httpRequest).Result;

                    Task<Stream> streamTask = response.Content.ReadAsStreamAsync();
                    Stream stream = streamTask.Result;
                    var sr = new StreamReader(stream);
                    soapResponse = XDocument.Load(sr);

                    logger.WriteLog(LogLevel.DEBUG, "Response obtained from WS");
                    logger.WriteLog(LogLevel.DEBUG, soapResponse.ToString());
                }
            }
            catch (AggregateException ex)
            {
                logger.WriteLog(LogLevel.ERROR, ex.Message);

                if (ex.InnerException is TaskCanceledException)
                {
                    throw new Exception("Timeout");
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                logger.WriteLog(LogLevel.ERROR, ex.Message);

                throw ex;
            }

            return soapResponse;
        }
    }
}
