using System;
using Appiume.Apm.MultiTenancy;
using Appiume.Apm.Tenancy.Authorization.Roles;
using Appiume.Apm.Tenancy.Authorization.Users;
using Appiume.Apm.Tenancy.MultiTenancy;

namespace Appiume.Apm.Tenancy.Configuration
{
    public class ApmTenancyEntityTypes : IApmTenancyEntityTypes
    {
        public Type User
        {
            get { return _user; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                if (!typeof (ApmUserBase).IsAssignableFrom(value))
                {
                    throw new ApmException(value.AssemblyQualifiedName + " should be derived from " + typeof(ApmUserBase).AssemblyQualifiedName);
                }

                _user = value;
            }
        }
        private Type _user;

        public Type Role
        {
            get { return _role; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                if (!typeof(ApmRoleBase).IsAssignableFrom(value))
                {
                    throw new ApmException(value.AssemblyQualifiedName + " should be derived from " + typeof(ApmRoleBase).AssemblyQualifiedName);
                }

                _role = value;
            }
        }
        private Type _role;

        public Type Tenant
        {
            get { return _tenant; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                if (!typeof(ApmTenantBase).IsAssignableFrom(value))
                {
                    throw new ApmException(value.AssemblyQualifiedName + " should be derived from " + typeof(ApmTenantBase).AssemblyQualifiedName);
                }

                _tenant = value;
            }
        }
        private Type _tenant;
    }
}