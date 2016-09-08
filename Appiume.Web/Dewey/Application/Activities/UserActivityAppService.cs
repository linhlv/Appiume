using Appiume.Apm.Application.Services;
using Appiume.Apm.Domain.Repositories;
using Appiume.Apm.Runtime.Session;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Apm.UI;
using Appiume.Web.Dewey.Application.Activities.Dto;
using Appiume.Web.Dewey.Application.Mapping;
using Appiume.Web.Dewey.Core.Activities;
using Appiume.Web.Dewey.Core.Friendships;
using Appiume.Web.Dewey.Core.Users;

namespace Appiume.Web.Dewey.Application.Activities
{
    public class UserActivityAppService : ApplicationService, IUserActivityAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IUserFollowedActivityRepository _followedActivityRepository;
        private readonly IFriendshipDomainService _friendshipDomainService;

        public UserActivityAppService(IRepository<User, long> userRepository, IUserFollowedActivityRepository followedActivityRepository, IFriendshipDomainService friendshipDomainService)
        {
            _userRepository = userRepository;
            _followedActivityRepository = followedActivityRepository;
            _friendshipDomainService = friendshipDomainService;
        }

        public GetFollowedActivitiesOutput GetFollowedActivities(GetFollowedActivitiesInput input)
        {
            var currentUser = _userRepository.Load(ApmSession.GetUserId());
            var user = _userRepository.Load(input.UserId);

            //Can see activities of this user?
            if (currentUser.Id != user.Id && !_friendshipDomainService.HasFriendship(user, currentUser))
            {
                throw new UserFriendlyException("Can not see activities of this user!");
            }

            //TODO: Think on private activities? When a private task is created or completed?

            var activities = _followedActivityRepository.Getactivities(input.UserId, input.IsActor, input.BeforeId, input.MaxResultCount);

            return new GetFollowedActivitiesOutput
                       {
                           Activities = activities.MapIList<UserFollowedActivity, UserFollowedActivityDto>()
                       };
        }
    }
}