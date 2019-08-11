using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Entities.Models
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
