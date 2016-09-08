using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Web.Dewey.Application.Users.Dto
{
    public class GetUserInput : IInputDto
    {
        public long UserId { get; set; }
    }
}