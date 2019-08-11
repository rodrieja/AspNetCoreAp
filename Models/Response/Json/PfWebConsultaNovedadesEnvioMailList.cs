using Models.Soap.Response;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Models.Response.Json
{
    public class PfWebConsultaNovedadesEnvioMailList
    {
        [DataMember]
        public List<PfWebConsultaNovedadEnvioMailRet> NovedadesMail { get; set; }
    }
}
