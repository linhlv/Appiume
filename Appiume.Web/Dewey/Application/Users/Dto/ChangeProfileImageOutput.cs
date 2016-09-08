using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Web.Dewey.Application.Users.Dto
{
    public class ChangeProfileImageOutput:IOutputDto
    {
        public string OldFileName { get; set; }
    }
}