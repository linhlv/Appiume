using System.Data.Common;
using System.Data.Entity;
using Appiume.Apm.Tenancy.Ef;
using Appiume.Web.IoT.Core.Authorization.Roles;
using Appiume.Web.IoT.Core.Events;
using Appiume.Web.IoT.Core.Users;
using Appiume.Web.IoT.Core.MultiTenancy;

namespace Appiume.Web.IoT.Ef
{
    public class IoTDbContext : ApmTenancyDbContext<Tenant, Role, User>
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<Event> Events { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual IDbSet<EventRegistration> EventRegistrations { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public IoTDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by Apm to pass connection string defined in ApmDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of ApmDbContext since Apm automatically handles it.
         */
        public IoTDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public IoTDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}
