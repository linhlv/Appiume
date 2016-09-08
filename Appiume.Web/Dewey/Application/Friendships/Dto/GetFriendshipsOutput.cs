using System.Collections.Generic;
using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Web.Dewey.Application.Friendships.Dto
{
    public class GetFriendshipsOutput : IOutputDto
    {
        public IList<FriendshipDto> Friendships { get; set; }
    }
}