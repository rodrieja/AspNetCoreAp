using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Entities.Models
{
    [DataContract]
    public class MailNewsResponse : ICtsResponse
    {
        [DataMember]
        public IDataList Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [DataMember]
        public List<ErrorResponse> Errors { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
