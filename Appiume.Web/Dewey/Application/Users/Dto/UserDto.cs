﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appiume.Web.Dewey.Application.Users.Dto
{
    /// <summary>
    /// Simple User DTO class.
    /// </summary>
    public class UserDto 
    {
        /// <summary>
        /// Name of the user.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Surname of the user.
        /// </summary>
        public virtual string Surname { get; set; }

        /// <summary>
        /// Email address of the user.
        /// </summary>
        public virtual string EmailAddress { get; set; }

        /// <summary>
        /// Profile image of the user.
        /// </summary>
        public virtual string ProfileImage { get; set; }
    }
}