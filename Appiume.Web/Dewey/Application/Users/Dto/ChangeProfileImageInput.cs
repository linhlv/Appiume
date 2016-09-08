using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Web.Dewey.Application.Users.Dto
{
    public class ChangeProfileImageInput :IInputDto
    {
        public string FileName { get; set; }
    }
}