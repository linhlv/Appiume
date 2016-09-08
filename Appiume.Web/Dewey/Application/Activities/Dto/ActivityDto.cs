using System;
using Appiume.Apm.Application.Services.Dto;
using Appiume.Web.Dewey.Core.Activities;

namespace Appiume.Web.Dewey.Application.Activities.Dto
{
    public class ActivityDto : EntityDto<long>
    {
        public ActivityType ActivityType { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
