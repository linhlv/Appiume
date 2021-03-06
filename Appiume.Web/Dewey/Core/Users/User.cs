﻿using System;
using Appiume.Apm.Extensions;
using Appiume.Apm.Tenancy.Authorization.Users;
using Microsoft.AspNet.Identity;

namespace Appiume.Web.Dewey.Core.Users
{
    public class User : ApmUser<User>
    {
        /// <summary>
        /// Profile image of the user. 
        /// </summary>
        public virtual string ProfileImage { get; set; }

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress, string password)
        {
            return new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Password = new PasswordHasher().HashPassword(password),
                IsEmailConfirmed = true,
                IsActive = true
            };
        }
    }
}