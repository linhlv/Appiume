using Appiume.Apm.Application.Services.Dto;
using Appiume.Apm.AutoMapper;
using Appiume.Web.Dewey.Core.People;

namespace Appiume.Web.Dewey.Application.People.Dtos
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