﻿using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Apm.Tenancy.Ef;
using Appiume.Web.Dewey.Core.Activities;
using Appiume.Web.Dewey.Core.Authorization.Roles;
using Appiume.Web.Dewey.Core.Events;
using Appiume.Web.Dewey.Core.Friendships;
using Appiume.Web.Dewey.Core.Users;
using Appiume.Web.Dewey.Core.Tasks;
using Appiume.Web.Dewey.Core.MultiTenancy;
using Appiume.Web.Dewey.Core.People;

namespace Appiume.Web.Dewey.EntityFramework
{
    public class DeweyDbContext : ApmTenancyDbContext<Tenant, Role, User>
    {
        /// <summary>
        ///
        /// </summary>
        public virtual IDbSet<Event> Events { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual IDbSet<EventRegistration> EventRegistrations { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual IDbSet<Task> Tasks { get; set; }

        /// <summary>
        ///
        /// </summary>
        public virtual IDbSet<Friendship> Friendships { get; set; }

        /// <summary>
        ///
        /// </summary>
        //public virtual new IDbSet<User> Users { get; set; }

        /* NOTE:
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public DeweyDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by Apm to pass connection string defined in ApmDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of ApmDbContext since Apm automatically handles it.
         */
        public DeweyDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public DeweyDbContext(DbConnection connection)
            : base(connection, true)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //TODO: Ignore base classes

            modelBuilder.Ignore<ApmUser<User>>();

            modelBuilder.Entity<User>().ToTable("ApmUsers");
            modelBuilder.Entity<Activity>().ToTable("SocialActivities")
                .Map<CreateTaskActivity>(m => m.Requires("ActivityType").HasValue(1))
                .Map<CompleteTaskActivity>(m => m.Requires("ActivityType").HasValue(2));

            modelBuilder.Entity<Friendship>()
                .ToTable("SocialFriendships");
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Task>().ToTable("SocialTasks");
            modelBuilder.Entity<UserFollowedActivity>().ToTable("SocialUserFollowedActivities");
        }
    }
}
