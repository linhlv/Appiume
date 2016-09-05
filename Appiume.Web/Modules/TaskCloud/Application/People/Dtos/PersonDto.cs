using Appiume.Apm.Application.Services.Dto;
using Appiume.Apm.AutoMapper;
using Appiume.Web.Modules.TaskCloud.Core.People;

namespace Appiume.Web.Modules.TaskCloud.Application.People.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(Person))] //AutoMapFrom attribute maps Person -> PersonDto
    public class PersonDto : EntityDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }
}