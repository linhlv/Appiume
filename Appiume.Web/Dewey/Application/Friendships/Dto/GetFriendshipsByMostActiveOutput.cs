using System.Collections.Generic;
using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Web.Dewey.Application.Friendships.Dto
{
    public class GetFriendshipsByMostActiveOutput : IOutputDto
    {
        public IList<FriendshipDto> Friendships { get; set; }
    }
}