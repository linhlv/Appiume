using System.ComponentModel.DataAnnotations;
using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Web.Dewey.Application.Friendships.Dto
{
    public class SendFriendshipRequestInput : IInputDto
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}