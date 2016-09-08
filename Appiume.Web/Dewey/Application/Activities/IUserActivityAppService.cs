
using Appiume.Apm.Application.Services;
using Appiume.Web.Dewey.Application.Activities.Dto;

namespace Appiume.Web.Dewey.Application.Activities
{
    public interface IUserActivityAppService : IApplicationService
    {
        GetFollowedActivitiesOutput GetFollowedActivities(GetFollowedActivitiesInput input);
    }
}