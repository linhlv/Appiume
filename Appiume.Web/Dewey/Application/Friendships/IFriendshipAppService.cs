
using Appiume.Apm.Application.Services;
using Appiume.Web.Dewey.Application.Friendships.Dto;

namespace Appiume.Web.Dewey.Application.Friendships
{
    public interface IFriendshipAppService : IApplicationService
    {
        GetFriendshipsOutput GetFriendships(GetFriendshipsInput input);

        GetFriendshipsByMostActiveOutput GetFriendshipsByMostActive(GetFriendshipsByMostActiveInput input);

        void ChangeFriendshipProperties(ChangeFriendshipPropertiesInput input);

        SendFriendshipRequestOutput SendFriendshipRequest(SendFriendshipRequestInput input);

        void RemoveFriendship(RemoveFriendshipInput input);

        void AcceptFriendship(AcceptFriendshipInput input);

        void RejectFriendship(RejectFriendshipInput input);

        void CancelFriendshipRequest(CancelFriendshipRequestInput input);

        void UpdateLastVisitTime(UpdateLastVisitTimeInput input);
    }
}