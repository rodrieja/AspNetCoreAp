using Microsoft.AspNetCore.Mvc;
using PfWebConsultaNovedadEnvioMailService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    class MailNewsParser : IParser
    {
        PfWebConsultaNovedadEnvioMailRes ctsResponse;
        MailNewsResponse mailNewsresponse;

        public JsonResult GetJsonResponse()
        {
            return ParseResponse();
        }

        private JsonResult ParseResponse()
        {
            mailNewsresponse = new MailNewsResponse
            {
                Data = new MailNewsList(),
                Errors = new List<ErrorResponse>()
            };

            mailNewsresponse.Data.CreateList();
            MailNewsList list = (MailNewsList)mailNewsresponse.Data;

            foreach (var ret in ctsResponse.pfWebConsultaNovedadEnvioMailRet)
            {
                list.mailNewsList.Add(new MailNews
                {
                    onSolicitud = ret.onSolicitud,
                    onCuil = ret.onCuil,
                    odEmail = ret.odEmail,
                    odApellido = ret.odApellido,
                    odNombre = ret.odNombre
                });
            }

            return new JsonResult(mailNewsresponse);
        }

        public MailNewsParser(PfWebConsultaNovedadEnvioMailRes response)
        {
            ctsResponse = response;
        }
    }
}
