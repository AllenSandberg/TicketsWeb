using PenaltiesManagement.Models.Entities;
using System.Collections.Generic;

namespace PenaltiesManagement.Models.API.SystemApi.AppealTemplatesList
{
    public class AppealTemplatesListApiResponse:BaseApiResponse
    {
        public List<AppealTemplateEntity> Templates { get; set; }
    }
}
