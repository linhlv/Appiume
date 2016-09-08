using System;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Appiume.Apm.Application.Services;
using Appiume.Apm.Domain.Repositories;
using Appiume.Apm.Runtime.Session;
using Appiume.Apm.UI;
using Appiume.Web.Dewey.Application.Friendships.Dto;
using Appiume.Web.Dewey.Application.Mapping;
using Appiume.Web.Dewey.Core.Friendships;
using Appiume.Web.Dewey.Core.Users;
using Appiume.Web.Dewey.Core.Utils.Mail;

namespace Appiume.Web.Dewey.Application.Friendships
{
    public class FriendshipAppService : ApplicationService, IFriendshipAppService
    {
        private readonly IRepository<User, long> _taskeverUserRepository;
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IEmailService _emailService;
        private readonly IFriendshipPolicy _friendshipPolicy;

        public FriendshipAppService(
            IRepository<User, long> taskeverUserRepository, 
            IFriendshipRepository friendshipRepository, 
            IFriendshipPolicy friendshipPolicy, 
            IEmailService emailService)
        {
            _taskeverUserRepository = taskeverUserRepository;
            _friendshipRepository = friendshipRepository;
            _friendshipPolicy = friendshipPolicy;
            _emailService = emailService;
        }

        public virtual GetFriendshipsOutput GetFriendships(GetFriendshipsInput input)
        {
            //TODO: Check if current user can see friendships of the the requested user!
            var friendships = _friendshipRepository.GetAllWithFriendUser(input.UserId, input.Status, input.CanAssignTask);
            return new GetFriendshipsOutput { Friendships = friendships.MapIList<Friendship, FriendshipDto>() };
        }

        public GetFriendshipsByMostActiveOutput GetFriendshipsByMostActive(GetFriendshipsByMostActiveInput input)
        {
            var friendships =
                _friendshipRepository
                    .GetAllWithFriendUser(ApmSession.GetUserId())
                    .Where(f => f.Status == FriendshipStatus.Accepted)
                    .OrderByDescending(friendship => friendship.LastVisitTime)
                    .Take(input.MaxResultCount)
                    .ToList();

            return new GetFriendshipsByMostActiveOutput { Friendships = friendships.MapIList<Friendship, FriendshipDto>() };
        }

        public virtual void ChangeFriendshipProperties(ChangeFriendshipPropertiesInput input)
        {
            var currentUser = _taskeverUserRepository.Load(ApmSession.GetUserId());
            var friendShip = _friendshipRepository.Get(input.Id); //TODO: Call FirstOrDefault and throw a specific exception?

            if (!_friendshipPolicy.CanChangeFriendshipProperties(currentUser, friendShip))
            {
                throw new ApplicationException("Can not change properties of this friendship!");
            }

            //TODO: Implement mappings using Auto mapper!

            if (input.CanAssignTask.HasValue)
            {
                friendShip.CanAssignTask = input.CanAssignTask.Value;
            }

            if (input.FollowActivities.HasValue)
            {
                friendShip.FollowActivities = input.FollowActivities.Value;
            }
        }

        public virtual SendFriendshipRequestOutput SendFriendshipRequest(SendFriendshipRequestInput input)
        {
            var friendUser = _taskeverUserRepository.FirstOrDefault(user => user.EmailAddress == input.EmailAddress);
            if (friendUser == null)
            {
                throw new UserFriendlyException("Can not find a user with email address: " + input.EmailAddress);
            }

            var currentUser = _taskeverUserRepository.Load(ApmSession.GetUserId());

            //Check if they are already friends
            var friendship = _friendshipRepository.GetOrNull(currentUser.Id, friendUser.Id);
            if (friendship != null)
            {
                if (friendship.CanBeAcceptedBy(currentUser))
                {
                    friendship.AcceptBy(currentUser);
                }

                return new SendFriendshipRequestOutput { Status = friendship.Status };
            }

            //Add new friendship request
            friendship = Friendship.CreateAsRequest(currentUser, friendUser);
            _friendshipRepository.Insert(friendship);

            SendRequestEmail(friendship);

            return new SendFriendshipRequestOutput { Status = friendship.Status };
        }

        public virtual void RemoveFriendship(RemoveFriendshipInput input)
        {
            var currentUser = _taskeverUserRepository.Load(ApmSession.GetUserId());
            var friendship = _friendshipRepository.Get(input.Id); //TODO: Call FirstOrDefault and throw a specific exception?

            if (!_friendshipPolicy.CanRemoveFriendship(currentUser, friendship)) //TODO: Maybe this method can throw exception!
            {
                throw new ApplicationException("Can not remove this friendship!"); //TODO: User friendliy exception
            }

            _friendshipRepository.Delete(friendship);
        }

        public virtual void AcceptFriendship(AcceptFriendshipInput input)
        {
            var friendship = _friendshipRepository.Get(input.Id); //TODO: Call FirstOrDefault and throw a specific exception?
            var currentUser = _taskeverUserRepository.Load(ApmSession.GetUserId());
            friendship.AcceptBy(currentUser);
            SendAcceptEmail(friendship.Pair);
        }

        public virtual void RejectFriendship(RejectFriendshipInput input)
        {
            RemoveFriendship(new RemoveFriendshipInput { Id = input.Id });
        }

        public void CancelFriendshipRequest(CancelFriendshipRequestInput input)
        {
            RemoveFriendship(new RemoveFriendshipInput { Id = input.Id });
        }

        public void UpdateLastVisitTime(UpdateLastVisitTimeInput input)
        {
            var friendship = _friendshipRepository.GetOrNull(ApmSession.GetUserId(), input.FriendUserId);
            if (friendship != null)
            {
                friendship.LastVisitTime = DateTime.Now;
            }
        }

        private void SendRequestEmail(Friendship friendship)
        {
            var mail = new MailMessage();
            mail.To.Add(friendship.Friend.EmailAddress);
            mail.IsBodyHtml = true;
            mail.Subject = friendship.User.NameAndSurname + " wants to be your friend on Taskever";
            mail.SubjectEncoding = Encoding.UTF8;

            var mailBuilder = new StringBuilder();
            mailBuilder.Append(
@"<!DOCTYPE html>
<html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta charset=""utf-8"" />
    <title>{TEXT_HTML_TITLE}</title>
    <style>
        body {
            font-family: Verdana, Geneva, 'DejaVu Sans', sans-serif;
            font-size: 12px;
        }
    </style>
</head>
<body>
    <h3>{TEXT_HEADER}</h3>
    <p>{TEXT_DESCRIPTION}</p>
    <p>&nbsp;</p>
    <p><a href=""http://www.taskever.com/#friends?activeSection=FriendshipRequests"">{TEXT_CLICK_TO_ANSWER}</a></p>
    <p>&nbsp;</p>
    <p><a href=""http://www.taskever.com"">http://www.taskever.com</a></p>
</body>
</html>");

            mailBuilder.Replace("{TEXT_HTML_TITLE}", "Friendship request on Taskever");
            mailBuilder.Replace("{TEXT_HEADER}", "You have a friendship request on Taskever");
            mailBuilder.Replace("{TEXT_DESCRIPTION}", friendship.User.NameAndSurname + " has sent a friendship request to you.");
            mailBuilder.Replace("{TEXT_CLICK_TO_ANSWER}", "Click here to answer to the request.");

            mail.Body = mailBuilder.ToString();
            mail.BodyEncoding = Encoding.UTF8;

            _emailService.SendEmail(mail);
        }

        private void SendAcceptEmail(Friendship friendship)
        {
            var mail = new MailMessage();
            mail.To.Add(friendship.User.EmailAddress);
            mail.IsBodyHtml = true;
            mail.Subject = friendship.Friend.NameAndSurname + " accepted your friendship request on Taskever";
            mail.SubjectEncoding = Encoding.UTF8;

            var mailBuilder = new StringBuilder();
            mailBuilder.Append(
@"<!DOCTYPE html>
<html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta charset=""utf-8"" />
    <title>{TEXT_HTML_TITLE}</title>
    <style>
        body {
            font-family: Verdana, Geneva, 'DejaVu Sans', sans-serif;
            font-size: 12px;
        }
    </style>
</head>
<body>
    <h3>{TEXT_HEADER}</h3>
    <p>{TEXT_DESCRIPTION}</p>
    <p>&nbsp;</p>
    <p><a href=""http://www.taskever.com/#user/{FRIEND_ID}"">{TEXT_CLICK_TO_SEE_PROFILE}</a></p>
    <p>&nbsp;</p>
    <p><a href=""http://www.taskever.com"">http://www.taskever.com</a></p>
</body>
</html>");

            mailBuilder.Replace("{TEXT_HTML_TITLE}", "Friendship request is accepted on Taskever");
            mailBuilder.Replace("{TEXT_HEADER}", "Your friendship request is accepted!");
            mailBuilder.Replace("{TEXT_DESCRIPTION}", friendship.Friend.NameAndSurname + " has accepted your friendship request.");
            mailBuilder.Replace("{TEXT_CLICK_TO_SEE_PROFILE}", "Click here to see profile of " + friendship.Friend.NameAndSurname);
            mailBuilder.Replace("{FRIEND_ID}", friendship.Friend.Id.ToString());

            mail.Body = mailBuilder.ToString();
            mail.BodyEncoding = Encoding.UTF8;

            _emailService.SendEmail(mail);
        }
    }
}