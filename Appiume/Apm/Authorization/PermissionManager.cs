﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Web;
using Appiume.Apm.Application.Features;
using Appiume.Apm.Collections.Extensions;
using Appiume.Apm.Configuration.Startup;
using Appiume.Apm.Dependency;
using Appiume.Apm.MultiTenancy;
using Appiume.Apm.Runtime.Session;

namespace Appiume.Apm.Authorization
{
    /// <summary>
    /// Permission manager.
    /// </summary>
    internal class PermissionManager : PermissionDefinitionContextBase, IPermissionManager, ISingletonDependency
    {
        public IApmSession ApmSession { get; set; }

        private readonly IIocManager _iocManager;
        private readonly IAuthorizationConfiguration _authorizationConfiguration;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PermissionManager(
            IIocManager iocManager,
            IAuthorizationConfiguration authorizationConfiguration)
        {
            _iocManager = iocManager;
            _authorizationConfiguration = authorizationConfiguration;

            ApmSession = NullApmSession.Instance;
        }

        public void Initialize()
        {
            foreach (var providerType in _authorizationConfiguration.Providers)
            {
                _iocManager.RegisterIfNot(providerType, DependencyLifeStyle.Transient); //TODO: Needed?
                using (var provider = _iocManager.ResolveAsDisposable<AuthorizationProvider>(providerType))
                {
                    provider.Object.SetPermissions(this);
                }
            }

            Permissions.AddAllPermissions();
        }

        public Permission GetPermission(string name)
        {
            var permission = Permissions.GetOrDefault(name);
            if (permission == null)
            {
                throw new ApmException("There is no permission with name: " + name);
            }

            return permission;
        }

        public IReadOnlyList<Permission> GetAllPermissions(bool tenancyFilter = true)
        {
            using (var featureDependencyContext = _iocManager.ResolveAsDisposable<FeatureDependencyContext>())
            {
                var featureDependencyContextObject = featureDependencyContext.Object;
                return Permissions.Values
                    .WhereIf(tenancyFilter, p => p.MultiTenancySides.HasFlag(ApmSession.MultiTenancySide))
                    .Where(p =>
                        p.FeatureDependency == null ||
                        ApmSession.MultiTenancySide == MultiTenancySides.Host ||
                        p.FeatureDependency.IsSatisfied(featureDependencyContextObject)
                    ).ToImmutableList();
            }
        }

        public IReadOnlyList<Permission> GetAllPermissions(MultiTenancySides multiTenancySides)
        {
            using (var featureDependencyContext = _iocManager.ResolveAsDisposable<FeatureDependencyContext>())
            {
                var featureDependencyContextObject = featureDependencyContext.Object;
                return Permissions.Values
                    .Where(p => p.MultiTenancySides.HasFlag(multiTenancySides))
                    .Where(p =>
                        p.FeatureDependency == null ||
                        ApmSession.MultiTenancySide == MultiTenancySides.Host ||
                        (p.MultiTenancySides.HasFlag(MultiTenancySides.Host) &&
                         multiTenancySides.HasFlag(MultiTenancySides.Host)) ||
                        p.FeatureDependency.IsSatisfied(featureDependencyContextObject)
                    ).ToImmutableList();
            }
        }
    }
}