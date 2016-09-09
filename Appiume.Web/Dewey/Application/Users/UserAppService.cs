using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appiume.Apm.Authorization;
using Appiume.Web.Dewey.Application.Users.Dto;
using Appiume.Web.Dewey.Core.Users;
using System.Net.Mail;
using Appiume.Apm.Application.Services.Dto;
using Appiume.Apm.Domain.Repositories;
using Appiume.Apm.Runtime.Session;
using Appiume.Apm.UI;
using Appiume.Web.Dewey.Application.Mapping;
using Appiume.Web.Dewey.Core.Friendships;
using Appiume.Web.Dewey.Core.Utils.Mail;
using Microsoft.AspNet.Identity;

namespace Appiume.Web.Dewey.Application.Users
{
    /* THIS IS JUST A SAMPLE. */
    public class UserAppService : EventCloudAppServiceBase, IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly UserManager _userManager;
        private readonly IPermissionManager _permissionManager;
        private readonly IEmailService _emailService;
        private readonly IFriendshipRepository _friendshipRepository;

        public UserAppService(UserManager userManager,
            IPermissionManager permissionManager,
            IFriendshipRepository friendshipRepository,
            IEmailService emailService,
            IRepository<User, long> userRepository)
        {
            _userManager = userManager;
            _permissionManager = permissionManager;
            _friendshipRepository = friendshipRepository;
            _emailService = emailService;
            _userRepository = userRepository;
        }

        public ListResultOutput<UserDto> GetUsers()
        {
            return new ListResultOutput<UserDto>
            {
                Items = _userManager.Users.ToList().MapTo<List<UserDto>>()
            };
        }

        public async Task ProhibitPermission(ProhibitPermissionInput input)
        {
            var user = await _userManager.GetUserByIdAsync(input.UserId);
            var permission = _permissionManager.GetPermission(input.PermissionName);

            await _userManager.ProhibitPermissionAsync(user, permission);
        }

        //Example for primitive method parameters.
        public async Task RemoveFromRole(long userId, string roleName)
        {
            CheckErrors(await _userManager.RemoveFromRoleAsync(userId, roleName));
        }


        public GetUserProfileOutput GetUserProfile(GetUserProfileInput input)
        {
            var currentUser = _userRepository.Load(ApmSession.GetUserId());

            var profileUser = _userRepository.Get(input.UserId);
            if (profileUser == null)
            {
                throw new UserFriendlyException("Can not found the user!");
            }

            if (profileUser.Id != currentUser.Id)
            {
                var friendship = _friendshipRepository.GetOrNull(currentUser.Id, input.UserId);
                if (friendship == null)
                {
                    return new GetUserProfileOutput { CanNotSeeTheProfile = true, User = profileUser.MapTo<UserDto>() }; //TODO: Is it true to send user informations?
                }

                if (friendship.Status != FriendshipStatus.Accepted)
                {
                    return new GetUserProfileOutput { CanNotSeeTheProfile = true, SentFriendshipRequest = true, User = profileUser.MapTo<UserDto>() };
                }
            }

            return new GetUserProfileOutput { User = profileUser.MapTo<UserDto>() };
        }

        public ChangeProfileImageOutput ChangeProfileImage(ChangeProfileImageInput input)
        {
            var currentUser = _userRepository.Get(ApmSession.GetUserId()); //TODO: test Load method
            var oldFileName = currentUser.ProfileImage;

            currentUser.ProfileImage = input.FileName;

            return new ChangeProfileImageOutput() { OldFileName = oldFileName };
        }


        public IList<UserDto> GetAllUsers()
        {
            return _userRepository.GetAllList().MapIList<User, UserDto>();
        }

        public UserDto GetActiveUserOrNull(string emailAddress, string password) //TODO: Make this GetUserOrNullInput and GetUserOrNullOutput
        {
            var userEntity = _userRepository.FirstOrDefault(user => user.EmailAddress == emailAddress && user.Password == password && user.IsEmailConfirmed);
            return userEntity.MapTo<UserDto>();
        }

        public GetUserOutput GetUser(GetUserInput input)
        {
            var user = _userRepository.Get(input.UserId);
            return new GetUserOutput(user.MapTo<UserDto>());
        }

        public void RegisterUser(RegisterUserInput registerUser)
        {
            var existingUser = _userRepository.FirstOrDefault(u => u.EmailAddress == registerUser.EmailAddress);
            if (existingUser != null)
            {
                if (!existingUser.IsEmailConfirmed)
                {
                    SendConfirmationEmail(existingUser);
                    throw new UserFriendlyException("You registere with this email address before (" + registerUser.EmailAddress + ")! We re-sent an activation code to your email!");
                }

                throw new UserFriendlyException("There is already a user with this email address (" + registerUser.EmailAddress + ")! Select another email address!");
            }

            var userEntity = registerUser.MapTo<User>();
            userEntity.Password = new PasswordHasher().HashPassword(userEntity.Password);
            //userEntity.GenerateEmailConfirmationCode();
            _userRepository.Insert(userEntity);
            SendConfirmationEmail(userEntity);
        }

        public void ConfirmEmail(ConfirmEmailInput input)
        {
            var user = _userRepository.Get(input.UserId);
            //user.ConfirmEmail(input.ConfirmationCode);
        }

        public GetCurrentUserInfoOutput GetCurrentUserInfo(GetCurrentUserInfoInput input)
        {
            //TODO: Use GetUser?
            return new GetCurrentUserInfoOutput { User = _userRepository.Get(ApmSession.GetUserId()).MapTo<UserDto>() };
        }

        public void ChangePassword(ChangePasswordInput input)
        {
            var currentUser = _userRepository.Get(ApmSession.GetUserId());
            if (currentUser.Password != input.CurrentPassword)
            {
                throw new UserFriendlyException("Current password is invalid!");
            }

            currentUser.Password = input.NewPassword;
            currentUser.Password = new PasswordHasher().HashPassword(currentUser.Password);
        }

        public void SendPasswordResetLink(SendPasswordResetLinkInput input)
        {
            var user = _userRepository.FirstOrDefault(u => u.EmailAddress == input.EmailAddress);
            if (user == null)
            {
                throw new UserFriendlyException("Can not find this email address in the system.");
            }

            //user.GeneratePasswordResetCode();
            SendPasswordResetLinkEmail(user);
        }

        public void ResetPassword(ResetPasswordInput input)
        {
            var user = _userRepository.Get(input.UserId);
            if (string.IsNullOrWhiteSpace(user.PasswordResetCode))
            {
                throw new UserFriendlyException("You can not reset your password. You must first send a reset password link to your email.");
            }

            if (input.PasswordResetCode != user.PasswordResetCode)
            {
                throw new UserFriendlyException("Password reset code is invalid!");
            }

            user.Password = input.Password;
            user.PasswordResetCode = null;
        }


        #region Private methods

        private void SendConfirmationEmail(User user)
        {
            var mail = new MailMessage();
            mail.To.Add(user.EmailAddress);
            mail.IsBodyHtml = true;
            mail.Subject = "Email confirmation for Taskever";
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
    <h3>{TEXT_WELCOME}</h3>
    <p>{TEXT_DESCRIPTION}</p>
    <p>&nbsp;</p>
    <p><a href=""http://www.taskever.com/Account/ConfirmEmail?UserId={USER_ID}&ConfirmationCode={CONFIRMATION_CODE}"">http://www.taskever.com/Account/ConfirmEmail?UserId={USER_ID}&amp;ConfirmationCode={CONFIRMATION_CODE}</a></p>
    <p>&nbsp;</p>
    <p><a href=""http://www.taskever.com"">http://www.taskever.com</a></p>
</body>
</html>");

            mailBuilder.Replace("{TEXT_HTML_TITLE}", "Email confirmation for Taskever");
            mailBuilder.Replace("{TEXT_WELCOME}", "Welcome to Taskever.com!");
            mailBuilder.Replace("{TEXT_DESCRIPTION}",
                "Click the link below to confirm your email address and login to the Taskever.com");
            mailBuilder.Replace("{USER_ID}", user.Id.ToString());
            mailBuilder.Replace("{CONFIRMATION_CODE}", user.EmailConfirmationCode);

            mail.Body = mailBuilder.ToString();
            mail.BodyEncoding = Encoding.UTF8;

            _emailService.SendEmail(mail);
        }

        private void SendPasswordResetLinkEmail(User user)
        {
            var mail = new MailMessage();
            mail.To.Add(user.EmailAddress);
            mail.IsBodyHtml = true;
            mail.Subject = "Password reset for Taskever";
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
    <h3>{TEXT_WELCOME}</h3>
    <p>{TEXT_DESCRIPTION}</p>
    <p>&nbsp;</p>
    <p><a href=""http://www.taskever.com/Account/ResetPassword?UserId={USER_ID}&ResetCode={RESET_CODE}"">http://www.taskever.com/Account/ResetPassword?UserId={USER_ID}&amp;ResetCode={RESET_CODE}</a></p>
    <p>&nbsp;</p>
    <p><a href=""http://www.taskever.com"">http://www.taskever.com</a></p>
</body>
</html>");

            mailBuilder.Replace("{TEXT_HTML_TITLE}", "Password reset for Taskever");
            mailBuilder.Replace("{TEXT_WELCOME}", "Reset your password on Taskever!");
            mailBuilder.Replace("{TEXT_DESCRIPTION}", "Click the link below to reset your password on the Taskever.com");
            mailBuilder.Replace("{USER_ID}", user.Id.ToString());
            mailBuilder.Replace("{RESET_CODE}", user.PasswordResetCode);

            mail.Body = mailBuilder.ToString();
            mail.BodyEncoding = Encoding.UTF8;

            _emailService.SendEmail(mail);
        }

        #endregion
    }
}