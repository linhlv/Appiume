﻿using System.Collections.Generic;
using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Web.Modules.TaskCloud.Application.People.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAllPeopleOutput 
    {
        /// <summary>
        /// 
        /// </summary>
        public List<PersonDto> People { get; set; }
    }
}