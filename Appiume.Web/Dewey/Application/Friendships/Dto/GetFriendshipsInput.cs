using System.ComponentModel.DataAnnotations;
using Appiume.Apm.Application.Services.Dto;
using Appiume.Web.Dewey.Core.Friendships;

namespace Appiume.Web.Dewey.Application.Friendships.Dto
{
    public class GetFriendshipsInput : IInputDto
    {
        [Range(1, int.MaxValue)]
        public long UserId { get; set; }

        public FriendshipStatus? Status { get; set; }

        public bool? CanAssignTask { get; set; }
    }
}