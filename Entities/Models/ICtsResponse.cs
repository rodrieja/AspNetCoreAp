using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Entities.Models
{
    public interface ICtsResponse
    {
        IDataList Data { get; set; }

        List<ErrorResponse> Errors { get; set; }

    }
}