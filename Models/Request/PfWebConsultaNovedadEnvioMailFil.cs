using Microsoft.AspNetCore.Mvc;

namespace Models.Request
{
    public class PfWebConsultaNovedadEnvioMailFil
    {
        [FromQuery]
        public string isSiguiente { get; set; }
    }
}
