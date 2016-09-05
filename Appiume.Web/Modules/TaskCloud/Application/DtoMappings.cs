using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appiume.Web.Modules.TaskCloud.Application.Tasks.Dtos;
using Appiume.Web.Modules.TaskCloud.Core.Tasks;
using AutoMapper;

namespace Appiume.Web.Modules.TaskCloud.Application
{
    /// <summary>
    /// 
    /// </summary>
    internal static class DtoMappings
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Map()
        {
            //I specified mapping for AssignedPersonId since NHibernate does not fill Task.AssignedPersonId
            //If you will just use EF, then you can remove ForMember definition.
            Mapper.CreateMap<Task, TaskDto>().ForMember(t => t.AssignedPersonId, opts => opts.MapFrom(d => d.AssignedPerson.Id));
        }
    }
}