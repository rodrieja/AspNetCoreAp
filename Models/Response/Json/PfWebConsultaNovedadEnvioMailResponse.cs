using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Models.Response.Json
{
    public class PfWebConsultaNovedadEnvioMailResponse
    {
        [DataMember]
        public PfWebConsultaNovedadesEnvioMailList Data { get; set; }
        [DataMember]
        public List<ErrorResponse> Errors { get; set; }
    }
}
