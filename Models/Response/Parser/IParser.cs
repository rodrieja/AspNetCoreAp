using Microsoft.AspNetCore.Mvc;

namespace Models.Response.Parser
{
    public interface IParser
    {
        JsonResult GetJsonResponse();
    }
}
