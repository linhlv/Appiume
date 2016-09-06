using System;
using System.Collections.Generic;
using Appiume.Apm.Application.Services.Dto;
using Appiume.Apm.AutoMapper;
using Appiume.Web.Dewey.Core.Events;

namespace Appiume.Web.Dewey.Application.Events.Dtos
{
    [AutoMapFrom(typeof(Event))]
    public class EventDetailOutput : FullAuditedEntityDto<Guid>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public bool IsCancelled { get; set; }

        public virtual int MaxRegistrationCount { get; protected set; }

        public int RegistrationsCount { get; set; }

        public ICollection<EventRegistrationDto> Registrations { get; set; }
    }
}