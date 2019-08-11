using Microsoft.AspNetCore.Mvc;

namespace Entities.Models
{
    public interface IParser
    {
        JsonResult GetJsonResponse();
    }
}
