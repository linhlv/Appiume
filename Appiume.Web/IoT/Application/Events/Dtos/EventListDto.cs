using System;
using Appiume.Apm.Application.Services.Dto;
using Appiume.Apm.AutoMapper;
using Appiume.Web.IoT.Core.Events;

namespace Appiume.Web.IoT.Application.Events.Dtos
{
    [AutoMapFrom(typeof (Event))]
    public class EventListDto: FullAuditedEntityDto<Guid>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public bool IsCancelled { get; set; }

        public virtual int MaxRegistrationCount { get; protected set; }

        public int RegistrationsCount { get; set; }
    }
}