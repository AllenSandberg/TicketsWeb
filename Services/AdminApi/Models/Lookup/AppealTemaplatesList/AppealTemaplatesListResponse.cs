using AdminApi.Models.Entities;
using System.Collections.Generic;

namespace AdminApi.Models.Lookup.AppealTemapltesList
{
    public class AppealTemaplatesListResponse:BaseResponse
    {
        public List<AppealTemplateEntity> Templates { get; set; }
    }
}
