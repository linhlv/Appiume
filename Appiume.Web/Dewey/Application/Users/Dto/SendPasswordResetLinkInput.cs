using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Web.Dewey.Application.Users.Dto
{
    public class SendPasswordResetLinkInput : IInputDto
    {
        public string EmailAddress { get; set; }
    }
}