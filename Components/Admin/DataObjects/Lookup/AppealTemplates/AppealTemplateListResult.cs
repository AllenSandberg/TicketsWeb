using DataObjects.Entities;
using System.Collections.Generic;

namespace DataObjects.Lookup.AppealTemplates
{
    /** Returned Result for AppealTemplateList(AppealTemplateListParameters parameters) */
    public class AppealTemplateListResult:BaseResult
    {
        public List<AppealTemplateEntity> Templates { get; set; }
    }
}
