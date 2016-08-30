﻿using System;
using Appiume.Apm.Application.Services.Dto;
using Appiume.Apm.AutoMapper;
using Appiume.Web.IoT.Core.Events;

namespace Appiume.Web.IoT.Application.Events.Dtos
{
    [AutoMapFrom(typeof(EventRegistration))]
    public class EventRegistrationDto : CreationAuditedEntityDto
    {
        public virtual Guid EventId { get; protected set; }

        public virtual long UserId { get; protected set; }

        public virtual string UserName { get; protected set; }

        public virtual string UserSurname { get; protected set; }
    }
}