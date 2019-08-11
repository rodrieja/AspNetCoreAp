using System.Runtime.Serialization;

namespace Models.Response.Json
{
    [DataContract]
    public class ErrorResponse
    {
        [DataMember]
        public string codigo;
        [DataMember]
        public string descripcion;
    }
}
