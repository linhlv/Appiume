using System.Collections.Generic;
using Appiume.Apm.Application.Services.Dto;

namespace Appiume.Web.Dewey.Application.Activities.Dto
{
    public class GetUserActivitiesOutput : IOutputDto
    {
        public IList<UserFollowedActivityDto> Activities { get; set; }
    }
}